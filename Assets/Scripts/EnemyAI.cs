using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float movementSpeed = 3f;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Check if the player is within detection range
        if (Vector3.Distance(transform.position, player.position) < detectionRange)
        {
            // Perform raycasting to detect obstacles between the enemy and the player
            RaycastHit hit;
            if (Physics.Raycast(transform.position, player.position - transform.position, out hit, detectionRange))
            {
                // Check if the ray hit the player
                if (hit.collider.CompareTag("Player"))
                {
                    // Player is in line of sight, move towards the player
                    MoveTowardsPlayer();

                    // Check if the player is within attack range
                    if (Vector3.Distance(transform.position, player.position) < attackRange)
                    {
                        // TODO: Implement attack logic
                        Attack();
                    }
                }
            }
        }
    }

    void MoveTowardsPlayer()
    {
        // Move the enemy towards the player
        transform.LookAt(player);
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
    }

    void Attack()
    {
        // TODO: Implement attack logic
        // This could include dealing damage to the player, playing an attack animation, etc.
    }
}
