using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Mouvement")]
    public float movementSpeed = 10f;
    public List<SkillData> skillBonus;
    public static bool canMove = true;
    [SerializeField] private Transform Orientation;
    
    [Header("Saut & Sol")]
    public Transform groundCheck;
    [SerializeField] private float _jumpForce = 2f;
    private bool isGrounded = true;
    private LayerMask _groundlayerMask;

    [Header("Détection de Pente")]
    [SerializeField] private float maxSlopeAngle = 45f;
    [SerializeField] private float detectionDistance = 0.5f;
    [SerializeField] private float rayLength = 1.5f;

    private Rigidbody rb;
    private Vector2 inputMovement;
    private float bonusSpeed = 1f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _groundlayerMask = LayerMask.GetMask("Ground");
    }

    void Start()
    {
        foreach(SkillData skill in skillBonus)
        {
            if (skill.estDebloquee)
            {
                bonusSpeed += skill.valeurBonus - 1f;
            }
        }
        movementSpeed *= bonusSpeed;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        inputMovement = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {    
        if (isGrounded && canMove)
            rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    private bool CheckSlope()
    {
        Vector3 moveDir = (Orientation.forward * inputMovement.y) + (Orientation.right * inputMovement.x).normalized;

        if (inputMovement.magnitude < 0.1f)
            return true;

        Vector3 rayOrigin = transform.position + (moveDir * detectionDistance) + Vector3.up;
        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, rayLength, _groundlayerMask))
        {
            float angle = Vector3.Angle(hit.normal, Vector3.up);
            
            Debug.DrawRay(rayOrigin, Vector3.down * rayLength, angle > maxSlopeAngle ? Color.red : Color.green);

            if (angle > maxSlopeAngle)
                return false;
        }

        return true;
    }

    public void MovePlayer()
    {
        Vector3 targetMove = (Orientation.forward * inputMovement.y) + (Orientation.right * inputMovement.x);
        Vector3 velocity = targetMove * movementSpeed;

        velocity.y = rb.linearVelocity.y;
        rb.linearVelocity = velocity;
    }

    void FixedUpdate()
    {
        bool slopeOK = CheckSlope();

        if (BoutiqueDeGraines.IsShopOpen || !canMove || !slopeOK)
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
            rb.angularVelocity = Vector3.zero;
            return;
        }

        MovePlayer();

        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, 0.15f, _groundlayerMask);
    }
}