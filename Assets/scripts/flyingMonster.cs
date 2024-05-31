using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyingMonster : MonoBehaviour
{
    public float flightSpeed = 3f;
    public float waypointReachedDistance = 0.1f;
    public DetectionZone biteDetectionZone;
    public List<Transform> waypoints;
    Animator animator;
    Rigidbody2D rb;
    Damageable damageable;
    int waypointNum = 0;
    Transform nextwaypoint;

    public bool _hasTarget = false;


    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool("hasTarget", value);
        }
    }
    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damageable = GetComponent<Damageable>();
    }
    private void Start()
    {
        nextwaypoint = waypoints[waypointNum];
    }
    // Update is called once per frame
    void Update()
    {
        HasTarget = biteDetectionZone.detectedColliders.Count > 0;
    }
    public bool CanMove
    {
        get
        {
            return animator.GetBool("canMove");
        }
    }
    private void FixedUpdate()
    {
        if (damageable.IsAlive)
        {
            if (CanMove)
            {
                Flight();
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            rb.gravityScale = 2f;
        }
    }
    private void Flight()
    {
        Vector2 directionToWaypoint = (nextwaypoint.position - transform.position).normalized;

        float distance = Vector2.Distance(nextwaypoint.position, transform.position);

        rb.velocity = directionToWaypoint * flightSpeed;
        UpdateDirection();

        if (distance < waypointReachedDistance)
        {
            waypointNum++;
            if (waypointNum >= waypoints.Count)
            {
                waypointNum = 0;
            }
            nextwaypoint = waypoints[waypointNum];
        }
    }
    public void UpdateDirection()
    {
        if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

}
