using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    public System.Action ActionHitEnemy { get; set; }
    public System.Action<float, float> ActionChangeHealth { get; set; }
    public System.Action ActionAttacking { get; set; }
    public System.Action<bool> ActionMovement { get; set; }
    public System.Action<MovementTarget> ActionMovementTarget { get; set; }
    public System.Action<PlayerHealth> ActionPlayerFinded { get; set; }
}
