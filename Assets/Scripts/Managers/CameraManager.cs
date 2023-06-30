using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float zoomSpeed = 10f;

    public Vector3 minBounds;
    public Vector3 maxBounds;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Camera movement
        float horizontalMovement = Input.GetAxis("Horizontal") * 10f;
        float verticalMovement = Input.GetAxis("Vertical") * 10f;
        float zoomInput = Input.GetAxis("Mouse ScrollWheel") * 100f;
        Vector3 newPosition = transform.position;
        newPosition.z -= horizontalMovement * moveSpeed * Time.deltaTime;
        newPosition.x += verticalMovement * moveSpeed * Time.deltaTime;
        newPosition.y -= zoomInput * moveSpeed * Time.deltaTime;
        transform.position = newPosition;
    }
}
