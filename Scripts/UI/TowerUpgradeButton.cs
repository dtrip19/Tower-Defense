using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerUpgradeButton : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler
{
    [SerializeField] int upgradeIndex;

    private Describable describable;
    private Tower selectedTower;
    private Image image;

    public static event Action<int> OnUpgrade;

    private void Awake()
    {
        describable = GetComponent<Describable>();
        image = transform.GetChild(0).GetComponent<Image>();
        Tower.OnSelect += SetUpgradeTower;
    }

    private void SetUpgradeTower(Tower tower)
    {
        selectedTower = tower;

        if (tower.towerSO.upgrades.Count != 0)
        {
            image.sprite = tower.towerSO.upgrades[upgradeIndex].icon;
            image.color = Color.white;
        }
        else
        {
            image.sprite = null;//Insert empty upgrade slot image
            image.color = Color.clear;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (selectedTower == null || selectedTower.Equals(null)) return;
        if (upgradeIndex >= selectedTower.towerSO.upgrades.Count) return;

        var towerSO = selectedTower.towerSO.upgrades[upgradeIndex];

        describable.Inspect(UI.GetTowerDataFromSO(towerSO));
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (selectedTower == null || selectedTower.Equals(null) || PointTracker.Points < selectedTower.towerSO.price) return;

        selectedTower.Upgrade(upgradeIndex);
        OnUpgrade?.Invoke(selectedTower.towerSO.price);
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
