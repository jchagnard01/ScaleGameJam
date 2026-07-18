using System.Reflection;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
   InputManager inputManager;
   PlayerLocomotion playerLocomotion;

   Animator animator;
   
   public bool isInteracting;
   
      private void Awake()
    {
        inputManager= GetComponent<InputManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        animator = GetComponent<Animator>();
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

    private void LateUpdate()
    {
        isInteracting = animator.GetBool("isInteracting");
    }
}
