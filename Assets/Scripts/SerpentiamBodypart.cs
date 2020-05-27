using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerpentiamBodypart : MonoBehaviour
{
    public SerpentiamBody serpentiamBody = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addSerpentiamHit() {
        if (serpentiamBody != null)
            if (serpentiamBody.boss.state != Boss.BossState.VULNERABLE)
                serpentiamBody.AddBodyHit();
            else {
                serpentiamBody.damage();
            }
        else
            Debug.LogError("Serpentiam not found!");
    }

}
