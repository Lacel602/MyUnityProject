using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    private DamagePopUp damagePopUp;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private float maxHealth = 100;
    [SerializeField]
    private float defAmor = 5;
    [SerializeField]
    private float deathTime = 2.0f;
    private float currentHealth;
    [SerializeField]
    private GameObject _popUpText;

    void Start()
    {
        damagePopUp = _popUpText.GetComponent<DamagePopUp>();
        currentHealth = maxHealth; 
    }

    public void TakeDamage(float damage, float piercing)
    {
        //Decrease health
        currentHealth -= damage * (100 - defAmor) * (100 + piercing) / 10000;
        //PopUpdamage
        damagePopUp.Create(transform.position, (int) damage);
        //Play hurt animation
        _animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
 
        //Disable enemies
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

        //Play dead animation
        _animator.SetBool("IsDead", true);

        Invoke("RemoveEnemy", deathTime);
    }

    private void RemoveEnemy()
    {
        // Remove or deactivate the GameObject when the death animation is done
        // You can use Destroy(gameObject) to completely remove it
        // or gameObject.SetActive(false) to deactivate it and reuse it later.
        gameObject.SetActive(false);
    }
}
