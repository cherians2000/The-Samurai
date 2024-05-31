using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    public ContactFilter2D castFilter;
    CapsuleCollider2D touchingCol;
    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float ceilingDistance = 0.05f;


    Animator animator;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];
    private bool _isGround = true;
    public bool IsGround { get
        { return _isGround; }
        private set
        {
            _isGround = value;
            animator.SetBool("isGrounded", value);
        }
}
    private bool  _isOnwall = true;
    private Vector2 wallCheckDirection=>gameObject.transform.localScale.x>0?Vector2.right:Vector2.left;
    public bool IsOnwall
    {
        get
        { return _isOnwall; }
        private set
        {
            _isOnwall = value;
            animator.SetBool("isOnWall", value);
        }
    }
    private bool _isOnCeiling = true;
    public bool IsOnCeiling
    {
        get
        { return _isOnCeiling; }
        private set
        {
            _isOnCeiling = value;
            animator.SetBool("isOnCeiling", value);
        }
    }
    private void Awake()
    {
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
       IsGround = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        IsOnwall = touchingCol.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = touchingCol.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;
    }
}
