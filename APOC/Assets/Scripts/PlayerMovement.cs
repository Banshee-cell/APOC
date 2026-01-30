using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;
    public float mouseSensitivity = 2f;

    public Transform cameraTransform;

    [Header("Head Bob")]
    public float bobSpeed = 14f;
    public float bobAmount = 0.05f;

    private CharacterController controller;
    private Vector3 velocity;
    private float xRotation = 0f;

    private float bobTimer = 0f;
    private Vector3 cameraStartLocalPos;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        cameraStartLocalPos = cameraTransform.localPosition;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
        HandleHeadBob();
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (controller.isGrounded)
        {
            if (velocity.y < 0)
                velocity.y = -5f;

            if (Input.GetKeyDown(KeyCode.Space))
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 100f * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 100f * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleHeadBob()
    {
        Vector3 horizontalVelocity = controller.velocity;
        horizontalVelocity.y = 0f;

        if (controller.isGrounded && horizontalVelocity.magnitude > 0.1f)
        {
            bobTimer += Time.deltaTime * bobSpeed;
            float bobOffset = Mathf.Sin(bobTimer) * bobAmount;

            cameraTransform.localPosition =
                cameraStartLocalPos + new Vector3(0f, bobOffset, 0f);
        }
        else
        {
            bobTimer = 0f;
            cameraTransform.localPosition = Vector3.Lerp(
                cameraTransform.localPosition,
                cameraStartLocalPos,
                Time.deltaTime * 8f
            );
        }
    }
}
