using Enums;
using Interfaces;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace InputSystems
{
    public class PlayerInputProvider : MonoBehaviour, IInputProvider
    {
        private ICheckCollision _groundCheck;
        private const string JumpButton = "Jump";
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private GameObject groundCheckObject;
        private HashSet<InputAction> _requestedActions = new HashSet<InputAction>();
        public float GetAxis(Axis axis)
        {
            return Input.GetAxisRaw(axis.ToUnityAxis());
        }

        //Check if the action contain or not
        public bool GetActionPressed(InputAction action)
        {
            return _requestedActions.Contains(action);
        }

        public void Start()
        {
            animator = GetComponent<Animator>();
            _groundCheck = groundCheckObject.GetComponent<ICheckCollision>();
        }

        public void Update()
        {
            if (_requestedActions.Contains(InputAction.Attack))
            {
                Debug.Log("Contains attack = true");
            }
            CaptureInput();
        }

        private void CaptureInput()
        {
            GetJumpInput(); 
            GetAttackInput();
        }

        private void GetAttackInput()
        {

            if (Input.GetMouseButtonDown(0))
            {
                if (!_requestedActions.Contains(InputAction.Attack) && IsGrounded())
                {
                    _requestedActions.Add(InputAction.Attack);
                    Invoke("RemoveAttack", 0.5f * Time.timeScale);
                }            
            }
        }

        private bool IsGrounded()
        {
            return _groundCheck.CheckCollision();
        }
        private void RemoveAttack()
        {
            if (_requestedActions.Contains(InputAction.Attack))
            {
                _requestedActions.Remove(InputAction.Attack);
                //Debug.Log("Ket thuc atk");
            }
        }

        private void GetJumpInput()
        {
            if (Input.GetButtonDown(JumpButton))
            {
                _requestedActions.Add(InputAction.Jump);
            }
            if (Input.GetButtonUp(JumpButton))
            {
                _requestedActions.Remove(InputAction.Jump);
            }
        }
    }
}
