using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    public static LevelsManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        transform.SetParent(null);

        DontDestroyOnLoad(this.gameObject);
    }

    public void PlayLevel(LevelSO levelSO)
    {
        if (levelSO != null)
        {
            SceneTransition.SwitchScene(levelSO.nameScene);
        }
        else
        {
            Debug.LogError("Play Level ERROR!!! LevelSO is Null!!!");
        }
    }

    public void GoToMenu()
    {
        SaveLevelProgress();

        SceneTransition.SwitchScene("MainMenu");
    }

    public void ResetCurrentLevel()
    {
        LevelSO currentLevel = DataManager.Instance.GetCurrentLevelSO();

        if (currentLevel != null)
        {
            SaveLevelProgress();

            SceneTransition.SwitchScene(currentLevel.nameScene);
        }
    }

    public void PlayNextLevel()
    {
        LevelSO nextLevel = DataManager.Instance.GetNextLevelSO();

        if (nextLevel != null)
        {
            SaveLevelProgress();
            Debug.Log("Next Level: " + nextLevel.nameScene);
            SceneTransition.SwitchScene(nextLevel.nameScene);
        }
        else
        {
            Debug.LogError("Next Level Is Null!!!");
        }
    }

    private void SaveLevelProgress()
    {
        
    }
}
