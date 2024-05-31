using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealText : MonoBehaviour
{
  public Vector3 moveSpeed = new Vector3 (0, 75, 0);
    public float timeToFade = 1f;

    RectTransform textTransform;
    TextMeshProUGUI textMeshPro;

    private float timeElapsed=0f;
    private Color startColor;


    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        textTransform=GetComponent<RectTransform>();
        startColor = textMeshPro.color;
    }

    // Update is called once per frame
    void Update()
    {
        textTransform.position += moveSpeed * Time.deltaTime;

        timeElapsed += Time.deltaTime;
        if(timeElapsed < timeToFade) 
        {
           float fadeAlpha= startColor.a *(1-(timeElapsed/timeToFade));
        textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b, fadeAlpha);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
