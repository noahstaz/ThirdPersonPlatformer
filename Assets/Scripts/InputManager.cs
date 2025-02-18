using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Camera mainCamera;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        bool jumpInput = Input.GetKeyDown(KeyCode.Space);

        Transform cameraTransform = mainCamera.transform;
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;
        
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 moveDirection = cameraRight * horizontalInput + cameraForward * verticalInput;
        moveDirection = moveDirection.normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            playerMovement.Move(moveDirection);
        }

        if (jumpInput)
        {
            playerMovement.Jump();
        }
    }
}