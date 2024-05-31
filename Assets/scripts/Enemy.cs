using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Enemy : MonoBehaviour
{
    public float walkspeed = 3f;
    public float walkStopRate;
    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;
    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    Animator animator;
    Damageable damageable;
    public enum WalkableDirection { Right,Left};
    private Vector2 walkDirectionVector=Vector2.right;

    private WalkableDirection _walkDirection;
    public WalkableDirection WalkDirection {
        get 
        { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }
           
            _walkDirection = value;
        }
    }

    public bool _hasTarget = false;
    

    public bool HasTarget { get { return _hasTarget; } 
        private set 
        {
            _hasTarget = value;
            animator.SetBool("hasTarget", value);
        }
    }
    
    public bool CanMove
    {
        get
        {
            return animator.GetBool("canMove");
        }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections=GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }
    private void Update()
    {
        HasTarget = attackZone.detectedColliders.Any(collider => collider.CompareTag("Player")); ;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if ( touchingDirections.IsGround && touchingDirections.IsOnwall)
        {
            FlipDirection();
        }
        if(!damageable.LockVelocity)
        {
            if (CanMove && touchingDirections.IsGround)
            {
                rb.velocity = new Vector2(walkspeed * walkDirectionVector.x, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
            }
        }
       
        
       
       
    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;

        }else if(WalkDirection == WalkableDirection.Left)
        {
            WalkDirection= WalkableDirection.Right;
        }
        else
        {
            Debug.LogError("sorry");
        }
    }
    public void onHit(int damage,Vector2 knockback)
    {
       
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
    public void OncliffDetected()
    {
        if(touchingDirections.IsGround)
        {
            FlipDirection();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Fire"))
        {
            // Assuming you have a Damageable component on the player
            Damageable EnemyDamageable = GetComponent<Damageable>();

            if (EnemyDamageable != null)
            {
                // Apply damage to the player
                EnemyDamageable.Hit(100, Vector2.zero); // You can customize the knockback vector as needed
            }
        }

    }
}
