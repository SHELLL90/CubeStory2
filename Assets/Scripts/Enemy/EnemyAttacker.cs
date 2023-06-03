using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyLogic))]
public class EnemyAttacker : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float radiusAttack;
    [SerializeField] private float delayBetweenAttack;
    [SerializeField] private float delayBeforeAttack;

    private EnemyLogic _enemyLogic;

    private IEnumerator _attackingCor;

    private void Awake()
    {
        _enemyLogic = GetComponent<EnemyLogic>();
        _enemyLogic.ActionPlayerFinded += TryAttack;
    }

    private void TryAttack(PlayerHealth player)
    {
        StopCorAttacking();
        _attackingCor = Attacking(player);
        StartCoroutine(_attackingCor);
    }

    private IEnumerator Attacking(PlayerHealth player)
    {
        yield return new WaitForSeconds(delayBeforeAttack);
        while (player != null)
        {
            if (Vector2.Distance(player.transform.position, transform.position) <= radiusAttack)
            {
                _enemyLogic.ActionAttacking?.Invoke();
                _enemyLogic.ActionPlayAnimation?.Invoke(TypeAnimation.Attack);

                player.Damage(damage);
                yield return new WaitForSeconds(delayBetweenAttack);
            }
            yield return null;
        }
    }

    private void StopCorAttacking()
    {
        if (_attackingCor != null)
        {
            StopCoroutine(_attackingCor);
            _attackingCor = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusAttack);
    }
}
