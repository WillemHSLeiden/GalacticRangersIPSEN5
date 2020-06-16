using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private Animator leftAirlock, rightAirlock;

    public void LoadLevelAirlock(string sceneName) 
    {
        StartCoroutine(LoadAsyncAirlock(sceneName));
       
    }

    public void LoadLevel(string sceneName) {
        StartCoroutine(LoadAsync(sceneName));
    } 

    private IEnumerator LoadAsyncAirlock(string sceneName)
    {
        yield return null;

        //Close airlocks
        closeAirlocks();

        //Begin to load specified scene
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        //Don't allow scene to activate scene yet
        operation.allowSceneActivation = false;

        while (!operation.isDone)// && !leftAirlock.GetCurrentAnimatorStateInfo(0).IsName("AirlockClosed")) 
        {
            Debug.Log(operation.progress);

            /*if (Input.GetKeyDown(KeyCode.Space)) {
                operation.allowSceneActivation = true;
            }*/

            if (leftAirlock.GetCurrentAnimatorStateInfo(0).IsName("AirlockClosed") && operation.progress >= 0.9f) {
                operation.allowSceneActivation = true;
            }

            yield return null; //wacht een frame
        }
    }

    private IEnumerator LoadAsync(string sceneName) {
        yield return null;

        //Begin to load specified scene
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        //Don't allow scene to activate scene until it is loaded
        operation.allowSceneActivation = false;

        while (!operation.isDone)// && !leftAirlock.GetCurrentAnimatorStateInfo(0).IsName("AirlockClosed")) 
        {
            Debug.Log(operation.progress);

            if (operation.progress >= 0.9f) {
                operation.allowSceneActivation = true;
            }

            yield return null; //wacht een frame
        }
    }

    public void Quit()
    {
        //Om te testen binnen Unity
        Debug.Log("Spel Afgesloten");
        SceneManager.LoadSceneAsync("MainMenu");
    }

    private void closeAirlocks() {
        leftAirlock.SetTrigger("Close");
        rightAirlock.SetTrigger("Close");
    }
}
