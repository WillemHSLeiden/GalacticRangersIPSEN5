using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingAsteroid : MonoBehaviour
{
    public Animator anim;


    public void Attack() {
        anim.SetTrigger("Charge");
    }

    public void FireLaser() {

    }
}
