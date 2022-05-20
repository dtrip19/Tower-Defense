using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float cameraDistanceLimit;
    [SerializeField] float scrollSensitivity;
    [SerializeField] float xSensitivity;
    [SerializeField] float ySensitivity;
    [SerializeField] Transform cameraTransform;

    private Transform _transform;
    private float startingDistance;
    //private Vector3 mousePrev;
    private const float maxAngleX = -1;
    private const float minAngleX = -70;
    private const float maxAngleY = 45;
    private const float minAngleY = -45;

    private void Awake()
    {
        _transform = transform;
        startingDistance = cameraTransform.localPosition.y;
        //mousePrev = Input.mousePosition;
    }

    private void Update()
    {
        var mouseCurrent = Input.mousePosition;

        Rotate(mouseCurrent);

        Zoom(Input.mouseScrollDelta);

        //mousePrev = mouseCurrent;
    }

    private void Rotate(Vector3 mouseCurrent)
    {
        float deltaTime = Time.deltaTime;

        float currentY = _transform.eulerAngles.y;
        if (currentY > 180)
            currentY -= 360;
        if (currentY < 45 && Input.GetKey(KeyCode.A))
            _transform.eulerAngles += new Vector3(0, xSensitivity * deltaTime, 0);
        if (currentY > -45 && Input.GetKey(KeyCode.D))
            _transform.eulerAngles -= new Vector3(0, xSensitivity * deltaTime, 0);

        float currentX = _transform.eulerAngles.x;
        if (currentX > 180)
            currentX -= 360;
        if (currentX < -5 && Input.GetKey(KeyCode.W))
            _transform.eulerAngles += new Vector3(ySensitivity * deltaTime, 0, 0);
        if (currentX > -70 && Input.GetKey(KeyCode.S))
            _transform.eulerAngles -= new Vector3(ySensitivity * deltaTime, 0, 0);

        //_transform.eulerAngles += new Vector3((mouseCurrent.y - mousePrev.y) * xSensitivity, (mouseCurrent.x - mousePrev.x) * ySensitivity, 0);
        //float newX = Mathf.Clamp(_transform.eulerAngles.x - 360, -80, -5);
        //_transform.eulerAngles = new Vector3(newX, _transform.eulerAngles.y, 0);
    }

    private void Zoom(Vector2 scrollDelta)
    {
        float newY = Mathf.Clamp(
            cameraTransform.localPosition.y + scrollDelta.y * scrollSensitivity,
            startingDistance - cameraDistanceLimit,
            startingDistance + cameraDistanceLimit);
        cameraTransform.localPosition = new Vector3(0, newY, 0);
    }
}
