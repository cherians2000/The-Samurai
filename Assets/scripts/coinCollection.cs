using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coinCollection : MonoBehaviour
{
    AudioSource CoinCollectionAudio;

    private void Awake()
    {
        CoinCollectionAudio = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable)
        {
            PlayerController playerController = collision.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.onCoinCollection();
                AudioSource.PlayClipAtPoint(CoinCollectionAudio.clip, gameObject.transform.position, CoinCollectionAudio.volume);
                Destroy(gameObject);
            }
        }
    }
}
