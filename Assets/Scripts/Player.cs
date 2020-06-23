using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [SerializeField] private int maxHealth, currentHealth;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private Text text;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private Light damageLight;
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
            ChangeLightSettings(5.03f, 10.46f);
            TakeDamage((int)enemy.getDamage());
            Destroy(collider.gameObject);
            GameLogger.GetInstance().PlayerGotHit(new PlayerHitInfo(0, collided.name, enemy.getDamage()));
        }
    }

    // Update is called once per frame
    void Update()
    {
        int currentScore = GameLogger.GetInstance().GetScore();
        text.text = "Score: " + currentScore;
        finalScoreText.text = currentScore.ToString();
        LerpChangeLightSettings(0, 0, 0.05f);
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

    private void ChangeLightSettings(float range, float intensity) 
    {
        damageLight.range = range;
        damageLight.intensity = intensity;
    }

    private void LerpChangeLightSettings(float range, float intensity, float lerp) {
        damageLight.range = Mathf.Lerp(damageLight.range, range, lerp);
        damageLight.intensity = Mathf.Lerp(damageLight.intensity, intensity, lerp);
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
