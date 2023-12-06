using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float movementSpeed = 3f;
    public float attackCooldown = 2f; // Adjust the cooldown time as needed
    private float timeSinceLastAttack;

    public int maxHealth = 100; // Maximum health of the enemy
    private int currentHealth;

    private Transform player;

    private bool isMoving = true; // Flag to control movement

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeSinceLastAttack = attackCooldown;
        currentHealth = maxHealth; // Initialize current health to max health
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < detectionRange)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, player.position - transform.position, out hit, detectionRange))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    // Check if the player is within attack range
                    if (Vector3.Distance(transform.position, player.position) > attackRange)
                    {
                        MoveTowardsPlayer();
                    }
                    else if (timeSinceLastAttack >= attackCooldown)
                    {
                        AttackPlayer();
                        timeSinceLastAttack = 0f;
                    }
                }
            }
        }

        timeSinceLastAttack += Time.deltaTime;
    }

    void MoveTowardsPlayer()
    {
        transform.LookAt(player);
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
    }

    void AttackPlayer()
    {
        // TODO: Implement attack logic
        // For example, reduce player's health
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            Debug.Log("Player Hit!!!");
            playerHealth.TakeDamage(10); // Adjust the damage value as needed
        }
    }

    public void TakeDamage(int damage)
    {
        // Reduce the enemy's health
        currentHealth -= damage;

        // Check if the enemy is dead
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // TODO: Implement death logic
        // For example, play death animation, spawn loot, or destroy the enemy object
        Destroy(gameObject);
    }
}
