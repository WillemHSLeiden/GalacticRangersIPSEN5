using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICameraFinder : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        Camera camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        canvas.worldCamera = camera;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
