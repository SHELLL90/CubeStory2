using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectedItemManager : MonoBehaviour
{
    public static CollectedItemManager Instance { get; private set; }

    [Header("Diamonds")]
    [SerializeField] private Text textNumberDiamonds;

    public static System.Action<float> ActionRegenHealth { get; set; }

    private int _currentDiamonds;

    public int CurrentDiamonds
    {
        get { return _currentDiamonds; }
        set 
        { 
            _currentDiamonds = value;
            textNumberDiamonds.text = _currentDiamonds.ToString();
        }
    }


    private void OnEnable()
    {
        Instance = this;
    }

    private void OnDisable()
    {
        Instance = null;
    }

    public void Collect(TypeCollectedItem typeCollectedItem, float value)
    {
        if (TypeCollectedItem.Diamond == typeCollectedItem) CurrentDiamonds += (int)value;
        else if (TypeCollectedItem.Heart == typeCollectedItem) ActionRegenHealth?.Invoke(value);
    }
}
