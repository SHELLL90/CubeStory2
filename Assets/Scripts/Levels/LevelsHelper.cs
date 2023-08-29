using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsHelper : MonoBehaviour
{
    public void GoToMenu()
    {
        LevelsManager.Instance.GoToMenu();
    }

    public void PlayNextLevel()
    {
        LevelsManager.Instance.PlayNextLevel();
    }

    public void ResetCurrentLevel()
    {
        LevelsManager.Instance.ResetCurrentLevel();
    }
}
