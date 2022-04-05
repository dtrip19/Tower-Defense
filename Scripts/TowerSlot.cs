using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class TowerSlot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TowerScriptableObject towerScriptableObject;

    private Describable describable;

    public static event Action<TowerScriptableObject> OnSelect;

    private void Awake()
    {
        describable = GetComponent<Describable>();
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
