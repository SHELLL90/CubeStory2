using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeDamageTrap { Enter, Stay, Exit }
public class Trap : MonoBehaviour
{
    [SerializeField] private TypeDamageTrap typeDamageTrap;
    [SerializeField] private float damageValue;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (typeDamageTrap == TypeDamageTrap.Enter) TryDamage(collision.gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (typeDamageTrap == TypeDamageTrap.Stay) TryDamage(collision.gameObject);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (typeDamageTrap == TypeDamageTrap.Exit) TryDamage(collision.gameObject);
    }

    private void TryDamage(GameObject obj)
    {
        IHealth health = obj.GetComponent<IHealth>();

        if (health != null)
        {
            float multiply = 1.0f;
            if (TypeDamageTrap.Stay == typeDamageTrap) multiply = Time.fixedDeltaTime;

            health.Damage(damageValue * multiply);
        }
    }
}
