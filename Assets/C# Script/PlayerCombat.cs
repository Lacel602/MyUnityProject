using Enums;
using Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Transform _atkpoint1;
    [SerializeField]
    private Transform _atkpoint2;
    [SerializeField]
    private float atkDamage;
    [SerializeField]
    private float atkRate = 2f;
    [SerializeField]
    private float piercing;
    [SerializeField]
    public LayerMask enemylayers;
    private IInputProvider _inputProvider;

    float nextAttackTime = 0f;

    public void Start()
    {
        _inputProvider = GetComponent<IInputProvider>();
    }
    void Update()
    {
        //Debug.Log("aa " + Time.time);
        if (Time.time >= nextAttackTime)
        {
            if (_inputProvider.GetActionPressed(InputAction.Attack))
            {
                PlayerAttack();
                nextAttackTime = Time.time + 1f / atkRate;
            }
        }
    }

    private void PlayerAttack()
    {
        //Trigger attack animation
        _animator.SetTrigger("Attacking");
        //Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapAreaAll(_atkpoint1.position, _atkpoint2.position, enemylayers);
        //Damage all enemies in range
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(atkDamage, piercing);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_atkpoint1 == null || _atkpoint2 == null)
        {
            return;
        }
        // Set color 
        Gizmos.color = Color.red;
        Vector2 center = (_atkpoint1.position + _atkpoint2.position) / 2f;
        Vector2 size = new Vector2(Mathf.Abs(_atkpoint2.position.x - _atkpoint1.position.x), Mathf.Abs(_atkpoint2.position.y - _atkpoint1.position.y)); // Calculate the size of the rectangle

        // Draw a wireframe rectangle to represent the overlap area
        Gizmos.DrawWireCube(center, size);
    }
}
