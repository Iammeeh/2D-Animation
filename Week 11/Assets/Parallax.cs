using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("Depth Settings")]
    [Tooltip("1 = Locked to camera (Sky), 0.5 = Medium distance (Mountains), 0.1 = Close (Trees)")]
    [Range(0f, 1f)]
    [SerializeField] private float parallaxFactor = 0.5f;

    [SerializeField] private bool lockVertical = true;

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    private void Start()
    {
        if (Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
            lastCameraPosition = cameraTransform.position;
        }
    }

    private void LateUpdate()
    {
        if (cameraTransform == null) return;

        // 1. Measure how far the camera moved since the last frame
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

        // 2. Move this background layer by a fraction of the camera's movement
        float moveX = deltaMovement.x * parallaxFactor;
        float moveY = lockVertical ? 0f : deltaMovement.y * parallaxFactor;

        transform.position += new Vector3(moveX, moveY, 0f);

        // 3. Remember the camera's new position for the next frame
        lastCameraPosition = cameraTransform.position;
    }
}
