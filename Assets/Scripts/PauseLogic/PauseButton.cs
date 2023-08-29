using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class PauseButton : MonoBehaviour
{
    public void ClickPause()
    {
        PauseManager.Instance.Pause();
    }

    public void ClickPause(bool pause)
    {
        PauseManager.Instance.Pause(pause);
    }
}
