using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
   [SerializeField] private Transform cameraTransform;
    [SerializeField] private bool faceMoveDirection;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;

    private Animator animator;

    private Rigidbody rb;
    private Vector2 moveInput;
    private bool isGrounded;
    

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //animator = GetComponentInChildren<Animator>();
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("Jump");
        }
    }
    

    void FixedUpdate()
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 movement =forward * moveInput.y + right * moveInput.x;
        movement = Vector3.ClampMagnitude(movement, 1f);
       
        Vector3 velocity = rb.linearVelocity;

        velocity.x = movement.x * moveSpeed;
        velocity.z = movement.z * moveSpeed;

        rb.linearVelocity = velocity;
        rb.MoveRotation(Quaternion.Euler(0f, cameraTransform.eulerAngles.y, 0f));
        
    }

    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

}
