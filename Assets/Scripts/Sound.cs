using UnityEngine.Audio;
using UnityEngine;

[System.Serializable] //Inspector visible
public class Sound {

    public string name;
    //reference to audio
    public AudioClip clip;

    [Range(0f, 1f)] //slider min,max
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source; //variabel in Manager

}
