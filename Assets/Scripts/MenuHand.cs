using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHand : MonoBehaviour
{
    private GameObject targetGameObject;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (targetGameObject != null) {
            transform.position = Vector3.Lerp(transform.position, targetGameObject.transform.position, 0.1f);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetGameObject.transform.rotation, 0.1f);
        }
    }

    public void SetParent(GameObject gameObject) {
        transform.SetParent(gameObject.transform);
        targetGameObject = gameObject;
    }

    public void SetTrigger(string trigger) {
        anim.SetTrigger(trigger);
    }

}
