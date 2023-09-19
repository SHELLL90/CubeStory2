using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class LevelsMainMenu : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] private GameObject background;
    [Header("Groups")]
    [SerializeField] private RectTransform parentGroups;
    [SerializeField] private GameObject gameObjectGroups;
    [SerializeField] private GroupButton prefabButtonGroup;
    [Header("Levels")]
    [SerializeField] private RectTransform parentLevels;
    [SerializeField] private GameObject gameObjectLevels;
    [SerializeField] private LevelButton prefabButtonLevel;
    [Min(0)] [SerializeField] private int maxLevelsOnScreen = 20;
    [SerializeField] private GameObject nextArrow;
    [SerializeField] private GameObject backArrow;

    private List<LevelButton> _levelButtons = new List<LevelButton>();
    private List<GroupButton> _groupButtons = new List<GroupButton>();

    [ShowNonSerializedField] private int _lastIndexCreatedLevels = 0;
    [ShowNonSerializedField] private int _currentLengthLevels = 0;

    private GroupLevelsSO _currentGroupLevels;

    public void OpenLevelsMenu()
    {
        background.SetActive(true);

        OpenGroups();
    }

    public void CloseLevelsMenu()
    {
        background.SetActive(false);

        TryDeleteAllGroups();
        TryDeleteAllLevels();
    }

    public void OpenGroups()
    {
        gameObjectGroups.SetActive(true);
        gameObjectLevels.SetActive(false);

        CreateGroups();
    }

    private void CreateGroups()
    {
        TryDeleteAllGroups();

        GroupLevelsSO[] groups = DataManager.Instance.GetGroups();
        if (groups != null && groups.Length > 0)
        {
            for (int i = 0; i < groups.Length; i++)
            {
                CreateGroup(groups[i]);
            }
        }
        else
        {
            Debug.LogError("Create Groups ERROR!!! Groups is null or length == 0 !!!");
        }
    }

    private void CreateGroup(GroupLevelsSO groupLevelsSO)
    {
        GroupButton groupButton = Instantiate(prefabButtonGroup, parentGroups);

        groupButton.SetGroupData(groupLevelsSO.groupID, groupLevelsSO.groupSprite, this);
    }

    private void TryDeleteAllGroups()
    {
        if (_groupButtons != null)
        {
            for (int i = 0; i < _groupButtons.Count; i++)
            {
                Destroy(_groupButtons[i].gameObject);
            }
        }

        _groupButtons = new List<GroupButton>();
    }

    public void SelectGroup(string groupID)
    {
        gameObjectGroups.SetActive(false);
        gameObjectLevels.SetActive(true);

        _lastIndexCreatedLevels = 0;
        _currentGroupLevels = null;

        CreateLevels(DataManager.Instance.GetGroupByID(groupID));
    }

    private void CreateLevels(GroupLevelsSO groupLevelsSO)
    {
        TryDeleteAllLevels();
        _currentGroupLevels = groupLevelsSO;

        if (groupLevelsSO != null)
        {
            if (groupLevelsSO.levels != null && groupLevelsSO.levels.Length > 0)
            {
                _lastIndexCreatedLevels = Mathf.Clamp(_lastIndexCreatedLevels, 0, groupLevelsSO.levels.Length - 1);

                int countCreatedLevels = 0;
                _currentLengthLevels = groupLevelsSO.levels.Length;
                for (int i = _lastIndexCreatedLevels; i < groupLevelsSO.levels.Length; i++)
                {
                    CreateLevel(groupLevelsSO.levels[i], i);
                    _lastIndexCreatedLevels = i;

                    countCreatedLevels++;
                    if (countCreatedLevels >= maxLevelsOnScreen) break;
                }
            }
            else
            {
                Debug.LogError("Create Levels ERROR!!! Levels in group is null or length == 0 !!! Group ID: " + groupLevelsSO.groupID);
            }
        }
        else
        {
            Debug.LogError("Create Levels ERROR!!! Groups is null or length == 0 !!!");
        }

        CheckArrowsLevels();
    }

    private void CreateLevel(LevelSO levelSO, int index)
    {
        LevelButton levelButton = Instantiate(prefabButtonLevel, parentLevels);

        levelButton.SetLevelData(levelSO.id, index + 1, levelSO.spriteLevel, this);

        _levelButtons.Add(levelButton);
    }

    private void CheckArrowsLevels()
    {
        if (_lastIndexCreatedLevels - maxLevelsOnScreen > 0)
        {
            backArrow.gameObject.SetActive(true);
        }
        else
        {
            backArrow.gameObject.SetActive(false);
        }

        if (_lastIndexCreatedLevels + 1 < _currentLengthLevels)
        {
            nextArrow.gameObject.SetActive(true);
        }
        else
        {
            nextArrow.gameObject.SetActive(false);
        }
    }

    public void NextLevels()
    {
        _lastIndexCreatedLevels += 1;
        CreateLevels(_currentGroupLevels);
    }

    public void BackLevels()
    {
        _lastIndexCreatedLevels -= maxLevelsOnScreen * 2;
        _lastIndexCreatedLevels += 1;
        CreateLevels(_currentGroupLevels);
    }

    private void TryDeleteAllLevels()
    {
        if (_levelButtons != null)
        {
            for (int i = 0; i < _levelButtons.Count; i++)
            {
                Destroy(_levelButtons[i].gameObject);
            }
        }

        _levelButtons = new List<LevelButton>();
    }

    public void PlayLevel(string id)
    {
        LevelSO level = DataManager.Instance.GetLevelSOByID(id);

        LevelsManager.Instance.PlayLevel(level);
    }
}
