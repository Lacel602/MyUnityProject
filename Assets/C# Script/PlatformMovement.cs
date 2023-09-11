using Enums;
using Extensions;
using Interfaces;
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(IInputProvider))]
public class PlatformMovement : MonoBehaviour
{
    private Rigidbody2D _rigiboby;
    private IInputProvider _inputProvider;
    private ICheck _groundCheck;
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
    private void Start()
    {
        _rigiboby = GetComponent<Rigidbody2D>();
        _inputProvider = GetComponent<IInputProvider>();
        _groundCheck = groundCheckObject.GetComponent<ICheck>();
    }

    private void FixedUpdate()
    {
        ApplyHorizontalMovement();
        ApplyJump();
        
    }
    private void Update()
    {
        AnimationChange();
    }

    private void AnimationChange()
    {
        _animator.SetFloat("VerticalForce", _rigiboby.velocity.y);
        _animator.SetBool("IsGrounded", IsGrounded());
        _animator.SetFloat("Speed", Math.Abs(_rigiboby.velocity.x));



        //Move to right and facing left
        if (_rigiboby.velocity.x > 0.01 && !isFacecingRight)
        {
            Debug.Log("x = " + _rigiboby.velocity.x);
            Flip();
            Debug.Log("turn right");
        }
        // Otherwise if the input is moving to left and facing right...
        else if (_rigiboby.velocity.x < -0.01 && isFacecingRight)
        {
            // ... flip the player.
            Debug.Log("x = " + _rigiboby.velocity.x);
            Flip();
            Debug.Log("turn left");
        }
    }

    private void ApplyJump()
    {
       if (IsGrounded() && _inputProvider.GetActionPressed(InputAction.Jump))
        {
            _rigiboby.SetVelocity(Axis.Y, jumpForce);
            
        }
    }

    private bool IsGrounded()
    {
        return _groundCheck.Check();
    }

    private void ApplyHorizontalMovement()
    {
        var inputX = _inputProvider.GetAxis(Axis.X);
        _rigiboby.SetVelocity(Axis.X, inputX * walkSpeed);       
    }

    private void Flip()
    {
        //Switch the way the player is labelled as facing.
        isFacecingRight = !isFacecingRight;
        // Multiply the player's x local scale by -1.
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }
}
