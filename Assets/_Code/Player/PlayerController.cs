using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 8f;
    public float mouseSensitivity = 2f;

    private Rigidbody rb;
    private Camera playerCamera;

    private float verticalLookRotation;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleInput();
        HandleMouseLook();
    }

    void HandleInput()
    {

        float moveSpeed = Input.GetKeyDown(KeyCode.LeftShift) ? sprintSpeed: walkSpeed;
        // Debug.Log(moveSpeed);

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;
        Vector3 moveVelocity = moveDirection.normalized;

        // Apply movement to the Rigidbody
        rb.velocity = new Vector3(moveVelocity.x * moveSpeed, rb.velocity.y, moveVelocity.z * moveSpeed);

        // Jumping (add your own conditions for jumping)
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
    }

    bool isGrounded(){
        Vector3 position = new Vector3(transform.position.x, transform.position.y - transform.localScale.y, transform.position.z);
        Vector3 size = new Vector3(transform.localScale.x * 0.7f, 0.1f, transform.localScale.z * 0.7f);

        if(Physics.OverlapBox(position, size, Quaternion.identity).Length > 1){

            //! Tells me why it is grounded (What objects the box overlaps with)
            /*    
            foreach(var obj in Physics.OverlapBox(position, size, Quaternion.identity)){
                Debug.Log(obj.name);
            }
            */
            return true;
        }
        return false;
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up * mouseX * mouseSensitivity);

        verticalLookRotation -= mouseY * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

        playerCamera.transform.localEulerAngles = Vector3.right * verticalLookRotation;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(new Vector3(transform.position.x, transform.position.y - transform.localScale.y, transform.position.z), new Vector3(transform.localScale.x * 0.7f, 0.1f, transform.localScale.z * 0.7f));
    }
}
