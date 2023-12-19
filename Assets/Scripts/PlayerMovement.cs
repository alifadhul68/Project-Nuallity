using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;
    private float originSpeed;
    [SerializeField]
    public float dashSpeed = 10f;
    private bool isDashing = false;
    [SerializeField]
    public float dashTime = 0.5f;

    [SerializeField]
    public GameObject SpeedBarier;

    [SerializeField]
    public float attackCooldown = 1f; // Adjust the cooldown time as needed
    private float timeSinceLastAttack;

    [SerializeField]
    private float attackRange = 2f; // The range of the attack
    [SerializeField]
    private LayerMask enemyLayer; // Layer to detect enemies
    [SerializeField]
    private int attackDamage = 10; // Damage dealt by the attack

    public ParticleSystem part;
    private ParticleSystem.EmissionModule partEmit;

    //audio variable for dash
    private AudioSource audioDash;
    // Start is called before the first frame update
    void Start()
    {
        originSpeed = movementSpeed;
        partEmit = part.emission;
        timeSinceLastAttack = attackCooldown;
        //looks for the audioSource comp in the player
        audioDash = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        handlePlayerInput();

        // Check if the player can attack and trigger attack when the player presses a designated key (e.g., Space)
        if (Input.GetKeyDown(KeyCode.Mouse0) && timeSinceLastAttack >= attackCooldown)
        {
            Attack();
            timeSinceLastAttack = 0f; // Reset the attack cooldown timer
        }

        // Update the attack cooldown timer
        timeSinceLastAttack += Time.deltaTime;

        // Dash mechanism
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    void handlePlayerInput()
    {
        float _horizontal = Input.GetAxis("Horizontal");
        float _vertical = Input.GetAxis("Vertical");

        Vector3 _movement = new Vector3(_horizontal, 0, _vertical);
        transform.Translate(_movement * movementSpeed * Time.deltaTime, Space.World);
    }

    void Attack()
    {

        // Implementing attack logic
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

        foreach (var enemyCollider in hitEnemies)
        {
            // Get the EnemyAI component from the collider
            EnemyAI enemyAI = enemyCollider.GetComponent<EnemyAI>();

            // Check if the enemyAI component exists
            if (enemyAI != null)
            {
                // Apply damage to the enemy
                enemyAI.TakeDamage(attackDamage);
                Debug.Log("Enemy Hit!!!");
            }
        }

    }


    IEnumerator Dash()
    {
        isDashing = true;
        float _horizontal = Input.GetAxis("Horizontal");
        float _vertical = Input.GetAxis("Vertical");
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 dashDirection = new Vector3(_horizontal, 0, _vertical);

        partEmit.enabled = true;
        //enable the audio and play it
        audioDash.enabled = true;
        audioDash.Play();
        float startTime = Time.time;
        while (Time.time - startTime < dashTime)
        {
            part.Emit(2);
            rb.velocity = dashDirection * dashSpeed;
            yield return null;
        }
        audioDash.enabled = false;
        rb.velocity = Vector3.zero;
        partEmit.enabled = false;
        isDashing = false;
    }

    public void ApplySpeedBoost(float boostAmount, float duration)
    {
        // Apply the speed boost to the player
        StartCoroutine(SpeedBoostRoutine(boostAmount, duration));
    }

    private IEnumerator SpeedBoostRoutine(float boostAmount, float duration)
    {
        SpeedBarier.SetActive(true);
        float originalSpeed = movementSpeed;
        movementSpeed += boostAmount;

        yield return new WaitForSeconds(duration);

        SpeedBarier.SetActive(false);
        movementSpeed = originalSpeed;
    }

    // Draw the attack range in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("slow"))
        {            
            if (other != null)
            {
                // Reduce the player's speed and dash speed
                movementSpeed *= 0.5f;
                dashSpeed *= 0.5f;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("slow"))
        {
            if (other != null)
            {
                //reset the player's speed and dash speed
                movementSpeed = originSpeed;
                dashSpeed *= 2f;
            }
        }
    }
}