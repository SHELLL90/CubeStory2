using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLogic : MonoBehaviour
{
    [Header("Levels Main Menu")]
    [SerializeField] private LevelsMainMenu levelsMainMenu;

    public void OpenLevelsMenu()
    {
        levelsMainMenu.OpenLevelsMenu();
    }
}
