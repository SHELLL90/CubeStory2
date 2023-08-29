using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CheckPoint : MonoBehaviour
{
    [Header("Position")]
    [SerializeField] private Vector2 positionCheckPoint;
    [Header("Size")]
    [SerializeField] private Vector2 sizeCollider;

    public Vector2 PositionCheckPoint { get { return (Vector2)transform.position + positionCheckPoint; } }

    private BoxCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        if (_collider == null) _collider = gameObject.AddComponent<BoxCollider2D>();

        _collider.size = sizeCollider;
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EnterZone();
        }
    }

    private void EnterZone()
    {
        CheckPointManager.Instance.SetCheckPoint(this);
        _collider.enabled = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(Color.green.r, Color.green.g, Color.green.b, 0.4f);
        Gizmos.DrawCube(transform.position, sizeCollider);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(PositionCheckPoint, 0.5f);
    }
}
