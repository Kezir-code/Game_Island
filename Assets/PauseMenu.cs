using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool IfPauseGame = false;
    public GameObject UIPauseMenu;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IfPauseGame)
            {
                UIPauseMenu.SetActive(false);
                Time.timeScale = 1f;
                IfPauseGame = false;
            }
            else
            {
                UIPauseMenu.SetActive(true);
                Time.timeScale = 1f;
                IfPauseGame = true;
            }
        }
    }
}
