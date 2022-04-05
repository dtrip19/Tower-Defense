using UnityEngine;

public class TowerPlacementManager : MonoBehaviour
{
    [SerializeField] GameObject towerObject;

    new private Camera camera;
    private GameObject ghostTower;
    private Transform ghostTowerTransform;
    private int layerMask = 1 << 8;
    private float ghostTowerSize;
    private bool towerCreatedLastFrame = false;

    private void Start()
    {
        camera = GetComponent<Camera>();
        TowerSlot.OnSelect += CreateGhostTower;
    }

    private void Update()
    {
        if (ghostTower == null) return;

        var ray = camera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray,out RaycastHit hit, 100,layerMask);
        if (hit.collider != null){
            if(Physics.OverlapSphere(hit.point, ghostTowerSize, 1 << 9).Length <= 0)
                ghostTowerTransform.position = hit.point + new Vector3(0, 0.5f, 0);
        }
        
        if (Input.GetMouseButtonDown(0) && !towerCreatedLastFrame)
        {
            ghostTowerTransform.GetChild(0).gameObject.layer = 9;
            ghostTower.GetComponent<Tower>().canShoot = true;
            ghostTower = null;
        }
        
        towerCreatedLastFrame = false;
    }

    private void CreateGhostTower(TowerScriptableObject towerScriptableObject)
    {
        if (ghostTower != null)
        {
            Destroy(ghostTower);
        }
        towerCreatedLastFrame = true;
        var newTower = Instantiate(towerObject);
        newTower.GetComponent<Tower>().SetScriptableObject(towerScriptableObject);

        ghostTower = newTower;
        ghostTowerTransform = newTower.transform;
        ghostTowerSize = towerScriptableObject.colliderSize;
    }    

    private void OnDestroy()
    {
        TowerSlot.OnSelect -= CreateGhostTower;
    }

}
