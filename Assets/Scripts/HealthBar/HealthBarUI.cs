using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image bar;

    private RectTransform _rectTransform;
    private HealthBarObject _barObject;

    private Transform _target;
    private Vector3 _offset;
    private Vector3 _defaultScale;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _defaultScale = _rectTransform.localScale;
    }

    private void LateUpdate()
    {
        CalculationPosition();
    }

    public void SetBar(Transform transformObject, Vector3 offset, float defaultMultiplyScale, HealthBarObject healthBarObject, float currentHealth, float maxHealth)
    {
        _target = transformObject;
        _offset = offset;
        _barObject = healthBarObject;

        _rectTransform.localScale = _defaultScale * defaultMultiplyScale;

        bar.fillAmount = currentHealth / maxHealth;

        CalculationPosition();
    }

    private void CalculationPosition()
    {
        if (_target != null)
        {
            Vector3 targetPos = _target.position + _offset;
            Vector3 position = MainCamera.Instance.MainCam.WorldToScreenPoint(targetPos);

            _rectTransform.position = position;
        }
    }
}
