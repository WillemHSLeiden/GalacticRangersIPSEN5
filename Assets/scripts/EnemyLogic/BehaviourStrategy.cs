using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BehaviourStrategy
{
    void onTriggerEnter();
    void setInActive();
    void setHealth(float health);
    void setDamage(float health);
}

