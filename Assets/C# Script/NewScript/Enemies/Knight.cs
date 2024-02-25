using Assets.C__Script.NewScript.Animation;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TouchingDirections), typeof(Rigidbody2D), typeof(Damagable))]
public class Knight : MonoBehaviour
{
    [SerializeField]
    public float walkAcceleration = 30f;
    [SerializeField]
    public float maxSpeed = 3f;
    [SerializeField]
    public float walkStopRate = 1f;

    [SerializeField]
    private DetectionZone attackZone;
    [SerializeField]
    private DetectionZone cliffDetection;
    private Animator animator;

    Rigidbody2D rb;
    private TouchingDirections touchingDirections;
    private Damagable damagable;

    public enum WalkEnum
    {
        Right, Left
    }
    private Vector2 walkDirectionVector = Vector2.right;
    private WalkEnum _walkDirection = WalkEnum.Right;

    public WalkEnum WalkDirection
    {
        get
        {
            return _walkDirection;
        }
        set
        {
            if (_walkDirection != value)
            {
                transform.localScale *= new Vector2(-1, 1);
                if (value == WalkEnum.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkEnum.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
                _walkDirection = value;
            }        
        }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damagable = GetComponent<Damagable>();
    }

    private bool _hasTarget = false;
    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public float AttackCooldown
    {
        get
        {
            return animator.GetFloat(AnimationStrings.attackCooldown);
        }
        private set
        {
            animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0));
        }
    }

    private void Update()
    {
        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
        HasTarget = attackZone.detectedColliders.Count > 0;
    }

    private void FixedUpdate()
    {
        if (touchingDirections.isGrounded && touchingDirections.isOnWall && Mathf.Abs(rb.velocity.x) > 0.001)
        {
            FlipDirection();
        }

        if (!damagable.LockVelocity)
        {
            if (CanMove)
            {
                rb.velocity = new Vector2(
                    Mathf.Clamp(
                        rb.velocity.x +
                        (walkAcceleration * walkDirectionVector.x * Time.fixedDeltaTime), -maxSpeed, maxSpeed)
                    , rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
            }
        }
    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkEnum.Right)
        {
            WalkDirection = WalkEnum.Left;
        }
        else if (WalkDirection == WalkEnum.Left)
        {
            WalkDirection = WalkEnum.Right;
        }
        else
        {
            Debug.LogError("Current walkDirection is not set to valid value");
        }
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    public void OnCliffDetected()
    {
        FlipDirection() ;
    }
}
