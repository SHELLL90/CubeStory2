using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSetter : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite defalutSkin;
    [SerializeField] private Sprite newSkin;

    private bool _defaultSkin = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Sprite sprite = defalutSkin;
            if (_defaultSkin) sprite = newSkin;

            MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
            materialPropertyBlock.SetTexture("_MainTex", sprite.texture);
            spriteRenderer.SetPropertyBlock(materialPropertyBlock);

            _defaultSkin = !_defaultSkin;
        }
    }
}
