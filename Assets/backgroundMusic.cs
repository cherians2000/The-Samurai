using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundMusic : MonoBehaviour
{
    public AudioSource Bgaudio;
    public PlayerController player;

    void Start()
    {
        Bgaudio = GetComponent<AudioSource>();
        player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.IsAlive==false)
        {
            Bgaudio.Stop();
        }
    }
}
