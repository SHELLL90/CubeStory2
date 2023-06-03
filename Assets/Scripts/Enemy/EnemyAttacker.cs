using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public enum TypeAttack { Melee, Range }

[RequireComponent(typeof(EnemyLogic))]
public class EnemyAttacker : MonoBehaviour
{
    [SerializeField] private TypeAttack typeAttack;
    [SerializeField] [ShowIf("typeAttack", TypeAttack.Melee)] [AllowNesting] private float damage;
    [SerializeField] private float radiusAttack;
    [SerializeField] private float delayBetweenAttack;
    [SerializeField] private float delayBeforeAttack;
    [SerializeField] [ShowIf("typeAttack", TypeAttack.Range)] [AllowNesting] private Missile prefabMissile;
    [SerializeField] [ShowIf("typeAttack", TypeAttack.Range)] [AllowNesting] private Transform pointShoot;
    [SerializeField] [ShowIf("typeAttack", TypeAttack.Range)] [AllowNesting] private MissileSetting missileSetting;
    [SerializeField] [ShowIf("typeAttack", TypeAttack.Melee)] [AllowNesting] private bool forceTarget;
    [SerializeField] [ShowIf("forceTarget")] [AllowNesting] private ForceAttackSetting forceAttackSetting;

    #region Editor
    private void OnValidate()
    {
        if (typeAttack == TypeAttack.Range)
        {
            forceTarget = false;
        }
    }
    #endregion Editor

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

                if (typeAttack == TypeAttack.Melee)
                {
                    player.Damage(damage);
                }
                else if (typeAttack == TypeAttack.Range)
                {
                    Missile missile = Instantiate(prefabMissile);

                    missile.transform.position = pointShoot.position;
                    
                    missileSetting.target = player.transform;
                    missile.SetMissileSetting(missileSetting);
                }

                if (forceTarget)
                {
                    forceAttackSetting.ForceTarget(player.transform, transform.position);
                    //Rigidbody2D rigidbody = player.GetComponent<Rigidbody2D>();
                    //if (rigidbody != null)
                    //{
                    //    Vector2 direction = player.transform.position - transform.position;
                    //    direction = direction.normalized;
                    //    rigidbody.AddForce(direction * forceAttackSetting.powerForce, forceAttackSetting.forceMode);
                    //}
                }

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
[System.Serializable]
public class ForceAttackSetting
{
    public float powerForce;
    public ForceMode2D forceMode;

    public void ForceTarget(Transform target, Vector3 pos)
    {
        Debug.Log("Try force");
        Rigidbody2D rigidbody = target.GetComponent<Rigidbody2D>();
        if (rigidbody != null)
        {
            Debug.Log("force");
            Vector2 direction = target.position - pos;
            direction = direction.normalized;
            rigidbody.AddForce(direction * powerForce, forceMode);
        }
    }
}