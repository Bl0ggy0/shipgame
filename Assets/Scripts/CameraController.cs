using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float mouseFollowAmount = 2f;
    [SerializeField] private float smoothSpeed = 5f;

    private Camera cam;
    private float zPosition;

    private void Start()
    {
        cam = Camera.main;
        zPosition = transform.position.z;
    }

    private void LateUpdate()
    {
        if (player == null)
            return;

        if (Mouse.current == null)
            return;

        // Get mouse position using the NEW Input System
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();

        // Convert mouse position to world position
        Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(mouseScreenPosition);
        mouseWorldPosition.z = 0f;

        // Calculate offset from player toward mouse
        Vector3 mouseOffset = mouseWorldPosition - player.position;
        mouseOffset.z = 0f;

        // Limit camera movement toward mouse
        mouseOffset = Vector3.ClampMagnitude(mouseOffset, mouseFollowAmount);

        // Camera target
        Vector3 targetPosition = player.position + mouseOffset;
        targetPosition.z = zPosition;

        // Smooth movement
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );
    }
}