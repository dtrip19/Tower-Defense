using UnityEngine;
using UnityEngine.EventSystems;

public class TowerUpgradeButton : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler
{
    [SerializeField] int upgradeIndex;

    private Describable describable;
    private TowerScriptableObject towerScriptableObject;

    private void Awake()
    {
        describable = GetComponent<Describable>();
        TowerSlot.OnSelect += SetUpgradeTower;
    }

    private void SetUpgradeTower(TowerScriptableObject towerScriptableObject)
    {
        this.towerScriptableObject = towerScriptableObject;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    private void OnDestroy()
    {
        TowerSlot.OnSelect -= SetUpgradeTower;
    }
}
