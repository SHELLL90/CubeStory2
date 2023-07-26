using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    public void Finish()
    {
        LevelSO nextLevel = DataManager.Instance.GetNextLevelSO();
        Debug.Log(nextLevel.nameScene);
        if (nextLevel != null)
        {
            SceneTransition.SwitchScene(nextLevel.nameScene);
        }
        else
        {
            Debug.LogError("Next Level Is Null!!!");
        }
    }
}
