using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Collider2D attackCollider;

    public Vector2 knockbackForce = Vector2.zero;

    [SerializeField]
    private float attackDamage = 10f;

    private void Awake()
    {
        attackCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damagable damagable = collision.GetComponent<Damagable>();

        if (damagable != null)
        {
            Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockbackForce : new Vector2(- knockbackForce.x, knockbackForce.y);
            //Hit
            damagable.Hit(attackDamage, deliveredKnockback, false);
            Debug.Log(collision.name + " hit for " + attackDamage + " damage");
        }
    }
}
