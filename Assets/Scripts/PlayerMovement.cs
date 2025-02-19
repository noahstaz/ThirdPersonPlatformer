using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    private Rigidbody rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    private float groundCheckRadius = 0.3f;
    [SerializeField] private GameObject playerModel;
    [SerializeField] private float fallMultiplier = 2.5f; // Adjust to make falling faster


    void Start()
    {
        rb = playerModel.GetComponent<Rigidbody>();
    }

    public void Move(Vector3 direction)
    {
        // Stop applying force if player is touching a wall
        if (IsTouchingWall(direction))
        {
            direction = Vector3.zero;
        }

        Vector3 targetVelocity = direction * moveSpeed;
        targetVelocity.y = rb.linearVelocity.y; // Preserve vertical velocity

        rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, targetVelocity, 10f * Time.deltaTime);
        
        // Apply rotation
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, 10f * Time.deltaTime);
        }
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsGrounded()
    {
        Collider[] colliders = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);
        foreach (Collider col in colliders)
        {
            Vector3 normal = col.transform.up;
            if (Vector3.Angle(normal, Vector3.up) < 45f)
            {
                return true;
            }
        }
        return false;
    }

    private bool IsTouchingWall(Vector3 moveDirection)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, moveDirection, out hit, 0.6f, groundLayer))
        {
            float angle = Vector3.Angle(hit.normal, Vector3.up);
            return angle > 45f && angle < 135f; // Detect walls
        }
        return false;
    }

    void Update()
    {
        if (!IsGrounded()) // Apply extra gravity only when in the air
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

}