using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class timmer : MonoBehaviour
{
    public TextMeshProUGUI timer;
    public float timeleft = 10;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        timeleft -= Time.deltaTime;
        if(timeleft <= 0)
        {
            return;
        }
        timer.text=Mathf.FloorToInt(timeleft).ToString();
    }
}
