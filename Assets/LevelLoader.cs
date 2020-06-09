using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    //Reference naar animatie
    public Animator transition;

    //Variabel voor de Inspector
    public float transitionTime = 3f; //1 on default

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    //IEnumerator coroutine
    IEnumerator LoadLevel(int LevelIndex) 
    {
        //Speel de animatie met gemaakte trigger
        transition.SetTrigger("Start");

        //Wacht voor bepaald aantal seconden
        yield return new WaitForSeconds(transitionTime);

        //laad de scene
        SceneManager.LoadScene(LevelIndex);
    }
}
