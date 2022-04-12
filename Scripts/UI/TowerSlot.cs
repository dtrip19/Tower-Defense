using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class TowerSlot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TowerScriptableObject towerScriptableObject;

    private Describable describable;

    public static event Action<TowerScriptableObject> OnSelect;

    private void Awake()
    {
        describable = GetComponent<Describable>();
        transform.GetChild(0).GetComponent<Image>().sprite = towerScriptableObject.icon;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnSelect?.Invoke(towerScriptableObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var towerData = new TowerData
        {
            bulletSpeed = towerScriptableObject.bulletSpeed,
            damage = towerScriptableObject.damage,
            lifeTime = towerScriptableObject.lifeTime,
            attackDelay = towerScriptableObject.attackDelay,
            description = towerScriptableObject.description
        };

        describable.Inspect(towerData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        describable.Uninspect();
    }
}
