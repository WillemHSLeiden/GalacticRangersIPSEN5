using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // static nodig om te checken vanuit andere scripts, zonder referece naar dit script
    public static bool GamePaused = false;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            if (GamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            
            }

        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; //freeze time
        GamePaused = false;

    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; //freeze time
        GamePaused = true;
    
    }

    public void QuitGame()
    {
        //Om te testen binnen Unity
        Debug.Log("Spel Afgesloten");
        Application.Quit();
    }
}
