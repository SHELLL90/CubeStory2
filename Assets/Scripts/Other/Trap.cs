using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public enum TypeDamageTrap { Enter, Stay, Exit }
public class Trap : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] private TypeDamageTrap typeDamageTrap;
    [SerializeField] private float damageValue;
    [Header("Force")]
    [SerializeField] private bool needForce;
    [SerializeField] [ShowIf("needForce")] [AllowNesting] private ForceMode2D forceMode;
    [SerializeField] [ShowIf("needForce")] [AllowNesting] private float powerForce;

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

            if (needForce)
            {
                Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 direction = obj.transform.position - transform.position;
                    direction *= powerForce;

                    rb.AddForce(direction, forceMode);
                }
            }
        }
    }
}
