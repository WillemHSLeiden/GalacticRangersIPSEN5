using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Nodig om van scenes te wisselen

public class MainItems : MonoBehaviour
{
    // Starten van het level
    public void StartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
