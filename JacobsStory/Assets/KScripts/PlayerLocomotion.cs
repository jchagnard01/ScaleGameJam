using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    InputManager inputManager;
    PlayerManager playerManager;
    AnimatorManager animatorManager;
    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRigidbody;

    public float inAirTimer;
    public float leapingVelocity;
    public float fallingVelocity;
    public float rayCastHeightOffset = 0.5f;
    public LayerMask groundLayer; 

    public bool isSprinting;
    public bool isGrounded;
    public bool isJumping;

    public float walkSpeed = 1;
    public float movementSpeed = 2;
    public float sprintSpeed = 5;
    public float rotationSpeed = 7;

    public float jumpHeight = 4;
    public float gravityIntensity = -1;


    public void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerManager = GetComponent<PlayerManager>();
        animatorManager = GetComponent<AnimatorManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;

    }
    public void HandleAllMovement()
    {
        //if (playerManager.isInteracting){return;}
        HandleFallingAndLanding();
        HandleMovement();
        HandleRotation();
       // HandleJumping();
    }
    private void HandleMovement()
    {
        
        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;

        if(isSprinting)
        {
            moveDirection = moveDirection * sprintSpeed;
        }
        else
        {
            if (inputManager.moveAmount >= 0.5f)
            {
             moveDirection = moveDirection * movementSpeed;
            }
            else
            {
                moveDirection = moveDirection * walkSpeed;
            }
        }
        

        //moveDirection = moveDirection * movementSpeed;
        Vector3 movementVelocity = moveDirection;
        playerRigidbody.linearVelocity = movementVelocity;
    }

    private void HandleRotation()
    {
        if (isJumping)
            return;

        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward; //pretty much keep rotation @ the position we looking at
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    private void HandleFallingAndLanding()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        rayCastOrigin.y = rayCastOrigin.y + rayCastHeightOffset;

        if(!isGrounded && !isJumping)
        {
            if(!playerManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("Falling", true);
            }

            inAirTimer = inAirTimer + Time.deltaTime;
            playerRigidbody.AddForce(transform.forward * leapingVelocity);
            playerRigidbody.AddForce(-Vector3.up * fallingVelocity * inAirTimer);

        }
        if(Physics.SphereCast(rayCastOrigin, 0.2f, -Vector3.up, out hit, groundLayer))
        {
            if(!isGrounded && !playerManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("Land", true);
            }

            inAirTimer = 0;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    public void HandleJumping()
    {
        if ( isGrounded)
         { 
            animatorManager.animator.SetBool("isJumping", true);
            animatorManager.PlayTargetAnimation("Jump", false);

            float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
            Vector3 playerVelocity = moveDirection;
            playerVelocity.y = jumpingVelocity;
            playerRigidbody.linearVelocity = playerVelocity;
        }
       
    }
}
