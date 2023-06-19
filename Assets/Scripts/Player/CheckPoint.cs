using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Vector2 positionCheckPoint;

    public Vector2 PositionCheckPoint { get { return positionCheckPoint; } }

    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
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
        Color color = Color.green;
        Gizmos.color = color;
        Gizmos.DrawWireSphere(positionCheckPoint, 0.5f);
    }
}
