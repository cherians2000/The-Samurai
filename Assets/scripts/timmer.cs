using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountdownScript : MonoBehaviour
{
    public float countdownTime = 10f;  // Set the initial countdown time in seconds
    public TextMeshProUGUI countdownText;  // Reference to the UI Text element

    void Update()
    {
        // Check if the countdown has not reached zero
        if (countdownTime > 0f )
        {
            // Decrease the countdown time by Time.deltaTime
            countdownTime -= Time.deltaTime;

            // Update the UI Text with the current countdown time
            countdownText.text = Mathf.Round(countdownTime).ToString();  // Round for display purposes

        }
    }
}
