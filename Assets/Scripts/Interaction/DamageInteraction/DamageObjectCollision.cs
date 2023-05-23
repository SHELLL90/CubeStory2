using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObjectCollision : MonoBehaviour
{
    [SerializeField] private float velocityCollisionDamage = 20.0f;

    private IHealth _health;

    private void Awake()
    {
        _health = GetComponent<IHealth>();
    }

    public bool TryDamage(float valueDamage, Vector3 relativeVelocity)
    {
        return TryDamage(valueDamage, relativeVelocity.magnitude);
    }

    public bool TryDamage(float valueDamage, float relativeVelocity)
    {
        if (relativeVelocity >= velocityCollisionDamage)
        {
            _health.Damage(valueDamage);
            return true;
        }
        return false;
    }

    private void OnValidate()
    {
        IHealth health = GetComponent<IHealth>();
        if (health == null)
        {
            Debug.LogError("У объекта: " + gameObject.name + " нет компонента с IHealth!!!");
        }
    }
}
