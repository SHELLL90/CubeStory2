using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    public void Finish()
    {
        LevelSO nextLevel = DataManager.Instance.GetNextLevelSO();
        
        if (nextLevel != null)
        {
            Debug.Log(nextLevel.nameScene);
            SceneTransition.SwitchScene(nextLevel.nameScene);
        }
        else
        {
            Debug.LogError("Next Level Is Null!!!");
        }
    }
}
