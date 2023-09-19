using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private Image imageLevel;
    [SerializeField] private Text levelNumberText;
    [ShowNonSerializedField] private string levelID;

    private LevelsMainMenu _levelsMainMenu;

    public void SetLevelData(string id, int levelNumber, Sprite spriteLevel, LevelsMainMenu levelsMainMenu)
    {
        levelID = id;
        _levelsMainMenu = levelsMainMenu;

        levelNumberText.text = levelNumber.ToString();

        if (spriteLevel != null) imageLevel.sprite = spriteLevel;
    }

    public void Play()
    {
        _levelsMainMenu.PlayLevel(levelID);
    }
}
