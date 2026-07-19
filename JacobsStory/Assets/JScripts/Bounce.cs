using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [Range(20, 1000)]
    public float bounceHeight;

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
        rb.AddForce(Vector3.up * bounceHeight);
    }
}