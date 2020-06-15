using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private HealthBar healthBar;


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
            BehaviourStrategy enemy = collider.gameObject.GetComponent<BehaviourStrategy>();
            TakeDamage((int)enemy.getDamage());
            Destroy(collider.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
