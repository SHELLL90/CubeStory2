using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public enum TypeCollectedItem { Diamond, Heart }

[RequireComponent(typeof(Animator))]
public class CollectedItem : MonoBehaviour
{
    [SerializeField] private TypeCollectedItem typeCollectedItem;
    [SerializeField] [ShowIf("typeCollectedItem", TypeCollectedItem.Heart)] [AllowNesting] private float valueRegenHealth;
    [SerializeField] [ShowIf("typeCollectedItem", TypeCollectedItem.Diamond)] [AllowNesting] private int valueDiamond;
    [SerializeField] private string nameAnimationCollect;
    [SerializeField] private float timeToDestroyAfterCollect = 2.0f;

    private Animator _animator;
    private bool _canCollect = true;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Collect();
        }
    }

    private void Collect()
    {
        if (_canCollect)
        {
            bool collected = CollectedItemManager.Instance.Collect(typeCollectedItem, GetValueItem());

            if (collected)
            {
                _animator.Play(nameAnimationCollect);

                _canCollect = false;

                Invoke("Destroy", timeToDestroyAfterCollect);
            }
        }
    }

    private float GetValueItem()
    {
        float value = 0;
        if (typeCollectedItem == TypeCollectedItem.Heart) value = valueRegenHealth;
        else if (typeCollectedItem == TypeCollectedItem.Diamond) value = valueDiamond;

        return value;
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }
}
