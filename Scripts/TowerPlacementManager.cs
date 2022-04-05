using UnityEngine;
using System;
public class TowerPlacementManager : MonoBehaviour
{
    [SerializeField] GameObject towerObject;

    new private Camera camera;
    private GameObject ghostTowerObject;
    private Transform ghostTowerTransform;
    private int layerMask = 1 << 8;
    private float ghostTowerSize;
    private bool towerCreatedLastFrame = false;
    public static event Action<TowerScriptableObject> OnPlace;
    private void Start()
    {
        camera = GetComponent<Camera>();
        TowerSlot.OnSelect += CreateGhostTower;
    }

    private void Update()
    {
        if (ghostTowerObject == null) return;

        var ray = camera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray,out RaycastHit hit, 100,layerMask);
        if (hit.collider != null){
            if(Physics.OverlapSphere(hit.point, ghostTowerSize, 1 << 9).Length <= 0)
                ghostTowerTransform.position = hit.point + new Vector3(0, 0.5f, 0);
        }

        var ghostTower = ghostTowerObject.GetComponent<Tower>();

        if (Input.GetMouseButtonDown(0) && !towerCreatedLastFrame && MoneyTracker.Points >= ghostTower.towerScriptableObject.price)
        {
            OnPlace?.Invoke(ghostTower.towerScriptableObject);

            ghostTowerTransform.GetChild(0).gameObject.layer = 9;
            ghostTowerObject.GetComponent<Tower>().canShoot = true;
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
