using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStates))]
public class PlayerHealth : MonoBehaviour, IHealth
{
    [SerializeField] private float maxHealth;
    public float MaxHealth { get { return maxHealth; } }

    private float _currentHealth;
    public float CurrentHealth
    {
        get
        {
            return _currentHealth;
        }
        private set
        {
            _currentHealth = value;
            if (_currentHealth <= 0)
            {
                Death();
            }
        }
    }

    private PlayerStates _playerStates;

    private void Awake()
    {
        _currentHealth = MaxHealth;
        _playerStates = GetComponent<PlayerStates>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Damage(0.1f);
        }
    }

    public void Damage(float value)
    {
        Debug.Log("Damage player " + value);
        _playerStates.ActionDamage?.Invoke();
        CurrentHealth -= value;
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }
}
