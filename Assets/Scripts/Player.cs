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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            BehaviourStrategy enemy = collision.gameObject.GetComponent<BehaviourStrategy>();
            enemy.getDamage
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
    }

}
