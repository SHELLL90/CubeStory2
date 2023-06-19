using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TypeDamageValue { Player, Enemy }
public class HealthChangeObj : MonoBehaviour
{
    [Header("Text Setting")]
    [SerializeField] private Text textValue;
    [SerializeField] private Color colorTextPlayerDamage;
    [SerializeField] private Color colorTextEnemyDamage;
    [Header("Other Setting")]
    [SerializeField] private float timeToHide = 2.0f;

    private RectTransform _rectTransform;

    public RectTransform ThisRectTransform
    {
        get 
        {
            if (_rectTransform == null) ThisRectTransform = GetComponent<RectTransform>();
            return _rectTransform;
        }
        private set { _rectTransform = value; }
    }

    public void SetData(float damage, TypeDamageValue typeDamage)
    {
        textValue.text = damage.ToString();

        if (TypeDamageValue.Player == typeDamage) textValue.color = colorTextPlayerDamage;
        else if (TypeDamageValue.Enemy == typeDamage) textValue.color = colorTextEnemyDamage;
    }
}
