using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    
    private Camera cam;

    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float gunAttackRange = 5f;
    public float movementSpeed = 3f;
    public float attackCooldown = 2f; // Adjust the cooldown time as needed
    private float timeSinceLastAttack;

    private Animator animator;

    public float maxHealth = 100; // Maximum health of the enemy
    private float currentHealth;

    private Transform player;

    private bool isMoving = true; // Flag to control movement

    public GameObject[] powerupPrefabs;
    public GameObject coin;
    private bool powerupsLoaded = false;
    //audio variable for death
    private AudioSource audioDestroy;
    private Gun gun;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeSinceLastAttack = attackCooldown;
        animator = GetComponent<Animator>();
        currentHealth = maxHealth; // Initialize current health to max health
        //looks for the audioSource comp in the player
        audioDestroy = GetComponent<AudioSource>();
        gun = GetComponentInChildren<Gun>();
        if (gun != null)
        {
            attackRange = gunAttackRange;
        }
        if (!powerupsLoaded)
        {
            LoadPowerupPrefabs();
            powerupsLoaded = true;
        }
        coin = Resources.Load<GameObject>("Prefabs/Coin");
    }

    void Update()
    {
        if(PlayerHealth.currentHealth > 0)
        {
            slider.transform.rotation = cam.transform.rotation;

            if (Vector3.Distance(transform.position, player.position) < detectionRange)
            {
                RotateTowardsPlayer(); // Continuously rotate towards the player

                Vector3 directionToPlayer = (player.position - transform.position).normalized;
                RaycastHit hit;
                if (Physics.Raycast(transform.position, directionToPlayer, out hit, detectionRange))
                {
                    if (hit.collider.CompareTag("PlayerDetector"))
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
    }

    void RotateTowardsPlayer()
    {
        if (currentHealth <= 0) // make sure not to trigger death animation and sound more than once
            return;
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * movementSpeed);
    }

    void PerformAttack()
    {
        if (currentHealth <= 0) // make sure not to trigger death animation and sound more than once
            return;
        if (gun != null) // If the enemy has a gun, shoot
        {
            //Debug.Log("Shooting");
            gun.Shoot();
        }
        else // If no gun, perform melee attack
        {
            MeleeAttack();
        }
    }

    void LoadPowerupPrefabs()
    {
        // Load powerup prefabs from the Resources folder
        powerupPrefabs = Resources.LoadAll<GameObject>("Prefabs/Powerups");
    }

    void MeleeAttack()
    {
        if (currentHealth <= 0) // make sure not to trigger death animation and sound more than once
            return;
        // Implement melee attack logic
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            //Debug.Log("Player Hit!!!");
            playerHealth.TakeDamage(10); // Adjust damage value as needed
        }
    }

    void MoveTowardsPlayer()
    {
        if (currentHealth <= 0) // make sure not to trigger death animation and sound more than once
            return;

        transform.LookAt(player);
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
    }

 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {

            if (currentHealth <= 0) // make sure not to trigger death animation and sound more than once
                return;

            // handle taking damage
            Destroy(other.gameObject);
            currentHealth -= other.gameObject.GetComponentInChildren<Projectile>().damage;
            slider.value = currentHealth / maxHealth;
            if (currentHealth <= 0)
            {
                slider.gameObject.SetActive(false);
                movementSpeed = 0f;
                audioDestroy.enabled = true;
                audioDestroy.Play();
                animator.SetBool("dead", true);
                StartCoroutine(PostDeathActions());
                Scoring.Instance.AddPointToEnemiesKilled();
            }
        }
    }

    IEnumerator PostDeathActions()
    {
        // Wait for the death animation to complete
        yield return new WaitForSeconds(1.5f);

        // Check if the scene name is "Endless"
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Endless")
        {
            // Determine whether to drop a coin or powerup (25% chance for each)
            float randomValue = UnityEngine.Random.value;
            if (randomValue < 0.25f)
            {
                // Choose a random powerup prefab from the list
                int randomPowerupIndex = UnityEngine.Random.Range(0, powerupPrefabs.Length);
                GameObject powerupPrefab = powerupPrefabs[randomPowerupIndex];

                // Instantiate the powerup at the enemy's position
                Instantiate(powerupPrefab, transform.position, Quaternion.identity);
            }
            else if (randomValue < 0.5f)
            {
                // Instantiate the coin prefab at the enemy's position
                Instantiate(coin, transform.position + new Vector3(0f,1f,0f), Quaternion.Euler(90f,0f,0f));
            }
        }
        else
        {
            // For other scenes, drop a powerup with a 25% chance
            if (UnityEngine.Random.value < 0.25f)
            {
                // Choose a random powerup prefab from the list
                int randomPowerupIndex = UnityEngine.Random.Range(0, powerupPrefabs.Length);
                GameObject powerupPrefab = powerupPrefabs[randomPowerupIndex];

                // Instantiate the powerup at the enemy's position
                Instantiate(powerupPrefab, transform.position, Quaternion.identity);
            }
        }

        // Now that the animation is done, remove the game object from the screen
        Destroy(gameObject);
    }



}
