using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainItems : MonoBehaviour
{
    // Starten van het level
    public void StartLevel()
    {
        //Laad volgende scene in Queue
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //LoadScene("naam van scene" of #indexnummer)
    }

    public void QuitGame() 
    {
        //Om te testen binnen Unity
        Debug.Log("Spel Afgesloten");
        Application.Quit();
    }
}
