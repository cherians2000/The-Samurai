using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    public int healthRestore = 20;
    public Vector3 spinApple = new Vector3 (0,180,0);
    AudioSource HealthPiclkAudio;
    private void Awake()
    {
        HealthPiclkAudio = GetComponent<AudioSource>();
    }
    void Update()
    {
        transform.eulerAngles += spinApple * Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable)
        {
           bool wasHealed= damageable.Heal(healthRestore);
            if (wasHealed)
            {
                AudioSource.PlayClipAtPoint(HealthPiclkAudio.clip, gameObject.transform.position, HealthPiclkAudio.volume);
                Destroy(gameObject);
            }
           
        }
    }
}
