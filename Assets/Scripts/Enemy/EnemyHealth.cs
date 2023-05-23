using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
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

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public void Damage(float value)
    {
        CurrentHealth -= value;
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }
}
