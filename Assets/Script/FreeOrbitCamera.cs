using UnityEngine;

public class FreeOrbitCamera : MonoBehaviour
{
    public GameObject target; // The target GameObject to orbit
    public float rotateSpeed = 2.0f; // Speed of rotation
    public float zoomSpeed = 2.0f; // Speed of zooming
    public float minZoom = 1.0f; // Minimum zoom distance
    public float maxZoom = 20.0f; // Maximum zoom distance
    public float smoothness = 0.1f; // Smoothing factor for Lerp

    private float currentZoom; // Current zoom level
    private float targetZoom; // Target zoom level
    private Vector3 targetPosition; // Target position for the camera
    
    private void Start()
    {
        if (target == null)
        {
            Debug.LogWarning("Target for FreeOrbitCamera not set. Using the camera's position instead.");
            target = this.gameObject;
        }

        // Initialize distance based on current distance to target
        currentZoom = Vector3.Distance(transform.position, target.transform.position);
        targetZoom = currentZoom;
    }

    private void Update()
    {
        // Zoom the camera with W & S keys or mouse wheel
        float zoomInput = Input.GetAxis("Vertical") + Input.GetAxis("Mouse ScrollWheel");
        targetZoom -= zoomInput * zoomSpeed;
        targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);

        // Apply smoothness to zoom
        currentZoom = Mathf.Lerp(currentZoom, targetZoom, smoothness);

        // Rotate the camera with A & D keys or mouse
        float horizontalRotation = Input.GetAxis("Horizontal");

        if (Input.GetMouseButton(0)) // Left mouse button held
        {
            horizontalRotation = Input.GetAxis("Mouse X");
        }

        // Calculate the new target position
        Vector3 direction = (transform.position - target.transform.position).normalized;
        Quaternion rotation = Quaternion.Euler(0, horizontalRotation * rotateSpeed, 0);
        targetPosition = target.transform.position + rotation * direction * currentZoom;

        // Apply smoothness to position
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness);

        // Make sure the camera is always looking at the target
        transform.LookAt(target.transform);
    }
}
