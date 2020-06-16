using UnityEngine;

public interface BehaviourStrategy
{
    void setInActive();
    void setHealth(float health);
    float getHealth();
    void setDamage(float damage);
    float getDamage();
    void setSpeed(float speed);
    float getSpeed();
    void setPlayerObject(Transform player);
}

