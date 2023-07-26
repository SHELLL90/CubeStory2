using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemySearchPlayer : MonoBehaviour
{
    [SerializeField] private float radius = 3.0f;
    [SerializeField] private float delaySearch = 5.0f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private EnemyLogic enemyLogic;
    

    public System.Action<PlayerHealth> ActionPlayerFinded { get; set; }

    private CircleCollider2D _circleCollider;
    private PlayerHealth _player;

    private float _timeCanNextSearch;

    private void Awake()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
        _circleCollider.isTrigger = true;
        _circleCollider.radius = radius;
    }

    private void Update()
    {
        if (_timeCanNextSearch < Time.time)
        {
            enemyLogic.ActionPlayerFinded?.Invoke(_player);
            _timeCanNextSearch = Time.time + delaySearch;
        }
    }

    private void Search()
    {
        if (_timeCanNextSearch > Time.time) return;
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, radius, transform.up, radius, layerMask);
        bool finded = false;

        for (int i = 0; i < hits.Length; i++)
        {
            PlayerHealth temp = hits[i].transform.GetComponent<PlayerHealth>();
            if (temp != null)
            {
                _player = temp;
                finded = true;

                break;
            }
        }

        if (!finded) _player = null;
        enemyLogic.ActionPlayerFinded?.Invoke(_player);

        _timeCanNextSearch = Time.time + delaySearch;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth health = collision.GetComponent<PlayerHealth>();
        if (health != null)
        {
            _player = health;
            enemyLogic.ActionPlayerFinded?.Invoke(_player);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerHealth health = collision.GetComponent<PlayerHealth>();
        if (health != null)
        {
            _player = null;
            enemyLogic.ActionPlayerFinded?.Invoke(_player);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
