using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{

    public Slider playerSlider;
    public PlayerController player;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public override void TakeDamage(int damage)
    {
        currentHealth = Mathf.Clamp(Mathf.Max(currentHealth - damage, 0), 0, 100);
        playerSlider.value = currentHealth;
        if (currentHealth == 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        // What is dead may never die
        if (isDead) return;

        isDead = true;
        player.enabled = false;
        GetComponent<Animator>().SetTrigger("die");
    }
}
