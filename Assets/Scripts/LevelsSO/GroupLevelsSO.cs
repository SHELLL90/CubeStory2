using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GroupLevels SO", menuName = "ScriptableObjects/GroupLevelsSO", order = 1)]
public class GroupLevelsSO : ScriptableObject
{
    public string groupID;
    public LevelSO[] levels;
}
