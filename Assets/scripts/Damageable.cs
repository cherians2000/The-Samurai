using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damagableHit;
    public UnityEvent<int, int> healthChanged;
    public int _maxHealth;
    private Animator animator;
    public int MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    public int _health = 100;

    public int Health
    {
        get { return _health; }
        set
        {
            _health = value;
            healthChanged?.Invoke(_health, MaxHealth);
            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    private bool _isAlive = true;
    private bool isInvincible = false;

    private float timeSinceHit = 0;
    public float isInvincibilityTime = 0.25f;

    public bool IsAlive
    {
        get { return _isAlive; }
        set
        {
            _isAlive = value;
            animator.SetBool("isAlive", value);
            Debug.Log("alive is " + value);
        }
    }
    public bool LockVelocity
    {
        get
        {
            return animator.GetBool("lockVelocity");
        }
        set
        {
            animator.SetBool("lockVelocity", value);
        }
    }
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > isInvincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
        
        
    }

    public bool Hit(int damage,Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;

            animator.SetTrigger("hit");
            LockVelocity = true;
            damagableHit?.Invoke(damage, knockback);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);
            return true;
           
        }
        return false;
    }
    public bool Heal(int healthRestore)
    {
        if (IsAlive && Health <MaxHealth)
        {
            int maxHeal = Mathf.Max(MaxHealth - Health,0);
            int actualHeal= Mathf.Min(maxHeal, healthRestore);
            Health += actualHeal;
            CharacterEvents.characterHealed(gameObject,actualHeal);
            return true;
        }
        return false;
    }
}
