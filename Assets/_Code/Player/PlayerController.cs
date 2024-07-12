using System.Collections;
using UnityEngine;

// Random thought but i should add a detonator to this game for directional changes


[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    [Header("Walking")]

    public float walkSpeed = 10f;
    public float sprintSpeed = 16f;
    public float mouseSensitivity = 2f;
    
    [Space(10)]
    [SerializeField] Vector3 moveVelocity;

    [Header("Dashing")]

    [SerializeField] float dashCD = 1f;
    [SerializeField] float dashForce = 20f;
    [SerializeField] bool canDash = true;
    public KeyCode dashKey = KeyCode.LeftShift;
    
    [Space(10)]
    [SerializeField] Vector3 dashVelocity;

    bool shouldDash => canDash && Input.GetKeyDown(dashKey);
    

    // Other

    private Rigidbody rb;
    private Camera playerCamera;

    private float verticalLookRotation;

    IEnumerator Dash(){
        canDash = false;
        
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;
        dashVelocity = new Vector3(moveDirection.normalized.x * dashForce, rb.velocity.y, moveDirection.normalized.z * dashForce);

        // Apply movement to the Rigidbody
        rb.velocity = new Vector3(dashVelocity.x + moveVelocity.x, rb.velocity.y, dashVelocity.z + moveVelocity.z);

        // Pythagoras Theorem (I think this is right)
        int i = 0;
        bool debug = true;
        while(Mathf.Sqrt((dashVelocity.x * dashVelocity.x) + (dashVelocity.z * dashVelocity.z)) > 0){

            if(debug){
                Debug.Log($"X^2 = {dashVelocity.x * dashVelocity.x} || Z^2 = {dashVelocity.z * dashVelocity.z} || Hypotenuse = {Mathf.Sqrt((dashVelocity.x * dashVelocity.x) + (dashVelocity.z * dashVelocity.z))} ");
            }
            
            if(i == 10_000){
                Debug.Log("Escaped after 10,000 iterations");
                dashVelocity = Vector3.zero;
                break;
            }


            if(dashVelocity.x - 0.1f >= 0){
                dashVelocity.x -= 0.1f;
            }

            else{
                dashVelocity.x = 0;
            }
            
            if(dashVelocity.z - 0.1f >= 0){
                dashVelocity.z -= 0.1f;
            }

            else{
                dashVelocity.z = 0;
            }

            i++;
           
        }

        yield return new WaitForSeconds(dashCD);
        
        canDash = true;
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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        canDash = true;
    }

    void Update()
    {
        // if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0){
            HandleInput();
        // }

        HandleMouseLook();
        if(shouldDash){
            StartCoroutine(Dash());
        }
    }

    void HandleInput()
    {

        float moveSpeed = Input.GetKeyDown(KeyCode.LeftShift) ? sprintSpeed: walkSpeed;
        // Debug.Log(moveSpeed);

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;
        moveVelocity = moveDirection.normalized;

        // Apply movement to the Rigidbody
        rb.velocity = new Vector3(moveVelocity.x * moveSpeed + dashVelocity.x, rb.velocity.y, moveVelocity.z * moveSpeed);

        // Jumping (add your own conditions for jumping)
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
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
