using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInputSystem : MonoBehaviour
{
    public static MobileInputSystem Instance { get; private set; }

    [Header("Blocks")]
    [SerializeField] private GameObject move;
    [SerializeField] private GameObject jump;
    [SerializeField] private GameObject attack;
    [SerializeField] private GameObject down;

    private void OnEnable()
    {
        Instance = this;
    }

    private void OnDisable()
    {
        Instance = null;
    }

    private void Start()
    {
        TryActiveAttack(false);
        TryActiveDown(false);
    }

    public static void TryActiveMove(bool active)
    {
        if (Instance != null)
        {
            Instance.ActiveMove(active);
        }
    }

    public static void TryActiveJump(bool active)
    {
        if (Instance != null)
        {
            Instance.ActiveJump(active);
        }
    }

    public static void TryActiveAttack(bool active)
    {
        if (Instance != null)
        {
            Instance.ActiveAttack(active);
        }
    }

    public static void TryActiveDown(bool active)
    {
        if (Instance != null)
        {
            Instance.ActiveDown(active);
        }
    }

    public void ActiveMove(bool active)
    {
        move.SetActive(active);
    }

    public void ActiveJump(bool active)
    {
        jump.SetActive(active);
    }

    public void ActiveAttack(bool active)
    {
        attack.SetActive(active);
    }

    public void ActiveDown(bool active)
    {
        down.SetActive(active);
    }
}
