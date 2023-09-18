using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDamaged;
    public event EventHandler OnHealed;
    private float currentHealth;
    [SerializeField]
    private float maxHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void setMaxHealth(float health)
    {
        maxHealth = health;
        this.currentHealth = maxHealth;
    }
    public void Damaged(float damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        if (OnDamaged != null) OnDamaged(this, EventArgs.Empty);
    }

    public void Healed(float healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (OnHealed != null) OnHealed(this, EventArgs.Empty);
    }

    public float GetHealthRatio()
    {
        return currentHealth / maxHealth;
    }
}
