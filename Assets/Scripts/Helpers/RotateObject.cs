using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        Vector3 eulers = new Vector3(0, 0, speed);
        transform.Rotate(eulers * Time.deltaTime);
    }
}
