using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    float playerHeight = 2f;
    public int health = 10;
    Rigidbody rb;
    public Camera playerCam;

    [Header("PlayerStats")]
    [SerializeField] float PlayerVelocity;
    
    //Houses variables pertaining to movement and orirntation
    [Header("Movement")]
    public float movementSpeed = 9.5f;
    float horizontalMovement;
    float verticalMovement;

    Vector3 movementDirection;
    Vector3 slopeMovementDirection;

    //[SerializeField] GameObject playerCollider;

    public float movementMultiplier = 10f;
    //[SerializeField] float airMovementMultiplier = 0.25f;

    [Header("Wallrun")]
    bool isWallRunning = false;
    [SerializeField] Transform orientation;
    [SerializeField] float distanceToWall = 1.5f;
    [SerializeField] float slipFactor = 1.75f;
    [SerializeField] float jumpForce_WallVersion = 20f;
    [SerializeField] float wallRunMulti = 2.5f;
    bool leftWallCheck = false;
    bool rightWallCheck = false;
    RaycastHit leftHit;
    RaycastHit rightHit;

    [Header("Jump")]
    public float jumpForce = 15f;

    //Keep track off all control keys
    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode jumpKeyController = KeyCode.JoystickButton0; // A
    [SerializeField] KeyCode dashKey = KeyCode.LeftShift; //StickyKeys is an issue - add delay to circumvent???
    [SerializeField] KeyCode dashKeyController = KeyCode.JoystickButton3; // Y
    [SerializeField] KeyCode walkKeyController = KeyCode.JoystickButton1; // B
    [SerializeField] KeyCode walkKey = KeyCode.LeftControl;
    [SerializeField] KeyCode resetKey = KeyCode.P;
    //[SerializeField] KeyCode resetKeyController = KeyCode.JoystickButton6;


    //Variables for friction/drag 
    [Header("Friction/Drag")]
    float floorDrag = 6f;
    float airDrag = 1f;

    //variables for ground-detection
    [Header("Floor Detection")]
    [SerializeField] LayerMask floorMask;
    bool isOnFloor;
    bool isOnPlatform;
    //float floorDistcance = 0.5f;
    RaycastHit slopeHit;
    RaycastHit platformHit;
    private Transform platformHitPosition;

    //all other variables
    //[Header("Misc")]
    //bool crouchingState = false;

    /*OnFloor() checks if player is on ground
     * part of ground detection system
     */
    private bool OnFloor() 
    {
        //array of raycasts? to check if touching "floor"
        Collider[] hitColliders = Physics.OverlapBox(transform.position, transform.localScale / 3f, Quaternion.Euler(0, 0, 0), floorMask);
        bool check = false;
        //cycle through array >> if none true then not on floor, if just one, on floor
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i]) 
            {
                check = true;
            }
        }
        return check;
    }

    /*OnSlope() checks if player is on uneven ground
     * part of ground detection system
     */
    private bool OnSlope()
    {
        //simple downward raycast check |
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight/2 +0.5f))
        {
            //if the player is not at a 90' angle with the worlds level
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
        return false;
    }

    private bool OnPlatform()
    {
        //simple downward raycast check |
        if (Physics.Raycast(transform.position, Vector3.down, out platformHit, playerHeight / 2 + 0.5f))
        {
            if (platformHit.transform.tag == "Platform")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }


    //Checks to find a wall for wall running
    private void FindWall()
    {
        leftWallCheck = Physics.Raycast(transform.position, -orientation.right, out leftHit,distanceToWall);
        rightWallCheck = Physics.Raycast(transform.position, orientation.right, out rightHit,distanceToWall);
    }

    //Set up player when instance is first loaded
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        MovePlayer();
        isOnPlatform = OnPlatform();

        if(isOnPlatform)
        {
            if (platformHit.transform.tag == "Platform")
            {
                platformHitPosition = platformHit.transform;
                //transform.position = Vector3.Lerp(transform.position, platformHitPosition.position, 1);
                Vector3 platformPushDirection = platformHitPosition.transform.position - transform.position;
                rb.AddForce(platformPushDirection.normalized * 0.4f, ForceMode.Impulse);
            }
        }
    }

    //Update various conditions per frame
    private void Update()
    {
        

        //isOnFloor = Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f);
        //isOnFloor = Physics.CheckSphere(transform.position - new Vector3(0, 0.5f, 0), floorDistcance,floorMask); //constant check for collision with "floor"
        isOnFloor = OnFloor();
        
        PlayerInput();
        ControlDrag();
        FindWall();

        /* //DEBUGGING CHECKS
        if (isOnFloor)
        { 
            print("FLOOR");
        }
        
        if (OnSlope())
        { 
            print("SLOPE");
        }*/

        //implementation of various function in movement
        
        if(!isOnFloor)
        {
            //print("Wall Running");
            if (leftWallCheck)
            {
                //print("LEFT - Wall Running");
                BeginWallrunning();
            }
            else if(rightWallCheck)
            {
                //print("RIGHT - Wall Running");
                BeginWallrunning();
            }
            else 
            {
                EndWallrunning();
            }
        }
        else
        {
            EndWallrunning();
        }

        

        if ((Input.GetKeyDown(jumpKey) || Input.GetKeyDown(jumpKeyController)) && isOnFloor)//can only jump if on floor
        {
            Jump();
        }
        if((Input.GetKeyDown(jumpKey) || Input.GetKeyDown(jumpKeyController)) && isWallRunning)
        {
            //print("JUMP")
            if (leftWallCheck)
            {
                //print("LEFT")
                Vector3 jumpDirectrionFromWall = transform.up + leftHit.normal;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(jumpDirectrionFromWall * jumpForce_WallVersion, ForceMode.Acceleration);
            }
            else if (rightWallCheck)
            {
                //print("RIGHT")
                Vector3 jumpDirectrionFromWall = transform.up + rightHit.normal;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(jumpDirectrionFromWall * jumpForce_WallVersion, ForceMode.Acceleration);
            }
            else 
            { 
                Jump(); 
            }
        }
        /*if (!isOnFloor)//changes player velocity for better falling experience
        {
            rb.velocity += Vector3.up * Physics.gravity.y*Time.deltaTime;
        }*/
        if (Input.GetKeyDown(dashKey) || Input.GetKeyDown(dashKeyController))//dash happens at key press
        {
            Dash();
        }
        if (Input.GetKey(walkKey) || Input.GetKeyDown(walkKeyController))// && isOnFloor) //Dependent on if we want to allow crouching mid-air
        {
            Crouch();
        }
        if (Input.GetKeyUp(walkKey) || Input.GetKeyUp(walkKeyController))// && isOnFloor) //Dependent on if we want to allow crouching mid-air
        {
            unCrouch();

        }
        if (Input.GetKeyDown(resetKey))
        {
            ResetLevel();
        }

        PlayerVelocity = rb.velocity.magnitude;
        slopeMovementDirection = Vector3.ProjectOnPlane(movementDirection, slopeHit.normal); // used in slope detection
    }


    //receive input from keys WASD
    void PlayerInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal"); //[-1,1] ->[A,D]
        verticalMovement = Input.GetAxisRaw("Vertical"); //[-1,1] -> [W,S]

        movementDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement; 

    }

    /* BeginWallrunning() initialises the wallrunning for the player
     */
    void BeginWallrunning()
    {
        isWallRunning = true;
        rb.useGravity = false;
    }

    void EndWallrunning()
    {
        isWallRunning = false;
        rb.useGravity = true;
    }

    /*Dash() adds a brief acceleration to player - intended as sprint at first but dash makes gameplay more interesting
     * essentially pushes the player object 
     */
    void Dash()
    {
        Vector3 screenCenter = playerCam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 5));
        Vector3 dashDirection = screenCenter - transform.position;

        if (isOnFloor)
        {
            //rb.AddForce(movementDirection.normalized * movementSpeed * movementMultiplier * 10f, ForceMode.Acceleration);
            rb.velocity += movementDirection.normalized * 10f;
        }
        else
        {
            //rb.AddForce(screenCenter.normalized * movementSpeed * movementMultiplier * 5f, ForceMode.Acceleration);
            rb.velocity += dashDirection.normalized * 10f;
        }
        
    }

    /*Crouch() allows the player to move slower as well as "Shrink down"
     */
    void Crouch()
    {
        //shink aspects of player
        transform.localScale = new Vector3(1, 0.5f, 1);
        playerHeight = playerHeight / 2;
        
        //reduce speed
        movementSpeed = 1.75f;
        

        //old code - kept for completions sake
        /*if (!crouchingState)
        {
            transform.localScale = new Vector3(1,0.5f,1);
            movementSpeed = 3f;
            playerHeight = playerHeight / 2;
            crouchingState = true;
        }
        else if(crouchingState)
        {
            transform.localScale =  new Vector3(1,1f,1);
            movementSpeed = 8f;
            playerHeight = playerHeight * 2;
            crouchingState = false;
        }*/
    }
    void unCrouch()
    {
        //return normal player dimensions
        transform.localScale = new Vector3(1, 1f, 1);
        playerHeight = playerHeight * 2;

        //increase speed
        movementSpeed = 6f;
    }

    void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    //Simply changes drag/friction depending on location in scene - can add more types of floor???
    void ControlDrag()
    {
        if (isOnFloor)
        {
            rb.drag = floorDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    //Function to apply inputs to player object in scene
    void MovePlayer()
    {
        if (isOnFloor && !OnSlope())
        {
            rb.AddForce(movementDirection.normalized * movementSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (isOnFloor && OnSlope())
        {
            rb.AddForce(slopeMovementDirection.normalized * movementSpeed * movementMultiplier, ForceMode.Acceleration);

        }
        else if (!isOnFloor && !isWallRunning)
        {
            //using velocity instead of force as allows for "smoother" falling
            rb.velocity += Vector3.up * Physics.gravity.y * Time.deltaTime * 1.5f;
            rb.AddForce(movementDirection.normalized * movementSpeed, ForceMode.Acceleration);
            //rb.AddForce(movementDirection.normalized * movementSpeed * movementMultiplier* airMovementMultiplier, ForceMode.Acceleration);
        }
        else if (isWallRunning)
        {
            rb.AddForce(movementDirection.normalized * wallRunMulti * movementSpeed, ForceMode.Acceleration);
            rb.AddForce(Vector3.down * slipFactor, ForceMode.Acceleration); //Makes only gravity effect be the "slip factor of all walls
        }

        }

    //Reset scene/level
    void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
