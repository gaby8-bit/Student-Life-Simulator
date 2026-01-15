using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    // Mărim viața la 5000 pentru ca 1 damage să fie insignifiant (0.02%)
    public int maxHealth = 5000; 
    private int currentHealth;
    public Slider hpSlider;

    void Start()
    {
        currentHealth = maxHealth;
        if (hpSlider != null)
        {
            hpSlider.maxValue = maxHealth;
            hpSlider.value = maxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (hpSlider != null) hpSlider.value = currentHealth;

        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        Debug.Log("Boss-ul a fost învins!");
        Destroy(gameObject);
    }
}