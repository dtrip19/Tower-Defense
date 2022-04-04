using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacementManager : MonoBehaviour
{

    [SerializeField] GameObject towerObject;
    Camera camera;
    GameObject ghostTower;
    int layerMask = 1 << 8;
    bool towerCreatedLastFrame = false;
    Transform ghostTowerTransform;
    float ghostTowerSize;

    void Start()
    {
        camera = GetComponent<Camera>();
        TowerSlot.OnSelect += CreateGhostTower;
    }

    // Update is called once per frame
    void Update()
    {
        if (ghostTower == null) return;

        var ray = camera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray,out RaycastHit hit, 100,layerMask);
        if (hit.collider!=null){
            if(Physics.OverlapSphere(hit.point, ghostTowerSize,1 << 9).Length <= 0)
                ghostTowerTransform.position = hit.point + new Vector3(0,0.5f,0);
        }
        
        if (Input.GetMouseButtonDown(0) && !towerCreatedLastFrame)
        {
            ghostTowerTransform.GetChild(0).gameObject.layer = 9;
            ghostTower.GetComponent<Tower>().canShoot = true;
            ghostTower=null;
        }
        
        towerCreatedLastFrame = false;


    }

    void CreateGhostTower(TowerScriptableObject towerScriptableObject)
    {
        if (this.ghostTower != null)
        {
            Destroy(this.ghostTower);
        }
        towerCreatedLastFrame = true;
        var newTower = Instantiate(towerObject);
        newTower.GetComponent<Tower>().SetScriptableObject(towerScriptableObject);
        this.ghostTower = newTower;
        ghostTowerTransform = newTower.transform;
        ghostTowerSize = towerScriptableObject.colliderSize;
    }    

    void OnDestroy()
    {
        TowerSlot.OnSelect -= CreateGhostTower;
    }

}
