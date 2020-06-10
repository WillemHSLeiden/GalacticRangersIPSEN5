using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void LoadLevel(int sceneIndex) 
    {
        StartCoroutine(LoadAsync(sceneIndex));
       
    }

    IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone) 
        {
            //luiken dicht maybe
            
            yield return null; //wacht een frame
            
        }
    }

    public void Quit()
    {
        //Om te testen binnen Unity
        Debug.Log("Spel Afgesloten");
        SceneManager.LoadSceneAsync("Menu");
    }
}
