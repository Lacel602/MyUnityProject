using Assets.C__Script.NewScript.Animation;
using Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damagable))]
public class NewPlayerController : MonoBehaviour
{
    public InventoryManager inventoryManager;
    [SerializeField]
    private float walkSpeed = 5f;
    private Vector2 playerInput;

    private Rigidbody2D rb;
    private Animator animator;

    private TouchingDirections touchingDirections;
    private Damagable damagable;

    private CapsuleCollider2D playerCollider;

    [SerializeField]
    private bool _isFacingRight = true;

    [SerializeField]
    private float jumpForce = 16f;

    public GameObject outOfArrow;

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

    private float lastSpeed;
    private float currentSpeed
    {
        get
        {
            if (canMove)
            {
                if (isMoving && !touchingDirections.isOnWall)
                {
                    if (touchingDirections.isGrounded)
                    {
                        if (isRunning)
                        {
                            return walkSpeed * 1.5f;
                        }
                        else if (isCrouch)
                        {
                            return walkSpeed * 0.5f;
                        }
                        else
                        {
                            return walkSpeed;

                        }
                    }
                    else
                    {
                        if (lastSpeed != 0)
                        {
                            return lastSpeed;
                        }
                        else
                        {
                            return walkSpeed;
                        }
                    }
                }
                else
                {
                    return 0f;
                }
            }
            else
            {
                //Lock player movement while attacking
                return 0f;
            }
        }
    }

    public bool canMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }

    private bool _holdingBow = false;

    //private GameObject 
    public bool HoldingBow
    {
        get
        {
            return _holdingBow;
        }
        private set
        {
            _holdingBow = value;
            animator.SetBool(AnimationStrings.isRangedAttacking, _holdingBow);
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damagable = GetComponent<Damagable>();
        playerCollider = GetComponent<CapsuleCollider2D>();
    }

    void Start()
    {

    }

    private void FixedUpdate()
    {
        if (!damagable.LockVelocity)
        {
            rb.velocity = new Vector2(playerInput.x * currentSpeed, rb.velocity.y);
            lastSpeed = currentSpeed;
        }
        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }

    public void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        playerInput = context.ReadValue<Vector2>();

        if (IsAlive)
        {
            isMoving = playerInput != Vector2.zero;
            SetFacingDirection(playerInput);
        }
        else
        {
            isMoving = false;
        }
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
        if (context.started && !isCrouch)
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
        if (context.started && touchingDirections.isGrounded && canMove && !isCrouch)
        {
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    public void OnAttack(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.attackTrigger);
        }
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
    public void OnRangedAttack(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (inventoryManager.RemoveItem(inventoryManager.itemsToPickUp[0]))
            {
                HoldingBow = true;
            }
            else
            {
                //Display speech box
                outOfArrow.SetActive(true);
            }
        }
        if (context.canceled)
        {
            HoldingBow = false;
        }
    }
    [HideInInspector]
    public float bowHoldingTime = 0f;
    [HideInInspector]
    public int charged = 1;
    private bool isCrouch
    {
        get
        {
            return animator.GetBool(AnimationStrings.isCrouch);
        }

        set
        {
            animator.SetBool(AnimationStrings.isCrouch, value);
        }
    }
    public void OnCrouch(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.isGrounded)
        {
            if (isCrouch && !touchingDirections.isCeiling)
            {
                isCrouch = false;
                ChangePlayerCollider();
                Debug.Log("Crouching: " + isCrouch);
                return;
            }
            if (!isCrouch)
            {
                isCrouch = true;
                ChangePlayerCollider();
                Debug.Log("Crouching: " + isCrouch);
                return;
            }
        }
    }

    private void ChangePlayerCollider()
    {
        if (isCrouch)
        {
            //1.76 -> 1.26
            playerCollider.size = new Vector2(playerCollider.size.x, playerCollider.size.y - 0.5f);
            playerCollider.offset = new Vector2(playerCollider.offset.x, playerCollider.offset.y - 0.25f);
        }
        else
        {
            //1.26 -> 1.76
            playerCollider.size = new Vector2(playerCollider.size.x, playerCollider.size.y + 0.5f);
            playerCollider.offset = new Vector2(playerCollider.offset.x, playerCollider.offset.y + 0.25f);
        }
    }

    private void Update()
    {
        CheckingChargeBow();
    }

    private void CheckingChargeBow()
    {
        //Debug.Log("HoldingbowTime = " + bowHoldingTime);
        if (HoldingBow)
        {
            bowHoldingTime += Time.deltaTime;
        }
        if (bowHoldingTime < 0.5f)
        {
            charged = 1;
        }
        else if (bowHoldingTime >= 0.5f && bowHoldingTime < 1f)
        {
            charged = 2;
        }
        else if (bowHoldingTime >= 1f)
        {
            charged = 3;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Arrow"))
        {
            Arrow arrow = collision.gameObject.GetComponent<Arrow>();
            if (arrow.pickUp)
            {
                inventoryManager.AddItem(inventoryManager.itemsToPickUp[0]);
                Destroy(collision.gameObject);
            }
        }
    }
}
