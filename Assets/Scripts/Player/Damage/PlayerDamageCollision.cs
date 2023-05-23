using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageCollision : MonoBehaviour
{
    [SerializeField] private float valueDamage = 3.0f;
    [SerializeField] private float multiplyReflect = 0.2f;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DamageObjectCollision damageObjectCollision = collision.transform.GetComponent<DamageObjectCollision>();
        if (damageObjectCollision != null)
        {
            bool damaged = damageObjectCollision.TryDamage(valueDamage, collision.relativeVelocity);
            if (damaged) _rigidbody.velocity = Vector2.Reflect(_rigidbody.velocity * multiplyReflect, collision.GetContact(0).normal);
        }
    }
}
