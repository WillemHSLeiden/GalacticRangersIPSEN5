using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private Text text;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Enemy")
        {
            GameObject collided = collider.gameObject;
            BehaviourStrategy enemy = collided.GetComponent<BehaviourStrategy>();
            TakeDamage((int)enemy.getDamage());
            Destroy(collider.gameObject);
            GameLogger.GetInstance().PlayerGotHit(new PlayerHitInfo(0, collided.name, enemy.getDamage()));
        }
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Score: " + GameLogger.GetInstance().GetScore();
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if(currentHealth <= 0)
        {
            //Game Over todo
        }
    }

    public void HealDamage(int health)
    {
        if(currentHealth + health > maxHealth) { 
            currentHealth += health;
            healthBar.SetHealth(currentHealth);
        }
        if (currentHealth + health <= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

}
