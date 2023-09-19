using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class GroupButton : MonoBehaviour
{
    [SerializeField] private Image imageGroup;
    [ShowNonSerializedField] private string groupID;

    private LevelsMainMenu _levelsMainMenu;

    public void SetGroupData(string id, Sprite spriteGroup, LevelsMainMenu levelsMainMenu)
    {
        groupID = id;
        _levelsMainMenu = levelsMainMenu;

        if (spriteGroup != null) imageGroup.sprite = spriteGroup;
    }

    public void SelectGroup()
    {
        _levelsMainMenu.SelectGroup(groupID);
    }
}
