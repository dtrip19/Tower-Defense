using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float scrollSensitivity;
    [SerializeField] float xSensitivity;
    [SerializeField] float ySensitivity;
    [SerializeField] Transform cameraTransform;

    private Transform _transform;
    private Vector3 mousePrev;

    private void Awake()
    {
        _transform = transform;
        mousePrev = Input.mousePosition;
    }

    private void Update()
    {
        var mouseCurrent = Input.mousePosition;

        if (Input.GetMouseButton(1))
            Rotate(mouseCurrent);

        Zoom(Input.mouseScrollDelta);

        mousePrev = mouseCurrent;
    }

    private void Rotate(Vector3 mouseCurrent)
    {
        _transform.eulerAngles += new Vector3((mouseCurrent.y - mousePrev.y) * xSensitivity, (mouseCurrent.x - mousePrev.x) * ySensitivity, 0);
        float newX = Mathf.Clamp(_transform.eulerAngles.x - 360, -80, -5);
        _transform.eulerAngles = new Vector3(newX, _transform.eulerAngles.y, 0);
    }

    private void Zoom(Vector2 scrollDelta)
    {
        //var positionDelta = new Vector3(0, scrollDelta.y * scrollSensitivity, 0);
        //cameraTransform.localPosition += positionDelta;
        cameraTransform.localPosition = new Vector3(0, Mathf.Clamp(cameraTransform.localPosition.y + scrollDelta.y * scrollSensitivity, 20, 75));
    }
}
