using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnReticle : MonoBehaviour
{

    public Transform target;
    public Transform player;

    void Orbit()
    {
        if (target != null)
        {
            Vector3 direction = player.position - target.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.2f);
            transform.position = player.position;
        }
    }

    void LateUpdate()
    {

        Orbit();

    }

}
