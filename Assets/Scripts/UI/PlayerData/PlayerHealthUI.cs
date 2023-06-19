using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [Header("Speed Change")]
    [SerializeField] private float speedChange = 1.0f;
    [Header("Animation")]
    [SerializeField] private string nameAnimationChangeHP;

    public static PlayerHealthUI Instance { get; private set; }

    private Animator _animator;

    private void OnEnable()
    {
        Instance = this;
    }

    private void OnDisable()
    {
        Instance = null;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void UpdateHealthBar(float value)
    {
        _animator.Play(nameAnimationChangeHP);

        StopAllCoroutines();
        StartCoroutine(ChangeAmount(value));
    }

    private IEnumerator ChangeAmount(float value)
    {
        while (healthBar.fillAmount != value)
        {
            healthBar.fillAmount = Mathf.MoveTowards(healthBar.fillAmount, value, Time.deltaTime * speedChange);
            yield return null;
        }
    }
}
