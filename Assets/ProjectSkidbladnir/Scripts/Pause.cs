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
                Resume();
            }
            else
            {
                Cursor.visible = true;
                pauseCanvas.SetActive(true);
                gamePaused = true;
                Time.timeScale = 0f;                               
            }  
        }
    }
    public void Retry()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Resume()
    {
        Cursor.visible = false;
        pauseCanvas.SetActive(false);
        gamePaused = false;
        Time.timeScale = 1f; 
        
    }
}