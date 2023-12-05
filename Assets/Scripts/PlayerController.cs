using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; 

    private Rigidbody2D rb;
    private float moveInput;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public FallingBlock fallingBlock;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");

        if (fallingBlock.nextBlock != null)
        {
            animator.Play("Run with something");
        }
        else {
            if (moveInput > 0 || moveInput < 0)
            {
                animator.Play("Run");
            }
            else
            {
                animator.Play("Idle");
            }
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        spriteRenderer.flipX = rb.velocity.x < 0f;
    }
}
