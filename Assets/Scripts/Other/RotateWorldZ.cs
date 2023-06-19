using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWorldZ : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.up = Vector3.up;
    }
}
