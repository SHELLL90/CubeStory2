using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[RequireComponent(typeof(EnemyLogic))]
public class EnemyMovement : ObjectMovement
{
    [SerializeField] private bool moveToTarget;
    [SerializeField] [ShowIf("moveToTarget")] [AllowNesting] private MovementTarget movementTargetSetting;
    [SerializeField] [ShowIf("moveToTarget")] [AllowNesting] private float delay = 10.0f;

    private EnemyLogic _enemyLogic;

    private float _canNextMoveToTarget;
    private void Start()
    {
        _enemyLogic = GetComponent<EnemyLogic>();
        base.IsMoving += IsMovingEnemy;

        _enemyLogic.ActionPlayerFinded += PlayerFinded;
    }

    private void IsMovingEnemy(bool isMoving)
    {
        _enemyLogic.ActionMovement?.Invoke(isMoving);
    }

    private void PlayerFinded(PlayerHealth player)
    {
        if (player != null)
        {
            if (_canNextMoveToTarget <= Time.time)
            {
                movementTargetSetting.transformTarget = player.transform;
                base.Movement(movementTargetSetting);

                _canNextMoveToTarget = Time.time + delay;
            }
        }
        else
        {
            base.StopCorMovementTarget();
        }
    }
}
