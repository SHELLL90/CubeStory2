using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class AttackTargetSearch : MonoBehaviour
{
    [SerializeField] private float radiusSearch;
    [SerializeField] private LayerMask layerMaskRaycast;

    public DamageObjectCollision ObjectCollision { get; private set; }

    private List<DamageObjectCollision> _objects = new List<DamageObjectCollision>();

    private CircleCollider2D _circleCollider;
    private PlayerStates _playerStates;

    private int _frames = 0;
    private void Start()
    {
        GetComponentInParent<AttackTarget>().SetTargetSearch(this);
        _playerStates = GetComponentInParent<PlayerStates>();

        _circleCollider = GetComponent<CircleCollider2D>();
        _circleCollider.radius = radiusSearch;
        _circleCollider.isTrigger = true;
    }

    private void FixedUpdate()
    {
        _frames++;
        if (_frames % 5 == 0)
        {
            CheckCanAttack();
        }
    }

    private void CheckCanAttack()
    {
        ObjectCollision = null;
        float distance = radiusSearch * 10;
        for (int i = 0; i < _objects.Count; i++)
        {
            float tempDistance = Vector2.Distance(transform.position, _objects[i].transform.position);
            if (tempDistance < distance)
            {
                if (OtherHelper.CheckAvailability(transform.position, _objects[i].gameObject, layerMaskRaycast))
                {
                    ObjectCollision = _objects[i];
                    distance = tempDistance;
                }
            }
        }

        if (ObjectCollision != null)
        {
            _playerStates.TargetSearched = true;
        } 
        else
        {
            _playerStates.TargetSearched = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageObjectCollision obj = collision.GetComponent<DamageObjectCollision>();
        if (obj)
        {
            _objects.Add(obj);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        DamageObjectCollision obj = collision.GetComponent<DamageObjectCollision>();
        if (obj)
        {
            _objects.Remove(obj);
        }
    }
}
