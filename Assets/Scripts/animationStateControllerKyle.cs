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
    int isPickupHash;
    int isStrafeLeftHash;
    int isStrafeRightHash;
    int isRunbackHash;

    //int isJumpingStillHash;
    //int isThrowHash;  

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

        //isJumpingStillHash = Animator.StringToHash("isJumpingStill");
        //isThrowHash = Animator.StringToHash("isThrow");   
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

        //bool isJumpingStill = animator.GetBool(isJumpingStillHash);     
        //bool isThrow = animator.GetBool(isThrowHash);

        bool runPressed = Input.GetKey("w");
        bool sneakPressed = Input.GetKey("left ctrl");
        bool jumpPressed = Input.GetKey("space");
        bool pickupPressed = Input.GetKey("e");
        bool leftStrafe = Input.GetKey("a");
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

        //PICKUP
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

        //Strafe Left
        if (!isStrafeLeft && leftStrafe)
        {
            //then set the isStrafeLeft boolean to be true
            animator.SetBool(isStrafeLeftHash, true);
        }
        // if player is straffing and stops
        if (isStrafeLeft && !leftStrafe)
        {
            //then set the isStrafeLeft boolean to be false
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

        //JUMPING STANDING STILL
        //if (!isJumpingStill && jumpPressed)
        //{
            //then set the isJumping boolean to be true
        //    animator.SetBool(isJumpingStillHash, true);

        //}
        // if player is jumping and stops running or stops walking
        //if (isJumpingStill && !jumpPressed)
        //{
            //then set the isJumping boolean to be false
        //    animator.SetBool(isJumpingStillHash, false);
        //}

        //THROW
        // if player not holding ball pickup
        //if ((!isThrow && isPickup) && pickupPressed)
        //{
            //then set the isPickup boolean to be true
        //    animator.SetBool(isThrowHash, true);
        //}
        // if player is holding ball 
        //if ((isThrow && !isPickup) && !pickupPressed)
        //{
            //then set the isRunning boolean to be false
        //    animator.SetBool(isThrowHash, false);
        //}


    }
}

