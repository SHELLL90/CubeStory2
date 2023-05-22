using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RigidbodyGravityScale : MonoBehaviour
{
    [SerializeField] private float gravityScaleValue = 1.0f;
    private void Awake()
    {
        GetComponent<Rigidbody2D>().gravityScale = gravityScaleValue;
    }
}
