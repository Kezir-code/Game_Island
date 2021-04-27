using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauzaMenu : MonoBehaviour
{
    public static bool GameIsPaused = true;
    public GameObject pauseMenuUI;
    
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Pause();
            }
            
        }
    }
    
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void QuitGame()
    {
        Debug.Log("Quitting a game");
        Application.Quit();
    }
    public void BacktoGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        
    }
}
