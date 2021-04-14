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
    int isJumpingStillHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isRunningHash = Animator.StringToHash("isRunning");
        isSneakingHash = Animator.StringToHash("isSneaking");
        isJumpingHash = Animator.StringToHash("isJumping");
        isJumpingStillHash = Animator.StringToHash("isJumpingStill");
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isSneaking = animator.GetBool(isSneakingHash);
        bool isJumping = animator.GetBool(isJumpingHash);
        bool isJumpingStill = animator.GetBool(isJumpingStillHash);
        bool runPressed = Input.GetKey("w");
        bool sneakPressed = Input.GetKey("left ctrl");
        bool jumpPressed = Input.GetKey("space");

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

        //JUMPING STANDING STILL
        if (!isJumpingStill && jumpPressed)
        {
            //then set the isJumping boolean to be true
            animator.SetBool(isJumpingStillHash, true);

        }
        // if player is jumping and stops running or stops walking
        if (isJumpingStill && !jumpPressed)
        {
            //then set the isJumping boolean to be false
            animator.SetBool(isJumpingStillHash, false);
        }
    }
}

