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
        Debug.Log("Player died");
        animator.SetBool("isDead", true);
        // Start the coroutine to handle post-death actions
        StartCoroutine(PostDeathActions());
    }

    IEnumerator PostDeathActions()
    {
        // Wait for 1.5 seconds
        yield return new WaitForSeconds(2.5f);

        // Remove the player from the screen
        gameObject.SetActive(false);

        // Display the game over screen
        //if (gameoverscreen != null)
        //{
        //    gameoverscreen.setactive(true);
        //}
    }
}