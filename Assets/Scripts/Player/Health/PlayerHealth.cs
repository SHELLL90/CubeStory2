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
                Dead();
            }
            else if (_currentHealth > maxHealth)
            {
                _currentHealth = maxHealth;
            }

            if (PlayerHealthUI.Instance != null) PlayerHealthUI.Instance.UpdateHealthBar(_currentHealth / maxHealth);
        }
    }
    public bool IsDead { get { return _playerStates.IsDead; } }
    private PlayerStates _playerStates;

    private void OnEnable()
    {
        CollectedItemManager.ActionRegenHealth += Regen;
    }

    private void OnDisable()
    {
        CollectedItemManager.ActionRegenHealth -= Regen;
    }

    private void Awake()
    {
        _currentHealth = MaxHealth;
        _playerStates = GetComponent<PlayerStates>();
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Damage(1.0f);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            Regen(1.0f);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            CurrentHealth = 0;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Respawn();
        }
    }
#endif

    public void Damage(float value)
    {
        Debug.Log("Damage player " + value);
        _playerStates.ActionDamage?.Invoke();
        CurrentHealth -= value;
    }

    public void Regen(float value)
    {
        Debug.Log("Regen Player " + value);
        CurrentHealth += value;
    }

    private void Dead()
    {
        _playerStates.IsDead = true;

        InputManager.Instance.SwitchActionMap(ActionMaps.None);
    }

    private void Respawn()
    {
        transform.SetParent(null);
        Vector3 newPos = CheckPointManager.Instance.GetPositionCheckPoint();

        newPos.z = transform.position.z;
        transform.position = newPos;

        CurrentHealth = maxHealth;
        _playerStates.IsDead = false;

        InputManager.Instance.SwitchActionMap(ActionMaps.Player);
    }
}
