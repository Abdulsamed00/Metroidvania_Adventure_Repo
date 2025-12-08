using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 3f;
    private Vector2 moveInput;
    
    [Header("Jump")]
    [SerializeField] float jumpForce = 3f;
    private bool isGrounded = true;
    private bool jumpPressed = false;
    
    [Header("Raycast")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    
    Animator anim;
    Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Jump input alındı!");
            jumpPressed = true;
        }
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundDistance, groundLayer);
        Debug.DrawRay(groundCheck.position, Vector2.down, Color.red);
        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        anim.SetFloat("Speed", MathF.Abs(moveInput.x));


        if (moveInput.x > 0.1f)
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        else if (moveInput.x < -0.1f)
        {
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }

        if (jumpPressed && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce),ForceMode2D.Impulse);
            anim.SetTrigger("Jump");
            jumpPressed = false;
        }
    }
}
