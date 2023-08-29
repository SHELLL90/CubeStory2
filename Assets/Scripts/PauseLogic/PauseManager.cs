using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }

    [SerializeField] private GameObject pauseBlock;
    [SerializeField] private GameObject[] unactiveObjectPause;

    public bool IsPaused { get; private set; }

    private void OnDestroy()
    {
        Instance = null;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        SetActiveObjects();
    }

    public void Pause()
    {
        Pause(!IsPaused);
    }

    public void Pause(bool pause)
    {
        if (IsPaused == pause) return;
        IsPaused = pause;

        SetActiveObjects();

        if (pause)
        {
            Time.timeScale = 0;
            InputManager.Instance.SwitchActionMap(ActionMaps.None);
        }
        else
        {
            Time.timeScale = 1;
            InputManager.Instance.SwitchToLastActionMap();
        }
    }

    private void SetActiveObjects()
    {
        pauseBlock.SetActive(IsPaused);
        for (int i = 0; i < unactiveObjectPause.Length; i++)
        {
            if (unactiveObjectPause[i] != null) unactiveObjectPause[i].SetActive(!IsPaused);
        }
    }
}
