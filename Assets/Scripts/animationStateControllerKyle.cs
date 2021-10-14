using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateControllerKyle : MonoBehaviour
{

    //[SerializeField] Transform cameraFollow;
    Animator animator;
    int isRunningHash;
    int isSneakingHash;
    int isJumpingHash;
    int isIdleJumpHash;
    int isPickupHash;
    int isStrafeLeftHash;
    int isStrafeRightHash;
    int isRunbackHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isRunningHash = Animator.StringToHash("isRunning");
        isSneakingHash = Animator.StringToHash("isSneaking");
        isJumpingHash = Animator.StringToHash("isJumping");
        isPickupHash = Animator.StringToHash("isPickup");
        isStrafeLeftHash = Animator.StringToHash("isLeftStrafe");
        isStrafeRightHash = Animator.StringToHash("isRightStrafe");
        isRunbackHash = Animator.StringToHash("isRunBack");
        isIdleJumpHash = Animator.StringToHash("isIdleJump");
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isSneaking = animator.GetBool(isSneakingHash);
        bool isJumping = animator.GetBool(isJumpingHash);
        bool isPickup = animator.GetBool(isPickupHash);
        bool isStrafeLeft = animator.GetBool(isStrafeLeftHash);
        bool isStrafeRight = animator.GetBool(isStrafeRightHash);
        bool isRunback = animator.GetBool(isRunbackHash);
        bool isIdleJump = animator.GetBool(isIdleJumpHash);

        bool runPressed = Input.GetKey("w");
        float conRun = Input.GetAxisRaw("Vertical");
        bool sneakPressed = Input.GetKey("left ctrl");
        bool conSneak = Input.GetKey(KeyCode.JoystickButton1);
        bool jumpPressed = Input.GetKey("space");
        bool conJump = Input.GetKey(KeyCode.JoystickButton0);
        bool pickupPressed = Input.GetKey("e");
        bool conPickup = Input.GetKey(KeyCode.JoystickButton2);
        bool leftStrafe = Input.GetKey("a");
        float conStrafe = Input.GetAxisRaw("Horizontal");
        bool rightStrafe = Input.GetKey("d");     
        bool runBack = Input.GetKey("s");

        //RUNNING
        // if player is running
        if (!isRunning && runPressed)
        {
            //then set the isRunning boolean to be true
            animator.SetBool(isRunningHash, true);
        }
        // if player is running and stops running
        if (isRunning && !runPressed)
        {
            //then set the isRunning boolean to be false
            animator.SetBool(isRunningHash, false);
        }

        //RUNNING With Controller
        if (!isRunning && (conRun>0))
        {
            animator.SetBool(isRunningHash, true);
        }
        if (isRunning && !(conRun>0))
        {
            animator.SetBool(isRunningHash, false);
        }

        //Run Backwards
        if (!isRunback && runBack)
        {
            //then set the isisRunback boolean to be true
            animator.SetBool(isRunbackHash, true);
        }
        // if player is running backwards and stops
        if (isRunback && !runBack)
        {
            //then set the isisRunback boolean to be false
            animator.SetBool(isRunbackHash, false);
        }

        //Run Backwards with Controller
        if (!isRunback && (conRun<0))
        {
            animator.SetBool(isRunbackHash, true);
        }
        if (isRunback && !(conRun<0))
        {
            animator.SetBool(isRunbackHash, false);
        }

        //SNEAKING
        // if player is walking and not sneaking and pressing left shift key
        if (!isSneaking && (runPressed && sneakPressed))
        {
            //then set the isSneaking boolean to be true
            animator.SetBool(isSneakingHash, true);
        }
        // if player is sneaking and stops sneaking or stops walking
        if (isSneaking && (!runPressed || !sneakPressed))
        {
            //then set the isSneaking boolean to be false
            animator.SetBool(isSneakingHash, false);
        }

        //SNEAKING With Controller
        if (!isSneaking && (runPressed && conSneak))
        {
            animator.SetBool(isSneakingHash, true);
        }
        if (isSneaking && (!runPressed || !conSneak))
        {
            animator.SetBool(isSneakingHash, false);
        }

        //PICKUP & THROW
        // if player not holding ball pickup
        if (!isPickup && pickupPressed)
        {
            //then set the isPickup boolean to be true
            animator.SetBool(isPickupHash, true);
        }
        // if player is holding ball 
        if (isPickup && !pickupPressed)
        {
            //then set the isRunning boolean to be false
            animator.SetBool(isPickupHash, false);
        }

        //PICKUP & THROW With Controller
        if (!isPickup && conPickup)
        {
            animator.SetBool(isPickupHash, true);
        }
        if (isPickup && !conPickup)
        {
            animator.SetBool(isPickupHash, false);
        }

        //Strafe Left
        if (!isStrafeLeft && (conStrafe<0))
        {
            //then set the isStrafeLeft boolean to be true
            animator.SetBool(isStrafeLeftHash, true);
        }
        // if player is straffing and stops
        if (isStrafeLeft && !(conStrafe<0))
        {
            //then set the isStrafeLeft boolean to be false
            animator.SetBool(isStrafeLeftHash, false);
        }

        //Strafe Left
        if (!isStrafeLeft && leftStrafe)
        {
            animator.SetBool(isStrafeLeftHash, true);
        }
        if (isStrafeLeft && !leftStrafe)
        {
            animator.SetBool(isStrafeLeftHash, false);
        }

        //Strafe Right
        if (!isStrafeRight && rightStrafe)
        {
            //then set the isStrafeRight boolean to be true
            animator.SetBool(isStrafeRightHash, true);
        }
        // if player is straffing and stops
        if (isStrafeRight && !rightStrafe)
        {
            //then set the isStrafeRight boolean to be false
            animator.SetBool(isStrafeRightHash, false);
        }

        //Strafe Right
        if (!isStrafeRight && (conStrafe>0))
        {
            animator.SetBool(isStrafeRightHash, true);
        }
        if (isStrafeRight && !(conStrafe>0))
        {
            animator.SetBool(isStrafeRightHash, false);
        }

        //JUMPING STANDING STILL
        if (!isIdleJump && (!runPressed && jumpPressed))
        {
            //then set the isJumping boolean to be true
            animator.SetBool(isIdleJumpHash, true);

        }
        //if player is jumping and stops running or stops walking
        if (isIdleJump && !jumpPressed)
        {
            //then set the isJumping boolean to be false
            animator.SetBool(isIdleJumpHash, false);
        }

        //JUMPING with RUNNING
        if (!isJumping && (runPressed && jumpPressed))
        {
            //then set the isJumping boolean to be true
            animator.SetBool(isJumpingHash, true);

        }
        // if player is jumping and stops running or stops walking
        if (isJumping && (!runPressed || !jumpPressed))
        {
            //then set the isJumping boolean to be false
            animator.SetBool(isJumpingHash, false);
        } 
    }
}

