using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static int maxHealth = 50;
    public static int currentHealth = maxHealth;
    private Animator animator;
    private HealthBar healthBar;
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        GameObject healthBarObject = GameObject.Find("HealthBar");
        healthBar = healthBarObject.GetComponent<HealthBar>();
        if (healthBar == null)
        {
            Debug.LogError("HealthBar script not found in the scene.");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (healthBar != null)
        {
            healthBar.SetValue(currentHealth);
        }
        // TODO: Add logic for player taking damage, e.g., play a hurt animation

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // TODO: Implement player death logic, e.g., restart the level
        Debug.Log("Player died");
        animator.SetBool("isDead", true);
    }
}