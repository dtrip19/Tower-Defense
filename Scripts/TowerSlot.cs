using UnityEngine;
using UnityEngine.EventSystems;
using System;
public class TowerSlot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{


    [SerializeField] TowerScriptableObject towerScriptableObject;
    public static event Action<TowerScriptableObject> OnSelect;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnSelect?.Invoke(towerScriptableObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
}
