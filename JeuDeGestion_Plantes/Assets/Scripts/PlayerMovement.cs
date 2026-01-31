using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 10f;

    [SerializeField] Transform Orientation;
    private Rigidbody rb;
    private Vector2 inputMovement;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        inputMovement = context.ReadValue<Vector2>();
    }

    public void MovePlayer()
    {
        // On calcule la direction souhaitée
        Vector3 targetMove = (Orientation.forward * inputMovement.y) + (Orientation.right * inputMovement.x);
        
        // On multiplie par la vitesse
        Vector3 velocity = targetMove * movementSpeed;

        // IMPORTANT : On conserve la vitesse verticale actuelle (gravité/saut)
        velocity.y = rb.linearVelocity.y;

        // On applique le tout au Rigidbody
        rb.linearVelocity = velocity;
    }

    void FixedUpdate()
    {
        MovePlayer();
    }
}
