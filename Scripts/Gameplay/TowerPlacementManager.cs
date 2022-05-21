using UnityEngine;
using System;

public class TowerPlacementManager : MonoBehaviour
{
    [SerializeField] GameObject towerPrefab;
    [SerializeField] Transform rangeIndicatorTransform;

    private Camera mainCam;
    private GameObject ghostTowerObject;
    private Transform ghostTowerTransform;
    private Transform _transform;
    private float ghostTowerSize;
    private bool towerCreatedLastFrame;

    public static event Action<int> OnPlace;

    private void Start()
    {
        mainCam = GetComponent<Camera>();
        _transform = transform;
        TowerSlot.OnSelect += CreateGhostTower;
        Tower.OnSelect += ShowRangeIndicator;
    }

    private void Update()
    {
        if (ghostTowerObject == null) return;

        var ray = mainCam.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit groundHit, 100, Layers.Ground);
        Physics.Raycast(ray, out RaycastHit unplaceableHit, 100, Layers.Unplaceable);
        if (groundHit.collider != null)
        {
            if (unplaceableHit.collider == null || Vector3.Distance(_transform.position, unplaceableHit.point) > Vector3.Distance(_transform.position, groundHit.point))
            {
                if (Physics.OverlapSphere(groundHit.point, ghostTowerSize, Layers.MapCollider).Length <= 0)
                {
                    ghostTowerTransform.position = groundHit.point;
                    rangeIndicatorTransform.position = groundHit.point;
                }
            }
        }

        var ghostTower = ghostTowerObject.GetComponent<Tower>();

        if (Input.GetMouseButtonDown(0) && !towerCreatedLastFrame && PointTracker.Points >= ghostTower.towerSO.price)
        {
            OnPlace?.Invoke(ghostTower.towerSO.price);

            var mapColliderTransform = ghostTowerTransform.GetChild(0);
            mapColliderTransform.gameObject.layer = 10;
            mapColliderTransform.GetComponent<SphereCollider>().enabled = true;
            ghostTowerObject.GetComponent<TowerBehaviorBase>().canShoot = true;
            var tower = ghostTowerObject.GetComponent<Tower>();
            tower.isGhostTower = false;
            tower.MouseDown();
            ghostTowerObject = null;
        }
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(ghostTowerObject);
            rangeIndicatorTransform.localScale = Vector3.zero;
        }

        towerCreatedLastFrame = false;
    }

    private void CreateGhostTower(TowerScriptableObject towerScriptableObject)
    {
        float scale = towerScriptableObject.range * 2;
        rangeIndicatorTransform.localScale = new Vector3(scale, scale, scale);
        if (ghostTowerObject != null)
        {
            Destroy(ghostTowerObject);
        }
        towerCreatedLastFrame = true;
        var newTower = Instantiate(towerPrefab);
        newTower.GetComponent<Tower>().SetScriptableObject(towerScriptableObject);

        ghostTowerObject = newTower;
        ghostTowerTransform = newTower.transform;
        ghostTowerSize = towerScriptableObject.colliderSize;
    }

    private void ShowRangeIndicator(Tower tower)
    {
        rangeIndicatorTransform.position = tower.transform.position;
        float scale = tower.towerSO.range * 2;
        rangeIndicatorTransform.localScale = new Vector3(scale, scale, scale);
    }

    private void OnDestroy()
    {
        TowerSlot.OnSelect -= CreateGhostTower;
    }
}
