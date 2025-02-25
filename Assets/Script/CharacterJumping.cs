using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

public class CharacterJumping : MonoBehaviour
{
    [SerializeField] private float jumpForce = 20;
    [SerializeField] private int jumpCount = 0;
    Rigidbody rb;
    Animator animator;
    Collider collider;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jumping();
            
        }
    

    }
    private void Jumping()
    {
        if (jumpCount < 1) 
        { 
            rb.AddForce(Vector3.up*jumpForce,ForceMode.Impulse);
            animator.SetBool("isJumping",true);
            jumpCount++;
            Debug.Log("jump");
            DisableCollider();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
            animator.SetBool("isJumping", false);
            Debug.Log("isGrounded");
        }
    }
    void EnableCollider()
    {
        collider.enabled = true;
        Debug.Log("player collider: true");
    }
    void DisableCollider()
    {
        collider.enabled = false;
        Debug.Log("player collider: false");

    }

}
