using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class InputManager : MonoBehaviour
{
    PlayerInput playerInput;
    public Vector2 movementInput;

    private void OnEnable()
    {
        if(playerInput == null)
        {
            playerInput = new PlayerInput();
            playerInput.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
        }
        playerInput.Enable();
    }
    private void OnDisable()
    {
        playerInput.Disable();
    }
}
