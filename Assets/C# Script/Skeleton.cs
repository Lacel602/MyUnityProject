using Assets.C__Script.NewScript.Animation;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Knight;

public class Skeleton : MonoBehaviour
{
    [Header("Movement")]
    public float walkAcceleration = 30f;
    public float maxSpeed = 3f;
    public float walkStopRate = 1f;

    [Header("Component")]
    [SerializeField]
    private DetectionZone attackZone;
    [SerializeField]
    private DetectionZone cliffDetection;
    private Animator animator;

    Rigidbody2D rb;
    private TouchingDirections touchingDirections;
    private Damagable damagable;
    private CapsuleCollider2D capsuleCollider2D;

    private void Awake()
    {
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damagable = GetComponent<Damagable>();
    }

    public void OnSpawn()
    {
        capsuleCollider2D.enabled = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        animator = GetComponent<Animator>();
        damagable = GetComponent<Damagable>();
    }
    public enum WalkEnum
    {
        Right, Left
    }
    private Vector2 walkDirectionVector = Vector2.left;
    private WalkEnum _walkDirection = WalkEnum.Left;
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
            }
            _walkDirection = value;
        }
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

    private void FixedUpdate()
    {
        if (touchingDirections.isGrounded && touchingDirections.isOnWall)
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
    public void FlipDirection()
    {
        if (WalkDirection == WalkEnum.Right)
        {
            WalkDirection = WalkEnum.Left;
            Debug.Log("Right to left");
        }
        else if (WalkDirection == WalkEnum.Left)
        {
            WalkDirection = WalkEnum.Right;
            Debug.Log("Left to Right");
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
        FlipDirection();
    }

    public void OnDeath()
    {
        Destroy(gameObject);
    }

    public void DisableAttackHitBox()
    {
        transform.Find("Attack").gameObject.SetActive(false);
    }

}
