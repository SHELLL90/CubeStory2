using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public static MainCamera Instance { get; set; }

    private Camera _camera;
    public Camera MainCam { get { return _camera; } }
    public Transform MainCamTransform { get { return _camera.transform; } }

    private void OnDestroy()
    {
        Instance = null;
    }
    private void Awake()
    {
        _camera = GetComponent<Camera>();
        Instance = this;
    }
}
