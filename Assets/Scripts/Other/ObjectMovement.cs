using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ObjectMovement : MonoBehaviour
{
    [SerializeField] private MovementPoint[] movementPoints;
    [SerializeField] [ShowIf("_thereIsRigidbody")] [AllowNesting] private float distanceStopped;

    #region Editor

    private bool _thereIsRigidbody;
    private void OnValidate()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _thereIsRigidbody = _rigidbody != null;
    }

    #endregion Editor

    private MovementPoint _currentMovementPoint;
    private int _currentPointIndex;

    private bool _canMovement;
    private bool _tempCanMovement;

    private IEnumerator _movementToTargetCor;

    private Rigidbody2D _rigidbody;
    private bool _ignoteY;

    public System.Action<bool> IsMoving { get; set; }

    private void Awake()
    {
        SwitchMovementPoint(0);
        CanMovement(true);

        _rigidbody = GetComponent<Rigidbody2D>();
        if (_rigidbody == null) distanceStopped = 0;
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (!_canMovement) return;

        Vector2 targetPosition = _currentMovementPoint.worldPosition;
        float speed = _currentMovementPoint.speed;

        Movement(targetPosition, speed);
        if (Vector2.Distance(targetPosition, (Vector2)transform.position) <= distanceStopped)
        {
            SwitchMovementPoint();
        }
    }

    private void Movement(Vector2 targetPosition, float speed)
    {
        if (_rigidbody == null) transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        else
        {
            Vector2 direction = targetPosition - (Vector2)transform.position;
            direction = direction.normalized;
            Vector3 velocity = direction * speed;
            if (_ignoteY) velocity = new Vector3(velocity.x, _rigidbody.velocity.y, velocity.z);
            _rigidbody.velocity = velocity;
        }
    }


    public void Movement(MovementTarget movementTarget)
    {
        StopCorMovementTarget();

        _movementToTargetCor = MovementCor(movementTarget);
        StartCoroutine(_movementToTargetCor);
    }

    private IEnumerator MovementCor(MovementTarget movementTarget)
    {
        _tempCanMovement = _canMovement;
        _canMovement = false;
        _ignoteY = movementTarget.ignoreY;
        while (Vector2.Distance(transform.position, movementTarget.transformTarget.position) > movementTarget.stoppingDistance)
        {
            Movement(movementTarget.transformTarget.position, movementTarget.speed);
            yield return null;
        }

        _canMovement = _tempCanMovement;
    }

    public void StopCorMovementTarget()
    {
        if (_movementToTargetCor != null)
        {
            StopCoroutine(_movementToTargetCor);
            _movementToTargetCor = null;

            _canMovement = _tempCanMovement;
        }
    }

    private void SwitchMovementPoint()
    {
        _currentPointIndex++;
        if (_currentPointIndex >= movementPoints.Length)
        {
            _currentPointIndex = 0;
        }

        SwitchMovementPoint(_currentPointIndex);
    }

    public void CanMovement(bool can)
    {
        _canMovement = can;
        IsMoving?.Invoke(_canMovement);
    }

    private void SwitchMovementPoint(int index)
    {
        _currentMovementPoint = movementPoints[index];
        _currentPointIndex = index;
    }
    [Button]
    public void ResetPositionPoints()
    {
        for (int i = 0; i < movementPoints.Length; i++)
        {
            movementPoints[i].worldPosition = transform.position;
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (movementPoints == null) return;
        for (int i = 0; i < movementPoints.Length; i++)
        {
            Color color = Color.blue;
            Gizmos.color = color;
            Gizmos.DrawWireSphere(movementPoints[i].worldPosition, 0.5f);
        }

    }
}
[System.Serializable]
public class MovementPoint
{
    public Vector2 worldPosition;
    public float speed = 5.0f;
}

[System.Serializable]
public class MovementTarget
{
    [HideInInspector] public Transform transformTarget;
    public bool ignoreY;
    public float speed;
    public float stoppingDistance;
}
