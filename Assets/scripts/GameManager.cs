using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Victory;
    public GameObject PausePanel;
    public PlayerController player;

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        PauseGame();
    }
    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PausePanel != null)
            {
                PausePanel.SetActive(true);
            }

            Time.timeScale = 0f;
        }
    }
    public void nextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1.0f;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;

    }
    public void exit()
    {
        Application.Quit();
    }
    public void DisplayVictory()
    {
        if(Victory != null)
        {
         Victory.SetActive(true);
        player.GetComponent<PlayerInput>().enabled = false;
        Time.timeScale = 0f;
        }
    }
    public void onContinue()
    {
        Time.timeScale = 1.0f;
        PausePanel.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("block"))
        {
            DisplayVictory();
        }
    }
}
