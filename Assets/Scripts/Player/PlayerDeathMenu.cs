using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathMenu : MonoBehaviour
{
    public static PlayerDeathMenu Instance { get; private set; }

    [SerializeField] private GameObject deathMenu;
    [SerializeField] private GameObject[] unactiveObjectDeath;

    private void OnDestroy()
    {
        Instance = null;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetActiveObjects();
    }

    public void DeathCheck()
    {
        SetActiveObjects();
    }

    public void Respawn()
    {
        if (PlayerComponents.Health != null) PlayerComponents.Health.Respawn();
    }

    private void SetActiveObjects()
    {
        if (PlayerComponents.Health == null) return;
        deathMenu.SetActive(PlayerComponents.Health.IsDead);
        for (int i = 0; i < unactiveObjectDeath.Length; i++)
        {
            if (unactiveObjectDeath[i] != null) unactiveObjectDeath[i].SetActive(!PlayerComponents.Health.IsDead);
        }
    }
}
