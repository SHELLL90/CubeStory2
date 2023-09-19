using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }
    [Expandable]
    [SerializeField] private DataSettingSO dataSetting;

    public static Action ActionDataLoaded { get; set; }
    
    public static bool IsMobile { get; private set; }
    public static bool DataLoaded { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        transform.SetParent(null);

        Instance = this;
        DontDestroyOnLoad(this.gameObject);

#if UNITY_EDITOR
        DataLoad();
#endif
    }

    private void DataLoad()
    {


#if UNITY_EDITOR
        IsMobile = dataSetting.editorIsMobile;
#endif

        DataLoaded = true;
        ActionDataLoaded?.Invoke();
    }

    #region LEVELS
#if UNITY_EDITOR
    [Button("Configure Levels")]
    public void ConfigureLevels()
    {
        if (dataSetting == null)
        {
            Debug.LogError("Data Setting Is NULL!!!");
            return;
        }
        if (dataSetting.groupsLevel == null || dataSetting.groupsLevel.Length == 0)
        {
            Debug.LogError("Groups Is Null!!!");
            return;
        }
        else
        {
            for (int i = 0; i < dataSetting.groupsLevel.Length; i++)
            {
                if (dataSetting.groupsLevel[i] != null)
                {
                    ConfigureGroup(dataSetting.groupsLevel[i]);
                }
                else
                {
                    Debug.LogError("Group Levels index array: " + i + " is Null!!!");
                }
            }
        }
    }

    private void ConfigureGroup(GroupLevelsSO group)
    {
        if (group.levels == null || group.levels.Length == 0)
        {
            Debug.LogError("Levels in group: " + group.name + " is Null!!!");
            return;
        }
        for (int i = 0; i < group.levels.Length; i++)
        {

            if (group.levels[i] != null)
            {
                LevelSO nextLevel = null;
                if (i + 1 < group.levels.Length)
                {
                    nextLevel = group.levels[i + 1];
                    if (nextLevel == null)
                    {
                        Debug.LogError("Next Level is Null!!! Index:" + i + 1 + "; Group " + group.name);
                    }
                }

                ConfigureLevel(group.levels[i], nextLevel, group);
            }
            else
            {
                Debug.LogError("Current Level Is Null!!! Index: " + i + "; Group " + group.name);
            }

            EditorUtility.SetDirty(group);
            AssetDatabase.SaveAssets();
        }
    }

    private void ConfigureLevel(LevelSO level, LevelSO nextLevel, GroupLevelsSO group)
    {
        level.TrySetIDScene(group.groupID);
        level.nextLevel = nextLevel;
        level.group = group;

        EditorUtility.SetDirty(level);
        AssetDatabase.SaveAssets();
    }
#endif

    public GroupLevelsSO[] GetGroups()
    {
        return dataSetting.groupsLevel;
    }

    public GroupLevelsSO GetGroupByID(string groupID)
    {
        GroupLevelsSO[] groups = GetGroups();
        if (groups != null && groups.Length > 0)
        {
            for (int i = 0; i < groups.Length; i++)
            {
                if (groups[i].groupID == groupID) return groups[i];
            }
        }
        else
        {
            Debug.LogError("Get Group By ID ERROR!!! Groups is null or length == 0 !!!");
        }

        return null;
    }

    public LevelSO GetNextLevelSO()
    {
        LevelSO currentLevel = GetCurrentLevelSO();
        if (currentLevel == null) return null;
        return currentLevel.nextLevel;
    }

    public LevelSO GetCurrentLevelSO()
    {
        string sceneName = GetCurrentSceneName();
        return GetLevelSOByNameScene(sceneName);
    }

    public LevelSO GetLevelSOByNameScene(string sceneName)
    {
        if (dataSetting == null)
        {
            Debug.LogError("Data Setting Is NULL!!!");
            return null;
        }

        if (dataSetting.groupsLevel == null || dataSetting.groupsLevel.Length == 0)
        {
            Debug.LogError("Groups Is Null!!!");
            return null;
        }

        for (int i = 0; i < dataSetting.groupsLevel.Length; i++)
        {
            for (int x = 0; x < dataSetting.groupsLevel[i].levels.Length; x++)
            {
                if (dataSetting.groupsLevel[i].levels[x].nameScene == sceneName)
                {
                    return dataSetting.groupsLevel[i].levels[x];
                }
            }
        }

        return null;
    }

    public LevelSO GetLevelSOByID(string idLevel)
    {
        if (dataSetting == null)
        {
            Debug.LogError("Data Setting Is NULL!!!");
            return null;
        }

        if (dataSetting.groupsLevel == null || dataSetting.groupsLevel.Length == 0)
        {
            Debug.LogError("Groups Is Null!!!");
            return null;
        }

        for (int i = 0; i < dataSetting.groupsLevel.Length; i++)
        {
            for (int x = 0; x < dataSetting.groupsLevel[i].levels.Length; x++)
            {
                if (dataSetting.groupsLevel[i].levels[x].id == idLevel)
                {
                    return dataSetting.groupsLevel[i].levels[x];
                }
            }
        }

        return null;
    }

    public string GetCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    #endregion LEVELS
}
