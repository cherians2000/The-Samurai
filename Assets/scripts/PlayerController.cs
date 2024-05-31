using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class PlayerController : MonoBehaviour
{
   
    public GameObject defeated;
   

    public TextMeshProUGUI coinCountText;
    private int coinCount = 0;
    public float walkSpeed = 5f;
    public float RunSpeed = 8f;
    public float jumpImpulse=10f;
    public float airWalkSpeed = 3f;
    Damageable damageable;
  
    Vector2 moveInput;
    Rigidbody2D rb;
    Animator animator;
    private bool _isAlive = true;
    private bool _isRunning = false;
    private bool _isMoving=false;
    public bool _IsFacingRight=true;
    TouchingDirections touchingDir;
    private void Awake()
    {
       
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDir=GetComponent<TouchingDirections>();
        damageable = GetComponent<Damageable>();
       
 
    }
    public float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving && !touchingDir.IsOnwall)
                {
                    if (touchingDir.IsGround)
                    {
                        if (IsRunning)
                        {
                            return RunSpeed;
                        }
                        else
                        {
                            return walkSpeed;
                        }
                    }
                    else
                    {
                        return airWalkSpeed;
                    }

                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
          
        }
    }
    public bool IsAlive
    {
        get
        {
            return animator.GetBool("isAlive");
        }
      
    }
    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
         set
        {
            _isMoving = value;
            animator.SetBool("isMoving", value);
        }
    }
    public bool IsRunning {
        get
        {
            return _isRunning;
        }
       private set
        {
            _isRunning = value;
            animator.SetBool("isRunning", value);
        }
    
    }
   
    public bool IsFacingRight { get { return _IsFacingRight; } private set {
            if(_IsFacingRight !=value)
            {
                transform.localScale *= new Vector2(-1,1);
            }
            _IsFacingRight = value;
        }}
    public bool CanMove
    {
        get
        {
            return animator.GetBool("canMove");
        }
    }



    private void Update()
    {
        if (!IsAlive)
        {
            Invoke("ShowDefeated", 2f);
            IsMoving = false;
            gameObject.GetComponent<PlayerInput>().enabled = false;
        }
    }

    private void ShowDefeated()
    {
        defeated.SetActive(true);
        Time.timeScale = 0f;
    }
    private void FixedUpdate()
    {
        if (!damageable.LockVelocity)
        {
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
        }
       
        animator.SetFloat("yVelocity", rb.velocity.y);
    }
   
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if(IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;

            setFacingDirection(moveInput);
        }
        else
        {
            IsMoving=false;
        }
       
    }

    private void setFacingDirection(Vector2 moveInput)
    {
       if(moveInput.x >0 && !IsFacingRight)
        {
            IsFacingRight=true;
        }else if(moveInput.x <0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("run");
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started && touchingDir.IsGround && CanMove )
        {
            animator.SetTrigger("Jump");
            rb.velocity =new Vector2(rb.velocity.x,jumpImpulse);
        }
    }
    public void onAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger("Attack");
        }
    }
   public void onRangedAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger("rangedAttack");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fire"))
        {
            // Assuming you have a Damageable component on the player
            Damageable playerDamageable = GetComponent<Damageable>();

            if (playerDamageable != null)
            {
                // Apply damage to the player
                playerDamageable.Hit(100, Vector2.zero); // You can customize the knockback vector as needed
            }
        }

    }
    public void OnHit(int damage, Vector2 knockback)
    {
        
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    public void onCoinCollection()
    {
        coinCount++;
        coinCountText.text="coins :"+coinCount.ToString();
    }


}
