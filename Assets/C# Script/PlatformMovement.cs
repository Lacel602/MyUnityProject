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

    [Header("Movement Configuration")]
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private GameObject groundCheckObject;
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
}
