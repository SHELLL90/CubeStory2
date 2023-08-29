using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    public void Finish()
    {
        LevelsManager.Instance.PlayNextLevel();
    }
}
