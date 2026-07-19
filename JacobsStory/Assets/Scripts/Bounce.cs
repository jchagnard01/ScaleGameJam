using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [Range(20, 1000)]
    public float bounceHeight;
    private Vector3 force;

    GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("Collided");
        GameObject bouncer = collision.gameObject;
        Rigidbody rb = bouncer.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * bounceHeight);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided");
        Rigidbody rb = player.GetComponent<Rigidbody>();
       // CharacterController cc = player.GetComponent<CharacterController>();
       // force = Vector3.up * bounceHeight;
        rb.AddForce(Vector3.up * bounceHeight);
       // cc.Move(force);
    }
}