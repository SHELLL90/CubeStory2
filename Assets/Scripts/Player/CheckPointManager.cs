using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public static CheckPointManager Instance { get; private set; }

    private void OnEnable()
    {
        Instance = this;
    }

    private void OnDisable()
    {
        Instance = null;
    }

    private CheckPoint _currentCheckPoint;

    public void SetCheckPoint(CheckPoint checkPoint)
    {
        _currentCheckPoint = checkPoint;
    }

    public Vector2 GetPositionCheckPoint()
    {
        if (_currentCheckPoint == null) return PlayerComponents.Health.transform.position;
        return _currentCheckPoint.PositionCheckPoint;
    }
}
