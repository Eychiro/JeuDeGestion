using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 10f;
    public Transform groundCheck;

    [SerializeField] private Transform Orientation;
    private Rigidbody rb;
    private Vector2 inputMovement;

    [SerializeField] private float _jumpForce = 2f;
    private bool isGrounded = true;
    private LayerMask _groundlayerMask;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
        _groundlayerMask = LayerMask.GetMask("Ground");
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        inputMovement = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {    
        if (isGrounded)
            rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
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
        if (BoutiqueDeGraines.IsShopOpen)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            return;
        }

        MovePlayer();

        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, 0.05f, _groundlayerMask);
    }
}
