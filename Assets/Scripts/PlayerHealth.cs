using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private Animator animator;
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

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