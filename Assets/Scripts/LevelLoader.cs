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
        Application.backgroundLoadingPriority = ThreadPriority.Low;
        StartCoroutine(LoadAsyncAirlock(sceneName));
    }

    public void LoadLevel(string sceneName) {
        StartCoroutine(LoadAsync(sceneName));
    } 

    private IEnumerator LoadAsyncAirlock(string sceneName)
    {

        //Close airlocks
        closeAirlocks();

        //Begin to load specified scene
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        //Don't allow scene to activate scene yet
        operation.allowSceneActivation = false;

        while (!operation.isDone && !leftAirlock.GetCurrentAnimatorStateInfo(0).IsName("AirlockClosed"))
        {
            Debug.Log(operation.progress);
            yield return null;
        }
        Debug.Log("Finished loading");
        operation.allowSceneActivation = true;
    }

    private IEnumerator LoadAsync(string sceneName) {

        //Begin to load specified scene
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        //Don't allow scene to activate scene until it is loaded
        operation.allowSceneActivation = false;

        while (!operation.isDone && operation.progress < 0.9f)// && !leftAirlock.GetCurrentAnimatorStateInfo(0).IsName("AirlockClosed")) 
        {
            Debug.Log(operation.progress);
            yield return null;
        }
        operation.allowSceneActivation = true;
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
