using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacementManager : MonoBehaviour
{

    Camera camera;
    GameObject ghostTower;
    int layerMask = 1 << 3;
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
        Physics.Raycast(ray,out RaycastHit hit, layerMask);
        if (hit.collider!=null){
            print("d");
            ghostTowerTransform.position = hit.point;
        }

    }

    void CreateGhostTower(GameObject ghostTower)
    {
        var newTower = Instantiate(ghostTower);
        this.ghostTower = newTower;
        ghostTowerTransform = newTower.transform;
    }    

    void OnDestroy()
    {
        TowerSlot.OnSelect -= CreateGhostTower;
    }
}
