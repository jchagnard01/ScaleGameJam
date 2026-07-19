using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Reflection;
using System.Security;

public class InputManager : MonoBehaviour
{
    PlayerManager playerManager;
    PlayerInput playerInput;
    PlayerLocomotion playerLocomotion;
    AnimatorManager animatorManager;

    public Vector2 movementInput;
    public float moveAmount;

    public float verticalInput;
    public float horizontalInput;

    public bool sprint_input;
    public bool jump_input;


    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        playerManager =  GetComponent<PlayerManager>();
    }


    private void OnEnable()
    {
        if(playerInput == null)
        {
            playerInput = new PlayerInput();
            playerInput.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
        
            playerInput.PlayerActions.Sprint.performed += i => sprint_input = true;
            playerInput.PlayerActions.Sprint.canceled += i => sprint_input = false;

            playerInput.PlayerActions.Jump.performed += i => jump_input = true;
            
        }

        playerInput.Enable();
    }
    private void OnDisable()
    {
        playerInput.Disable();
    }

    public void HandleAllInputs()
    {
        if(playerManager.isInteracting){return;}
        HandleMovementInput();
        HandleSprintInput();
        HandleJumpInput();
        //HandleAttackInput
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount);
        
        
    }

    private void HandleSprintInput()
    {
        if(sprint_input && moveAmount > 0.5f)
        {
            playerLocomotion.isSprinting = true;
        }
        else
        {
            playerLocomotion.isSprinting = false;
        }
    }
    
    private void HandleJumpInput()
    {
        if (jump_input)
        {
            jump_input = false;
            playerLocomotion.HandleJumping();
        }
    }
}
