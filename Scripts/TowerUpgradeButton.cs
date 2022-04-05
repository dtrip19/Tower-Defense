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
        Tower.OnSelect += SetUpgradeTower;
    }

    private void SetUpgradeTower(Tower tower)
    {
        towerScriptableObject = tower.towerScriptableObject.upgrades[upgradeIndex];
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

    public void OnPointerDown(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        describable.Uninspect();
    }

    private void OnDestroy()
    {
        Tower.OnSelect -= SetUpgradeTower;
    }
}
