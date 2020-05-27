using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Results : MonoBehaviour
{
    public void NextLevel()
    {
        //Laad volgende scene in Queue
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //LoadScene("naam van scene" of #indexnummer)
    }

    public void StopLevel()
    {
        //Om te testen binnen Unity
        Debug.Log("Ga terug naar MainMenu");
        SceneManager.LoadScene("Menu");
    }
}
