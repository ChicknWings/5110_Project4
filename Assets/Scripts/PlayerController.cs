using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; 

    private Rigidbody2D rb;
    private float moveInput;
    private Animator animator;
    public GameObject spriteObject;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");

        if (moveInput > 0) 
        {
            animator.Play("Run");
            
        }

           
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }
}
