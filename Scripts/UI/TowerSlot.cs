using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class TowerSlot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TowerScriptableObject towerSO;

    private Describable describable;

    public static event Action<TowerScriptableObject> OnSelect;

    private void Awake()
    {
        describable = GetComponent<Describable>();
        transform.GetChild(0).GetComponent<Image>().sprite = towerSO.icon;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnSelect?.Invoke(towerSO);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var towerData = new TowerData
        {
            description = towerSO.description,
            price = towerSO.price,
            damage = towerSO.damage,
            attackDelay = towerSO.attackDelay,
            pierce = towerSO.pierce,
            bulletSpeed = towerSO.bulletSpeed,
            lifeTime = towerSO.lifeTime,
            range = towerSO.range,
            size = towerSO.colliderSize
        };

        describable.Inspect(towerData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        describable.Uninspect();
    }
}
