using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarObject : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [Min(0.1f)][SerializeField] private float defaultMultiplyScale = 1.0f;
    [SerializeField] private bool delayHide;
    [ShowIf("delayHide")][AllowNesting][SerializeField] private float timeDelayHide = 2.0f;

    private bool _isVisible;
    public bool IsVisible
    {
        get { return _isVisible; }
        set
        {
            _isVisible = value;
        }
    }

    private void OnBecameVisible()
    {
        IsVisible = true;
    }

    private void OnBecameInvisible()
    {
        IsVisible = false;
        BarUI = null;
    }

    private EnemyLogic _enemy;

    private HealthBarUI _healthBarUI;
    private HealthBarUI BarUI
    {
        get 
        {
            if (_healthBarUI == null)
            {
                _healthBarUI = HealthBarManager.Instance.GetHealthBar();
                _healthBarUI.transform.localScale = Vector3.zero;
            }
            return _healthBarUI; 
        }
        set 
        {
            if (_healthBarUI != null && value == null)
            {
                HealthBarManager.Instance.ReturnHealthBar(_healthBarUI);
            }
            _healthBarUI = value; 
        }
    }




    private void Awake()
    {
        _enemy = GetComponent<EnemyLogic>();
        if (_enemy == null)
        {
            Debug.LogError("enemy is Null!!! " + gameObject.name);
            Destroy(this);
            return;
        }
        else
        {
            _enemy.ActionChangeHealth += ChangeHealth;
        }
    }

    private void ChangeHealth(float currentHealth, float maxHealth)
    {
        if (BarUI != null)
        {
            BarUI.SetBar(transform, offset, defaultMultiplyScale, this, currentHealth, maxHealth);
            BarUI.transform.SetAsLastSibling();

            if (delayHide)
            {
                StopAllCoroutines();
                StartCoroutine(WaitHide());
            }
        }
    }

    public void HideHealthBar()
    {
        BarUI = null;
    }

    private IEnumerator WaitHide()
    {
        yield return new WaitForSeconds(timeDelayHide);
        HideHealthBar();
    }
}
