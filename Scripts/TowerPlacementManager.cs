using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacementManager : MonoBehaviour
{

    Camera camera;
    GameObject ghostTower;
    int layerMask = 1 << 8;
    bool towerCreatedLastFrame = false;
    Transform ghostTowerTransform;

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
            ghostTowerTransform.position = hit.point + new Vector3(0,0.5f,0);
        }
        if (Input.GetMouseButtonDown(0) && !towerCreatedLastFrame)
        {
            ghostTower.GetComponent<Tower>().canShoot = true;
            ghostTower=null;
        }
        
        towerCreatedLastFrame = false;
    }

    void CreateGhostTower(GameObject ghostTower)
    {
        if (this.ghostTower != null)
        {
            Destroy(this.ghostTower);
        }
        towerCreatedLastFrame = true;
        var newTower = Instantiate(ghostTower);
        this.ghostTower = newTower;
        ghostTowerTransform = newTower.transform;
    }    

    void OnDestroy()
    {
        TowerSlot.OnSelect -= CreateGhostTower;
    }

}
