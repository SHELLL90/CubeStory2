using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data SO", menuName = "ScriptableObjects/DataSettingSO", order = 1)]
public class DataSettingSO : ScriptableObject
{
    [Header("Editor")]
    public bool editorIsMobile;
    [Header("Levels")]
    public GroupLevelsSO[] groupsLevel;
}
