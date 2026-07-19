using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Playables;


//simple controller to get you started
public class SimpleController : MonoBehaviour
{
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private Animator animator;

    public AudioSource jump1;

    public AudioSource jump2;


    private Vector3 playerVelocity;
    private bool groundedPlayer = true;
    private float playerSpeed = 10.0f;
    private float jumpHeight = 1.5f;
    private float gravityValue = -9.81f;
    private float jumpCount = 2;
    private float jumpsRemaining;

    [SerializeField]
    private float verticalOffset = 1.0f;
    [SerializeField]
    private float delay = 1.0f;
    private float timer;
    


    private void Start()
    {
        UnityEngine.Debug.Log("Amount of jumps = " + jumpsRemaining);
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (Cursor.visible)
        {
            Cursor.visible = false;
        }
        //grounded check 
        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            jumpsRemaining = jumpCount;
        }
        if (!groundedPlayer && !controller.isGrounded)
        {
            animator.SetFloat("Vertical", playerVelocity.y);
        }
        else if (!groundedPlayer && controller.isGrounded)
        {
            animator.SetFloat("Vertical", 0);
            groundedPlayer = true;
            jumpsRemaining = 0;
        }

        //movement
        Vector3 cameraRight = Vector3.ProjectOnPlane(Camera.main.transform.right, Vector3.up).normalized;
        Vector3 cameraForward = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up).normalized;
        Vector3 move = Input.GetAxis("Horizontal") * cameraRight + Input.GetAxis("Vertical") * cameraForward;
        move.y = 0;
        controller.Move(move * Time.deltaTime * playerSpeed);
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
            animator.SetFloat("Forward", Mathf.Max(Mathf.Abs(Input.GetAxis("Vertical")), Mathf.Abs(Input.GetAxis("Horizontal"))));
        }

        //jumping
        if (Input.GetButtonDown("Jump") && (jumpsRemaining > 0))
        {
            UnityEngine.Debug.Log("jump has been pressed");
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            groundedPlayer = false;
            animator.SetFloat("Vertical", playerVelocity.y);
            jumpsRemaining -= 1;
            //   UnityEngine.Debug.Log("Amount of jumps = " + jumpsRemaining);
            if (jumpsRemaining == 1)
            {
                jump1.Play();
            }
            else if (jumpsRemaining == 0)
            {
                jump2.Play();
            }
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftControl) && groundedPlayer)
        {
            animator.SetBool("Sliding", true);
        }
        else
        {
            animator.SetBool("Sliding", false);
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
      
            animator.speed = 2f;
            playerSpeed = 20f;
           
        }
        else
        {
            animator.speed = 1f;
            playerSpeed = 10f;
        }


        
    }

    
}
