using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthChangeObjManager : MonoBehaviour
{
    public static HealthChangeObjManager Instance { get; set; }

    [SerializeField] private HealthChangeObj damageValuePrefab;
    [SerializeField] private Transform parentObjects;

    private PoolBase<HealthChangeObj> _bars;

    private void Awake()
    {
        Instance = this;

        _bars = new PoolBase<HealthChangeObj>(Preload, GetAction, ReturnAction, 5);
    }

    public void SetObjDamageValue(Vector3 worldPosition, float valueDamage, TypeDamageValue typeDamage)
    {
        HealthChangeObj obj = Get();
        obj.ThisRectTransform.position = MainCamera.Instance.MainCam.WorldToScreenPoint(worldPosition);

        obj.SetData(valueDamage, typeDamage);
    }

    public void Return(HealthChangeObj obj)
    {
        _bars.Return(obj);
    }

    public HealthChangeObj Get()
    {
        return _bars.Get();
    }

    private HealthChangeObj Preload()
    {
        HealthChangeObj obj = Instantiate(damageValuePrefab);

        obj.transform.parent = parentObjects;
        obj.transform.SetSiblingIndex(0);

        return obj;
    }
    private void GetAction(HealthChangeObj obj) => obj.gameObject.SetActive(true);
    private void ReturnAction(HealthChangeObj obj) => obj.gameObject.SetActive(false);
}
