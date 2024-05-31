using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerVictory : MonoBehaviour
{
    public GameObject vict;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("block"))
        {
            vict.SetActive(true);
        }
    }
}
