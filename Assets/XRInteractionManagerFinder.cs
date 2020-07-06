using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRInteractionManagerFinder : MonoBehaviour
{
    XRInteractionManager xrInteractionManager;
    [SerializeField] XRSimpleInteractable xrSimpleInteractable;

    // Start is called before the first frame update
    void Start()
    {
        xrInteractionManager = GameObject.FindGameObjectWithTag("XRInteractionManager").GetComponent<XRInteractionManager>();
        xrSimpleInteractable.interactionManager = xrInteractionManager;
    }
}
