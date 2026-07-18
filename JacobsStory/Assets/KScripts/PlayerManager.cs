using UnityEngine;

public class PlayerManager : MonoBehaviour
{
   InputManager inputManager;
   PlayerLocomotion playerLocomotion;
   private void Awake()
    {
        inputManager= GetComponent<InputManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    private void Update()
    {
        inputManager.HandleAllInputs();
    }

    private void FixedUpdate()
    {
        //when moving/handling a rigidbody it's better to do it in fixed update
        playerLocomotion.HandleAllMovement();
    }
}
