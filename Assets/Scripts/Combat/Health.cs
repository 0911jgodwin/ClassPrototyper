using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    public bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    public bool IsDead()
    {
        return isDead;
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        if (currentHealth == 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        if (isDead) return;

        isDead = true;
        GetComponent<Animator>().SetTrigger("die");
    }
}
