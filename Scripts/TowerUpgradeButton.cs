using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerUpgradeButton : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler
{
    [SerializeField] int upgradeIndex;

    private Describable describable;
    private Tower selectedTower;
    private Image image;

    private void Awake()
    {
        describable = GetComponent<Describable>();
        image = GetComponent<Image>();
        Tower.OnSelect += SetUpgradeTower;
    }

    private void SetUpgradeTower(Tower tower)
    {
        selectedTower = tower;
        image.sprite = tower.towerScriptableObject.upgrades[upgradeIndex].icon;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (selectedTower == null || selectedTower.Equals(null)) return;
        if (upgradeIndex >= selectedTower.towerScriptableObject.upgrades.Count) return;

        var towerScriptableObject = selectedTower.towerScriptableObject.upgrades[upgradeIndex];
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
        if (selectedTower == null || selectedTower.Equals(null)) return;

        selectedTower.Upgrade(upgradeIndex);
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
