using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Missile : MonoBehaviour
{
    private float _damageValue;
    private ForceAttackSetting _forceAttackSetting;
    private Rigidbody2D _rigidbody;

    public Rigidbody2D ThisRigidbody
    {
        get 
        {
            if (_rigidbody == null) _rigidbody = GetComponent<Rigidbody2D>();
            return _rigidbody; 
        }
        set { _rigidbody = value; }
    }

    private void Awake()
    {
        ThisRigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetMissileSetting(MissileSetting setting)
    {
        _damageValue = setting.damageValue;
        _forceAttackSetting = setting.forceAttackSetting;

        transform.right = (setting.target.position - transform.position).normalized;
        _rigidbody.AddForce(transform.right * setting.speed, setting.forceMode);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IHealth health = collision.transform.GetComponent<IHealth>();
        if (health != null)
        {
            health.Damage(_damageValue);
            _forceAttackSetting.ForceTarget(collision.transform, transform.position);
        }
        Destroy(this.gameObject);
    }
}
[System.Serializable]
public class MissileSetting
{
    public float damageValue;
    public ForceAttackSetting forceAttackSetting;
    public float speed;
    public ForceMode2D forceMode;
    [HideInInspector] public Transform target;
}
