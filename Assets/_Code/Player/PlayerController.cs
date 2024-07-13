using System.Collections;
using UnityEditor.Search;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    [Header("Walking")]

    public float walkSpeed = 10f;
    public float sprintSpeed = 16f;
    public float mouseSensitivity = 2f;
    [SerializeField] float damping = 0.9f;
    
    [Space(10)]
    [SerializeField] Vector3 moveVelocity;

    [Header("Dashing")]

    [SerializeField] float dashCD = 1f;
    [SerializeField] float dashForce = 20f;
    [SerializeField] bool canDash = true;
    public KeyCode dashKey = KeyCode.LeftShift;
    
    [Space(10)]
    [SerializeField] Vector3 dashVelocity;

    [Header("Jumping")]

    [SerializeField] bool canJump = true;
    [SerializeField] float jumpForce = 6f;
    [SerializeField] KeyCode jumpKey = KeyCode.Space;

    [Header("Slamming")]

    [SerializeField] float slamForce = 30f;
    [SerializeField] bool canSlam;
    [SerializeField] KeyCode slamKey = KeyCode.LeftControl;
    

    bool shouldDash => canDash && Input.GetKeyDown(dashKey);
    bool shouldJump => isGrounded() && canJump && Input.GetKeyDown(jumpKey);
    bool shouldSlam => !isGrounded() && canSlam && Input.GetKeyDown(slamKey);

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
        canJump = true;
        canSlam = true;
    }

    void Update()
    {
       
        HandleInput();
      

        HandleMouseLook();
        if(shouldDash){
            StartCoroutine(Dash());
        }

        if(shouldSlam){
            Slam();
        }

        if(isGrounded()){
            canSlam = true;
        }
    }

    void FixedUpdate(){
        ApplyForce();
    }

    void ApplyForce(){
        // Apply the input force to the rigidbody's velocity
        rb.velocity += moveVelocity;

        // Ensure the input force doesn't affect the rigidbody's natural velocity decay
        rb.velocity = new Vector3(rb.velocity.x * (1 - Time.deltaTime), rb.velocity.y, rb.velocity.z * (1 - Time.deltaTime));
    
    }

    
    IEnumerator Slow(Vector3 reduction, float time){
        
        int reductionNum = 10;                              // The number of reductions until the velocity reaches 0
        Vector3 chunk = reduction/reductionNum;             // Idk what to call this variable, if you can think of a better name use it PLEASE

        for(int i = 0; i < reductionNum; i++){

            rb.velocity -= chunk;

            if(rb.velocity.x - chunk.x < 0){
                rb.velocity += new Vector3(rb.velocity.x - chunk.x + Mathf.Abs(rb.velocity.x - chunk.x), 0, 0);
            }

            if(rb.velocity.y - chunk.y < 0){
                rb.velocity += new Vector3(0, rb.velocity.y - chunk.y + Mathf.Abs(rb.velocity.y - chunk.y), 0);
            }
            

            if(rb.velocity.z - chunk.z < 0){
                rb.velocity += new Vector3(0, 0, rb.velocity.z - chunk.z + Mathf.Abs(rb.velocity.z - chunk.z));
            }
            /*
                ? I need to find how to make it so i remove just enough velocity so that rb.velocity go to 0

                0 - x + c

                10 - 11
                0 - 10 + 11 = 1
                
                x - c + (0-x+c) = 0
                x - c + 0 -x + c = 0
                
                100 - 101 = -1
                |-1| = 1

                x-c + |x-c|

                100 - 101 + |100 - 101|
                -1 + 1 = 0
                12 - 15 = -3
                -3 + 3 = 0

                YESSSSS I FOUND IT
                   
                
            
            */
            

            yield return new WaitForSeconds(time);

        }
        
    }

    void HandleInput()
    {

        float moveSpeed = Input.GetKeyDown(KeyCode.LeftShift) ? sprintSpeed: walkSpeed;
        // Debug.Log(moveSpeed);

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = (transform.right * moveX + transform.forward * moveZ) * walkSpeed;
        moveVelocity = moveDirection.normalized;

        moveVelocity *= damping;

        // Jumping (add your own conditions for jumping)
        if (shouldJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void Slam(){
        rb.velocity += new Vector3(0, -slamForce, 0);
        canSlam = false;
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
