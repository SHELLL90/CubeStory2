using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyLogic))]
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
            _enemyLogic.ActionChangeHealth?.Invoke(_currentHealth, maxHealth);
            if (_currentHealth <= 0)
            {
                Death();
            }
        }
    }

    private EnemyLogic _enemyLogic;

    private void Awake()
    {
        _enemyLogic = GetComponent<EnemyLogic>();
        _currentHealth = MaxHealth;
    }

    public void Damage(float value)
    {
        CurrentHealth -= value;
        _enemyLogic.ActionHitEnemy?.Invoke();
        _enemyLogic.ActionPlayAnimation?.Invoke(TypeAnimation.Damage);
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }
}
