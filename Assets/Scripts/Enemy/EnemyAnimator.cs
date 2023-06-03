using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeAnimation { Walk, Idle, Attack, Damage, Death }

[RequireComponent(typeof(EnemyLogic))]
[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private AnimationSetting[] animations;

    private EnemyLogic _enemyLogic;
    private Animator _animator;

    private void Awake()
    {
        _enemyLogic = GetComponent<EnemyLogic>();
        _animator = GetComponent<Animator>();

        _enemyLogic.ActionPlayAnimation += TryPlayAnimation;
    }

    public void TryPlayAnimation(TypeAnimation typeAnimation)
    {
        AnimationSetting animation = GetAnimation(typeAnimation);
        if (animation != null) PlayAnimation(animation);
    }

    private void PlayAnimation(AnimationSetting animation)
    {
        _animator.Play(animation.nameAnimation);
    }

    private AnimationSetting GetAnimation(TypeAnimation typeAnimation)
    {
        AnimationSetting animation = null;

        for (int i = 0; i < animations.Length; i++)
        {
            if (animations[i].typeAnimation == typeAnimation)
            {
                animation = animations[i];
                break;
            }
        }

        return animation;
    }
}
[System.Serializable]
public class AnimationSetting
{
    public TypeAnimation typeAnimation;
    public string nameAnimation;
}
