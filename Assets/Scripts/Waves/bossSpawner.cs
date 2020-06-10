using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject serpentiam;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(serpentiam, Vector3.zero, Quaternion.identity);
    }
}


// VERWIJDER DIT SCRIPT DIT IS PUUR VOOR DE DEMO
// VERWIJDER DIT SCRIPT DIT IS PUUR VOOR DE DEMO
// VERWIJDER DIT SCRIPT DIT IS PUUR VOOR DE DEMO