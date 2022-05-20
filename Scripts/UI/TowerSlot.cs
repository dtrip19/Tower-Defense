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
        describable.Inspect(UI.GetTowerDataFromSO(towerSO));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        describable.Uninspect();
    }
}
