using UnityEngine;

public class TowerSelectionManager : MonoBehaviour
{
    private Camera mainCam;
    private Tower hoveredTower;

    private void Awake()
    {
        mainCam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (hoveredTower != null && !hoveredTower.Equals(null))
        {
            if (hoveredTower.isGhostTower)
                return;
        }

        var ray = mainCam.ScreenPointToRay(Input.mousePosition);
        bool hitTower = Physics.Raycast(ray, out RaycastHit hit, 100, Layers.Tower);
        if (!hitTower)
        {
            if (hoveredTower != null && !hoveredTower.Equals(null))
            {
                hoveredTower.MouseExit();
                hoveredTower = null;
            }
            return;
        }

        if (hit.collider.TryGetComponent(out Tower newTower))
            hoveredTower = hit.collider.GetComponent<Tower>();
        else
        {
            Debug.LogError("TowerSelectionManager hit object in tower layer with no Tower component");
            return;
        }

        hoveredTower.MouseEnter();

        if (Input.GetKeyDown(KeyCode.Mouse0))
            hoveredTower.MouseDown();
        else if (Input.GetKeyDown(KeyCode.Mouse1))
            hoveredTower.RightMouseDown();
    }
}
