using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionAction : MonoBehaviour
{
    [System.Serializable]
    public class hitEvents : UnityEvent { }

    [SerializeField]
    public hitEvents hitEvent, blockEvent;

    private bool hit = false;

    public SerpentiamBody serpentiamBody = null;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "laser") {
            if (!hit)
                hitEvent.Invoke();
            else
                blockEvent.Invoke();
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            hitEvent.Invoke();
        }
    }

    public void hitSwitch() {
        if (hit)
            hit = true;
        else
            hit = false;
    }

    public void damageParent() {

    }

    public void addSerpentiamHit() {
        if (serpentiamBody != null)
            serpentiamBody.AddBodyHit();
        else
            Debug.LogError("Serpentiam not found!");
    }
}
