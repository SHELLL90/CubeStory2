using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    public static HealthBarManager Instance { get; set; }

    [SerializeField] private HealthBarUI prefabHealthBarUI;
    [SerializeField] private Transform parentHealthBarsUI;

    private PoolBase<HealthBarUI> _bars;

    private void Awake()
    {
        Instance = this;

        _bars = new PoolBase<HealthBarUI>(Preload, GetAction, ReturnAction, 2);
    }

    public void ReturnHealthBar(HealthBarUI healthBar)
    {
        _bars.Return(healthBar);
    }

    public HealthBarUI GetHealthBar()
    {
        return _bars.Get();
    }

    private HealthBarUI Preload()
    { 
        HealthBarUI healthBarUI = Instantiate(prefabHealthBarUI);

        healthBarUI.transform.parent = parentHealthBarsUI;
        healthBarUI.transform.SetSiblingIndex(0);

        return healthBarUI;
    }
    private void GetAction(HealthBarUI healthBar) => healthBar.gameObject.SetActive(true);
    private void ReturnAction(HealthBarUI healthBar) => healthBar.gameObject.SetActive(false);
}
