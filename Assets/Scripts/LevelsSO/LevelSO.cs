using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "Level SO", menuName = "ScriptableObjects/levelSO", order = 1)]
public class LevelSO : ScriptableObject
{
#if UNITY_EDITOR
    public SceneAsset sceneLevel;
#endif
    [ReadOnly]
    public string id;
    [ReadOnly]
    public string nameScene;
    [Header("Other SO")]
    [ReadOnly]
    public GroupLevelsSO group;
    [ReadOnly]
    public LevelSO nextLevel;
    [Header("Setting")]
    [ShowAssetPreview]
    public Sprite spriteLevel;
#if UNITY_EDITOR
    public void TrySetIDScene(string groupID)
    {
        if (sceneLevel != null)
        {
            id = groupID + "_" + sceneLevel.name;
            nameScene = sceneLevel.name;
        }
        else
        {
            Debug.LogError("Scene Level Is Null " + this.name + "; Group ID: " + groupID);
        }
    }
#endif
}
