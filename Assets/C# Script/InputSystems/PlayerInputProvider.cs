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
        private const string JumpButton = "Jump";
        [SerializeField]
        private Animator animator;

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
        }

        public void Update()
        {
            if (_requestedActions.Contains(InputAction.Attack))
            {
                //Debug.Log("Contains attack = true");
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
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            if (Input.GetMouseButtonDown(0))
            {
                if (!_requestedActions.Contains(InputAction.Attack))
                {
                    //Debug.Log("Add attack");
                    _requestedActions.Add(InputAction.Attack);
                }

                float t = 0.5f;
                Invoke("RemoveAttack", t * Time.timeScale);
            }

            //if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            //{
            //    if (_requestedActions.Contains(InputAction.Attack))
            //    {
            //        _requestedActions.Remove(InputAction.Attack);
            //        Debug.Log("Ket thuc atk");
            //    }
            //}
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
