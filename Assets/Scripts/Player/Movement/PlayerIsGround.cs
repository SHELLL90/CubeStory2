using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStates))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerIsGround : MonoBehaviour
{
    [SerializeField] private RaycastIsGroundSetting downRaycast;
    [SerializeField] private RaycastIsGroundSetting upRaycast;
    [SerializeField] private RaycastIsGroundSetting leftRaycast;
    [SerializeField] private RaycastIsGroundSetting rightRaycast;
    [Header("Other")]
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private bool debug;

    private PlayerStates _playerStates;

    private void Awake()
    {
        _playerStates = GetComponent<PlayerStates>();
    }

    private void FixedUpdate()
    {
        Check();
    }

    private void Check()
    {
        _playerStates.IsGroundDown = GetBoolIsGround(downRaycast);
        _playerStates.IsGroundUp = GetBoolIsGround(upRaycast);
        _playerStates.IsGroundLeft = GetBoolIsGround(leftRaycast);
        _playerStates.IsGroundRight = GetBoolIsGround(rightRaycast);
    }

    private bool GetBoolIsGround(RaycastIsGroundSetting setting)
    {
        bool isGround = false;

        for (int i = 0; i < setting.pointsRaycast.Length; i++)
        {
            if (Raycast(setting.pointsRaycast[i], setting.directionRaycast, setting.lengthRaycast))
            {
                isGround = true;
                break;
            }
        }

        return isGround;
    }

    private bool Raycast(Transform point, Vector2 direction, float lengthRaycast)
    {
        bool isHit = false;
        direction = transform.TransformDirection(direction);

        if (Physics2D.Raycast(point.position, direction, lengthRaycast, layerMask.value))
        {
            isHit = true;
        }

#if UNITY_EDITOR
        if (debug)
        {
            if (isHit)
            {
                Debug.DrawRay(point.position, direction * lengthRaycast, Color.green);
            }
            else
            {
                Debug.DrawRay(point.position, direction * lengthRaycast, Color.red);
            }
            
        }
#endif

        return isHit;
    }
}

[System.Serializable]
public class RaycastIsGroundSetting
{
    public Transform[] pointsRaycast;
    public Vector2 directionRaycast;
    public float lengthRaycast = 0.2f;
}
