using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 movespeed = new Vector2(20f,0);
    public Vector2 knockback = new Vector2(0, 0);
    public int damage = 15;
    Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Start()
    {
        rb.velocity = new Vector2(movespeed.x * transform.localScale.x, movespeed.y);
        Invoke("DestroyAfterDelay", 1.5f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable != null)
        {
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            bool gotHit = damageable.Hit(damage, deliveredKnockback);
            if (gotHit)
            {
               
                Debug.Log(collision.name + "hit for" + damage + "current Health = " + damageable.Health);
                Destroy(gameObject);
            }
        }
    }
    void DestroyAfterDelay()
    {
        Debug.Log("Projectile not triggered, destroying");
        Destroy(gameObject);
    }
}
