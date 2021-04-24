using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
        //za�adowanie nast�pnej sceny w projekcie
    }

    public void QuitGame()
    {
        Debug.Log("Game closed!");
        Application.Quit();
    }
}
