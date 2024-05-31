/*using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public HealthBar healthBar;
    public int MaxHealth = 100;
    public int currentHealth;
    public int fireCount=0;
    public PlayerController pc;
    // Start is called before the first frame update
    void Start()
    {

        currentHealth = MaxHealth;
        healthBar.SetHealth(MaxHealth);
    }
    private void Update()
    {
        if (currentHealth <= 0)
        {
            pc.IsAlive = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fire"))
        {
            TakeDamage(55);
            fireCount++;
            if(fireCount>3)
            {
                pc.IsAlive = false;
            }
        }
    }
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);*/
 /*   }
}*/
