using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void LoadLevel(int sceneIndex) 
    {
        FindObjectOfType<AudioManager>().Play("Knoppie");
        StartCoroutine(LoadAsync(sceneIndex));
        

    }

    IEnumerator LoadAsync(int sceneIndex)
    {
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        //operation.allowSceneActivation = false;

        while (!operation.isDone) 
        {
            //luiken dicht maybe


            /*if (Input.GetKeyDown(KeyCode.Space)) {
                operation.allowSceneActivation = true;
            }*/
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
