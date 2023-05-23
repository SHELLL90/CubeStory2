using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerForce))]
[RequireComponent(typeof(PlayerStates))]
public class AttackTarget : MonoBehaviour
{
    [Header("Search")]
    [SerializeField] private float radiusSearchTarget = 7.0f;
    [Header("Force")]
    [SerializeField] private PlayerForceSetting playerForceSetting;
    [Min(1)][SerializeField] private int maxNumberAttack;

    private float _delayBetweenAttack;

    public float DelayBetweenAttack
    {
        get { return _delayBetweenAttack; }
        private set { _delayBetweenAttack = value; }
    }

    private int _currentNumberAttack;

    public int CurrentNumberAttack
    {
        get { return _currentNumberAttack; }
        private set 
        {
            _currentNumberAttack = value;
            TryReload();
            Debug.Log("Current Number Attack " + _currentNumberAttack);
        }
    }


    private PlayerForce _playerForce;
    private PlayerStates _playerStates;
    private AttackTargetSearch _targetSearch;

    private System.Action ActionTryAttack;

    private bool _reloading = false;

    private void OnEnable()
    {
        InputManager.ActionAttack += TryAttack;
    }

    private void OnDisable()
    {
        InputManager.ActionAttack -= TryAttack;
    }

    private void Awake()
    {
        _playerForce = GetComponent<PlayerForce>();
        _playerStates = GetComponent<PlayerStates>();
        ActionTryAttack = TryAttackSearch;

        DelayBetweenAttack = playerForceSetting.delayBetweenForce;
        CurrentNumberAttack = maxNumberAttack;
    }

    private void TryAttack()
    {
        if (CurrentNumberAttack > 0)
        {
            ActionTryAttack?.Invoke();
        } 
    }

    private void TryAttackSearch()
    {
        DamageObjectCollision damageObjectCollision = SearchTarget();
        if (damageObjectCollision != null)
        {
            Attack(damageObjectCollision);
        }
    }

    private void TryAttackList()
    {
        if (_targetSearch.ObjectCollision != null)
        {
            Attack(_targetSearch.ObjectCollision);
        }
    }

    private void Attack(DamageObjectCollision damageObjectCollision)
    {
        playerForceSetting.directionForce = damageObjectCollision.transform.position - transform.position;
        _playerForce.Force(playerForceSetting);

        CurrentNumberAttack--;
    }

    private void TryReload()
    {
        if (!_reloading && CurrentNumberAttack < maxNumberAttack)
        {
            StartCoroutine(Reloading());
        }
    }

    private IEnumerator Reloading()
    {
        _reloading = true;
        Debug.Log("Start");
        float timeReloading = 0;
        float endTimeReloading = DelayBetweenAttack;
        while (timeReloading < endTimeReloading)
        {
            timeReloading += Time.deltaTime;
            float percent = timeReloading / endTimeReloading;
            _playerStates.ActionReloading?.Invoke(percent);

            yield return null;
        }
        EndReload();
    }

    private void EndReload()
    {
        Debug.Log("End");
        _reloading = false;
        CurrentNumberAttack++;
        TryReload();
    }

    public void SetTargetSearch(AttackTargetSearch targetSearch)
    {
        _targetSearch = targetSearch;
        ActionTryAttack = TryAttackList;
    }

    private DamageObjectCollision SearchTarget()
    {
        DamageObjectCollision damageObjectCollision = null;
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, radiusSearchTarget, Vector2.up);

        for (int i = 0; i < hits.Length; i++)
        {
            DamageObjectCollision temp = hits[i].transform.GetComponent<DamageObjectCollision>();
            if (temp != null)
            {
                if (damageObjectCollision == null)
                {
                    damageObjectCollision = temp;
                }
                else if (OtherHelper.GetDistance(damageObjectCollision.transform, transform) < OtherHelper.GetDistance(temp.transform, transform))
                {
                    damageObjectCollision = temp;
                }
            }
        }

        return damageObjectCollision;
    }
}
