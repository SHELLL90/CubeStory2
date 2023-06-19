using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStates))]
public class CameraIsDead : MonoBehaviour
{
    [SerializeField] private GameObject cameraDead;
    private void Awake()
    {
        GetComponent<PlayerStates>().ActionDead += Dead;

        Dead(false);
    }

    private void Dead(bool isDead)
    {
        cameraDead.SetActive(isDead);
    }
}
