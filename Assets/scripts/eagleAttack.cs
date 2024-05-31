using Unity.VisualScripting;
using UnityEngine;

public class EagleAttack : MonoBehaviour
{
    public Transform player; 
    public float followSpeed = 5f;
    public float birdSize = 0.25047f;
    public GameObject bird;
    private int hit=0;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;


        transform.Translate(directionToPlayer * followSpeed * Time.deltaTime);

       
        FlipSprite(directionToPlayer.x > 0);

    }

    private void FlipSprite(bool isFacingRight)
    {
       
        transform.localScale = new Vector2((isFacingRight ? birdSize : -birdSize), birdSize);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        if (collision.gameObject.CompareTag("Sword"))
        {
            hit += 1;
            if (hit>=1)
            {
                animator.SetBool("isAlive",false );
                GetComponent<Collider2D>().enabled = false;
                GetComponent<EagleAttack>().enabled = false;
                Destroy(bird, 1f); // destroy bird after 5 sec//
            }
            
        }
        else
        {
            return;
        }
    }

}
