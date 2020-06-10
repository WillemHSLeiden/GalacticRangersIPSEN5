using UnityEngine;

public interface BehaviourStrategy
{
    void setInActive();
    void setHealth(float health);
    void setDamage(float damage);
    void setSpeed(float speed);
    void setPlayerObject(Transform player);
}

