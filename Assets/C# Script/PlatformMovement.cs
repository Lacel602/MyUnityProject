using Enums;
using Extensions;
using Interfaces;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Windows;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(IInputProvider))]
public class PlatformMovement : MonoBehaviour
{
    private Rigidbody2D _rigiboby;
    private IInputProvider _inputProvider;
    private ICheckCollision _groundCheck;

    [SerializeField]
    private ParticleSystem dustEffect;
    [SerializeField]
    private ParticleSystem landingEffect;

    [SerializeField]
    private Animator _animator;
    [Header("Movement Configuration")]
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private GameObject groundCheckObject;
    private bool isFacecingRight = true;
    private bool canMove = true;
    private Vector2 lastGroundedPosition;

    [SerializeField]
    private GhostFadeEffect _ghosteffect;
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 6f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 0.5f;
    private void Start()
    {
        _rigiboby = GetComponent<Rigidbody2D>();
        _inputProvider = GetComponent<IInputProvider>();
        _groundCheck = groundCheckObject.GetComponent<ICheckCollision>();
        lastGroundedPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (!canMove || isDashing)
        {
            return;
        }
        ApplyHorizontalMovement();
        ApplyJump();
    }
    private void Update()
    {
        if (!_inputProvider.GetActionPressed(InputAction.Attack))
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            {
                StartCoroutine(Dash());
            }
        }
        CheckingCanMove();
        AnimationChange();
        SavingLastGroundedPosition();
    }

    private void CheckingCanMove()
    {
        if (_inputProvider.GetActionPressed(InputAction.Attack))
        {
            canMove = false;
            _rigiboby.SetVelocity(Axis.X, 0);
        }
        else
        {
            canMove = true;
        }
    }

    private void SavingLastGroundedPosition()
    {
        if (IsGrounded())
        {
            lastGroundedPosition = transform.position;
        }
    }

    public void ReturnLastGroundedPosition()
    {
        transform.position = lastGroundedPosition;
    }

    private void AnimationChange()
    {
        _animator.SetBool("IsDashing", isDashing);
        _animator.SetFloat("VerticalForce", _rigiboby.velocity.y);
        _animator.SetBool("IsGrounded", IsGrounded());
        _animator.SetFloat("Speed", Math.Abs(_rigiboby.velocity.x));
        if (_rigiboby.velocity.x > 0.01 && !isFacecingRight)
        {
            //Debug.Log("x = " + _rigiboby.velocity.x);
            Flip();
            //Debug.Log("turn right");
        }
        else if (_rigiboby.velocity.x < -0.01 && isFacecingRight)
        {
            // ... flip the player.
            //Debug.Log("x = " + _rigiboby.velocity.x);
            Flip();
            //Debug.Log("turn left");
        }
    }

    private void ApplyJump()
    {
       if (IsGrounded() && _inputProvider.GetActionPressed(InputAction.Jump))
        {
            _rigiboby.SetVelocity(Axis.Y, jumpForce);
            CreateDustEffect();
        }
    }

    private bool IsGrounded()
    {
        return _groundCheck.CheckCollision();
    }

    private void ApplyHorizontalMovement()
    {
        var inputX = _inputProvider.GetAxis(Axis.X);
        _rigiboby.SetVelocity(Axis.X, inputX * walkSpeed);       
    }

    private void Flip()
    {
        if (IsGrounded())
        {
            CreateDustEffect();
        }  

        isFacecingRight = !isFacecingRight;

        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }

    private void CreateDustEffect()
    {
        dustEffect.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            //Debug.Log("Landing");
            landingEffect.Play();
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = _rigiboby.gravityScale;
        _rigiboby.gravityScale = 0f;
        _rigiboby.SetVelocity(Axis.X, transform.localScale.x * dashingPower);
        _rigiboby.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        Debug.Log(_rigiboby.velocity);
        Debug.Log("Move");
        _ghosteffect.SetMakeGhost(true);
        yield return new WaitForSeconds(dashingTime);
        _ghosteffect.SetMakeGhost(false);
        _rigiboby.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
