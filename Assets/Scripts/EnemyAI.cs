using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float movementSpeed = 3f;
    public float attackCooldown = 2f; // Adjust the cooldown time as needed
    private float timeSinceLastAttack;

    private Animator animator;

    public int maxHealth = 100; // Maximum health of the enemy
    private int currentHealth;

    private Transform player;

    private bool isMoving = true; // Flag to control movement

    //audio variable for death
    private AudioSource audioDestroy;
    private PlayerGun gun;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeSinceLastAttack = attackCooldown;
        animator = GetComponent<Animator>();
        currentHealth = maxHealth; // Initialize current health to max health
        //looks for the audioSource comp in the player
        audioDestroy = GetComponent<AudioSource>();
        gun = GetComponentInChildren<PlayerGun>();
        if (gun != null)
        {
            attackRange = 10f;
        }
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < detectionRange)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToPlayer, out hit, detectionRange))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    if (Vector3.Distance(transform.position, player.position) > attackRange)
                    {
                        MoveTowardsPlayer();
                    }
                    else
                    {
                        if (timeSinceLastAttack >= attackCooldown)
                        {
                            PerformAttack();
                            timeSinceLastAttack = 0f;
                        }
                    }
                }
            }
        }

        timeSinceLastAttack += Time.deltaTime;
    }

    void PerformAttack()
    {
        if (gun != null) // If the enemy has a gun, shoot
        {
            Debug.Log("Shooting");
            gun.Shoot();
        }
        else // If no gun, perform melee attack
        {
            MeleeAttack();
        }
    }

    void MeleeAttack()
    {
        // Implement melee attack logic
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            Debug.Log("Player Hit!!!");
            playerHealth.TakeDamage(10); // Adjust damage value as needed
        }
    }

    void MoveTowardsPlayer()
    {
        transform.LookAt(player);
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
    }

    //void AttackPlayer()
    //{
    //    // TODO: Implement attack logic
    //    // For example, reduce player's health
    //    PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
    //    if (playerHealth != null)
    //    {
    //        Debug.Log("Player Hit!!!");
    //        playerHealth.TakeDamage(10); // Adjust the damage value as needed
    //    }
    //}

    //public void TakeDamage(int damage)
    //{
    //    // Reduce the enemy's health
    //    currentHealth -= damage;

    //    // Check if the enemy is dead
    //    if (currentHealth <= 0)
    //    {
    //        movementSpeed = 0f;
    //        audioDestroy.enabled = true;
    //        audioDestroy.Play();
    //        animator.SetBool("isDead", true);
    //        Invoke("Death",2.5f);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            Destroy(other.gameObject);
            currentHealth -= 10;
            if (currentHealth <= 0)
            {
                movementSpeed = 0f;
                audioDestroy.enabled = true;
                audioDestroy.Play();
                animator.SetBool("dead", true);
                StartCoroutine(PostDeathActions());
            }
        }
    }

    IEnumerator PostDeathActions()
    {
        // Wait for the death animation to complete
        yield return new WaitForSeconds(1.5f);

        // Now that the animation is done, remove the game object from the screen
        Destroy(gameObject);
    }
}
