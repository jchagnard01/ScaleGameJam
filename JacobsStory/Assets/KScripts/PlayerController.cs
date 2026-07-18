
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
  private Rigidbody rb;
private UnityEngine.Vector2 playerInput;

private void Awake()
  {
    rb = GetComponent<Rigidbody>();
  }

  public void OnJump(InputAction.CallbackContext context)
  {
    if (context.performed)
    {
      rb.AddForce(new Vector3(0, 100, 0));
    }
  }

  public void OnMovement(InputAction.CallbackContext context)
  {
    playerInput = context.ReadValue<UnityEngine.Vector2>();
  }

  private void Update()
  {
    rb.linearVelocity = new Vector3(playerInput.x, rb.linearVelocity.y, playerInput.y);
    
    //if(keyboard.current.akey) Debug.Log("clicked");
  }
}