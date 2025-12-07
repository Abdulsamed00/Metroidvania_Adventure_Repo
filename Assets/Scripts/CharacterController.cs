using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    private Vector2 moveInput;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);

        if (moveInput.x > 0.1f)
        {
            transform.localScale = new Vector2(MathF.Abs(transform.localScale.x), transform.localScale.y);
        }
        else if (moveInput.x < -0.1f)
        {
            transform.localScale = new Vector2(-MathF.Abs(transform.localScale.x), transform.localScale.y);
        }
    }
}
