using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Configurations")]
    public Transform playerCamera;
    [Range(0.5f, 4)] public float cameraSensitivity = 1f;
    public bool cameraLocked = false;

    [Header("Orientation")]
    [SerializeField] Transform orientation;

    private float rotationX;
    private float rotationY;
    private Vector2 inputRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
        rotationY = transform.localRotation.eulerAngles.y;
        rotationX = playerCamera.localRotation.eulerAngles.x;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        inputRotation = context.ReadValue<Vector2>();
    }

    void Update()
    {
        if (cameraLocked)
            return;

        rotationX -= inputRotation.y * (cameraSensitivity / 10);
        rotationY += inputRotation.x * (cameraSensitivity / 10);

        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        orientation.transform.localRotation = Quaternion.Euler(0, rotationY, 0);
        
        playerCamera.localRotation = Quaternion.Euler(rotationX, rotationY, 0);
    }
}