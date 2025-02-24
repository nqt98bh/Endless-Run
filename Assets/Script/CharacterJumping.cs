using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJumping : MonoBehaviour
{
    [SerializeField] private float jumpForce = 20;
    [SerializeField] private int jumpCount = 0;
    Rigidbody rb;
    Animator animator;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
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
}
