using UnityEngine;
using UnityEngine.EventSystems;
using System;
public class TowerSlot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] GameObject tower;
    public static event Action<GameObject> OnSelect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnSelect?.Invoke(tower);
        print("clicked on the tower slot");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        print("mouse entered the tower slot image");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        print("mouse exit");
    }
}
