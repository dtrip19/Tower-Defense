using UnityEngine;
using System;

public class TowerPlacementManager : MonoBehaviour
{
    [SerializeField] GameObject towerObject;

    new private Camera camera;
    private GameObject ghostTowerObject;
    private Transform ghostTowerTransform;
    private Transform _transform;
    private int groundLayerMask = 1 << 8;
    private int unplaceableLayerMask = 1 << 9;
    private int mapColliderLayerMask = 1 << 10;
    private float ghostTowerSize;
    private bool towerCreatedLastFrame = false;

    public static event Action<TowerScriptableObject> OnPlace;

    private void Start()
    {
        camera = GetComponent<Camera>();
        _transform = transform;
        TowerSlot.OnSelect += CreateGhostTower;
    }

    private void Update()
    {
        if (ghostTowerObject == null) return;

        var ray = camera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit groundHit, 100, groundLayerMask);
        Physics.Raycast(ray, out RaycastHit unplaceableHit, 100, unplaceableLayerMask);
        if (groundHit.collider != null)
        {
            if (unplaceableHit.collider == null || Vector3.Distance(_transform.position, unplaceableHit.point) > Vector3.Distance(_transform.position, groundHit.point))
            {
                if (Physics.OverlapSphere(groundHit.point, ghostTowerSize, mapColliderLayerMask).Length <= 0)
                    ghostTowerTransform.position = groundHit.point;
            }
        }

        var ghostTower = ghostTowerObject.GetComponent<Tower>();

        if (Input.GetMouseButtonDown(0) && !towerCreatedLastFrame && PointTracker.Points >= ghostTower.towerScriptableObject.price)
        {
            OnPlace?.Invoke(ghostTower.towerScriptableObject);

            var mapColliderTransform = ghostTowerTransform.GetChild(0);
            mapColliderTransform.gameObject.layer = 10;
            mapColliderTransform.GetComponent<SphereCollider>().enabled = true;
            ghostTowerObject.GetComponent<TowerBehaviorBase>().canShoot = true;
            ghostTowerObject = null;
        }
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(ghostTowerObject);
        }

        towerCreatedLastFrame = false;
    }

    private void CreateGhostTower(TowerScriptableObject towerScriptableObject)
    {
        if (ghostTowerObject != null)
        {
            Destroy(ghostTowerObject);
        }
        towerCreatedLastFrame = true;
        var newTower = Instantiate(towerObject);
        newTower.GetComponent<Tower>().SetScriptableObject(towerScriptableObject);

        ghostTowerObject = newTower;
        ghostTowerTransform = newTower.transform;
        ghostTowerSize = towerScriptableObject.colliderSize;
    }    

    private void OnDestroy()
    {
        TowerSlot.OnSelect -= CreateGhostTower;
    }
}
