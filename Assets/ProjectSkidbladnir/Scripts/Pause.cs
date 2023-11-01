using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pauseCanvas;
    bool gamePaused;    
    void Start()
    {
        gamePaused = false;
    }   

    void Update()
    {
        bool pausebutton = Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape);
        if (pausebutton)
        {
            if (gamePaused)
            {
                pauseCanvas.SetActive(false);
                Time.timeScale = 1f;
                gamePaused = false;
            }
            else
            {
                pauseCanvas.SetActive(true);
                Time.timeScale = 0f;
                gamePaused = true;
            }  
        }
    }
    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Resume()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }
}