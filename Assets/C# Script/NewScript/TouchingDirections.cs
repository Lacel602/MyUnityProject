using Assets.C__Script.NewScript.Animation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    [SerializeField]
    private bool precisionGroundCheck = false;

    public bool facingRightAtStart;
    public ContactFilter2D castFilter;

    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float ceilingDistance = 0.05f;

    private Collider2D touchingCol;
    [SerializeField]
    private Collider2D groundCol;
    private Animator animator;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    [SerializeField]
    private bool _isGrounded;
    public bool isGrounded
    {
        get
        {
            return _isGrounded;
        }
        private set
        {
            _isGrounded = value;
            animator.SetBool(AnimationStrings.isGrounded, value);
        }
    }

    private bool _isOnWall;
    public bool isOnWall
    {
        get
        {
            return _isOnWall;
        }
        private set
        {
            _isOnWall = value;
            animator.SetBool(AnimationStrings.isOnWall, value);
        }
    }

    private bool _isCeiling;
    public bool isCeiling
    {
        get
        {
            return _isCeiling;
        }
        private set
        {
            _isCeiling = value;
            animator.SetBool(AnimationStrings.isCeiling, value);
        }
    }

    private Vector2 wallCheckDirection()
    {
        if (facingRightAtStart)
        {
            return gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        }
        else
        {
            return gameObject.transform.localScale.x < 0 ? Vector2.right : Vector2.left;
        }
    }

    private void Awake()
    {
        touchingCol = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!precisionGroundCheck)
        {
            isGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        }
        else
        {
            isGrounded = groundCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        }
        isOnWall = touchingCol.Cast(wallCheckDirection(), castFilter, wallHits, wallDistance) > 0;
        isCeiling = touchingCol.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;
    }
}
