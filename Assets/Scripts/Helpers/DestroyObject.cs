using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] private Object[] objs;

    public void DestroyObj()
    {
        for (int i = 0; i < objs.Length; i++)
        {
            Destroy(objs[i]);
        }
    }
}
