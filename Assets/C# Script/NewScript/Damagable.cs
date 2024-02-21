using Assets.C__Script.NewScript.Animation;
using System;
using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour
{
    public UnityEvent<float, Vector2> damageableHit;
    private Animator animator;

    public event EventHandler OnDamaged;
    public event EventHandler OnHealed;

    [SerializeField]
    private bool _isAlive = true;
    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        private set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("isAlive: " + _isAlive);
        }
    }

    [SerializeField]
    public float _maxHealth = 1f;
    public float MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        private set
        {
            _maxHealth = value;
        }
    }

    private float _currentHealth;

    [SerializeField]
    private bool isInvincible = false;
    private float timeSinceHit = 0;
    [SerializeField]
    private float invincibilityTimer = 0.25f;

    [SerializeField]
    private DamagePopUp damagePopUp;
    [SerializeField]
    private DamagePopUp critDamagePopUp;
    public float CurrentHealth
    {
        get
        {
            return _currentHealth;
        }
        set
        {
            _currentHealth = value;

            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                IsAlive = false;
            }
        }
    }

    public bool LockVelocity
    {
        get
        {
            return animator.GetBool(AnimationStrings.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimationStrings.lockVelocity, value);
        }
    }

    private void Awake()
    {
        CurrentHealth = MaxHealth;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit >= invincibilityTimer)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
    }
    public void Hit(float damage, Vector2 knockback, bool isCritical)
    {
        if (IsAlive && !isInvincible)
        {
            CurrentHealth -= damage;
            isInvincible = true;
            LockVelocity = true;
            animator.SetTrigger(AnimationStrings.hitTrigger);
            //Create Pop-up text
            if (isCritical)
            {
                damage *= 1.5f;
                critDamagePopUp.Create(transform.position, (int)damage);
            }
            else
            {
                damagePopUp.Create(transform.position, (int)damage);
            }
            //Knockback
            damageableHit?.Invoke(damage, knockback);

            if (OnDamaged != null) OnDamaged(this, EventArgs.Empty);
        }
    }

    public void Healed(float amount)
    {
        if (IsAlive)
        {
            if (OnHealed != null) OnHealed(this, EventArgs.Empty);
        }
    }

    public float GetHealthRatio()
    {
        return CurrentHealth / MaxHealth;
    }
}
