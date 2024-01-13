using Assets.C__Script.NewScript.Animation;
using Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class NewPlayerController : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 5f;
    private float airMovementSpeed = 4f;
    private Vector2 playerInput;

    private Rigidbody2D rb;
    private Animator animator;

    private TouchingDirections touchingDirections;

    [SerializeField]
    private bool _isFacingRight = true;

    [SerializeField]
    private float jumpForce = 16f;

    public bool isFacingRight
    {
        get
        {
            return _isFacingRight;
        }
        set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    private bool _isMoving = false;
    public bool isMoving
    {
        get
        {
            return _isMoving;
        }
        set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }

    private bool _isRunning = false;

    public bool isRunning
    {
        get
        {
            return _isRunning;
        }
        set
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isFastRunning, value);
        }
    }

    private float currentSpeed
    {
        get
        {
            if (isMoving && !touchingDirections.isOnWall)
            {

                if (isRunning)
                {
                    return walkSpeed * 1.5f;
                }
                else
                {
                    return walkSpeed;
                }
            }
            else
            {
                return 0f;
            }
        }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
    }

    void Start()
    {

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(playerInput.x * currentSpeed, rb.velocity.y);
        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }

    public void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        playerInput = context.ReadValue<Vector2>();
        isMoving = playerInput != Vector2.zero;

        SetFacingDirection(playerInput);
    }

    private void SetFacingDirection(Vector2 playerInput)
    {
        if (playerInput.x > 0 && !isFacingRight)
        {
            //Face right
            isFacingRight = !isFacingRight;
        }
        else if (playerInput.x < 0 && isFacingRight)
        {
            //Face left
            isFacingRight = !isFacingRight;
        }
    }

    public void OnRun(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isRunning = true;
        }
        else if (context.canceled)
        {
            isRunning = false;
        }
    }

    public void OnJump(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.isGrounded)
        {
            animator.SetTrigger(AnimationStrings.jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
