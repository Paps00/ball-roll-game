using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float minZoom = 10f;
    public float maxZoom = 80f;
    public Transform player;
    public float Speed_of_Rotation = 5f;
    public float zoomSpeed = 5f;
    

    private float currentRotation = 0f;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        cameraRotateAroundPlayer();
        Zooming();
        MoveCameraToPlayer();
        PlayerDirectionController();
    }

    void cameraRotateAroundPlayer()
    {
        currentRotation += Input.GetAxis("Mouse X") *Speed_of_Rotation;
        transform.rotation = Quaternion.Euler(0f, currentRotation, 0f);
    }

    void Zooming()
    {
        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView - scrollWheelInput * zoomSpeed, minZoom, maxZoom);
    }

    void MoveCameraToPlayer()
    {
        Quaternion rotation = Quaternion.Euler(0f, currentRotation, 0f);
        Vector3 rotatedOffset = rotation * offset;
        transform.position = player.position + rotatedOffset;
    }

    void PlayerDirectionController()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 cameraForward = transform.forward;
        Vector3 cameraRight = transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        Vector3 desiredMoveDirection = cameraForward.normalized * verticalInput + cameraRight.normalized * horizontalInput;

        MovePlayer(desiredMoveDirection);
    }

    void MovePlayer(Vector3 moveDirection)
    {
        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z) * Speed_of_Rotation;

        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            player.rotation = Quaternion.RotateTowards(player.rotation, toRotation, Speed_of_Rotation * Time.deltaTime * 10f);
        }
    }
}
