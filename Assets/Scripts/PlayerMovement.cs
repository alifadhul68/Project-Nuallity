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

    /*[SerializeField]
    public float attackCooldown = 1f; // Adjust the cooldown time as needed
    private float timeSinceLastAttack;

    [SerializeField]
    private float attackRange = 2f; // The range of the attack
    [SerializeField]
    private LayerMask enemyLayer; // Layer to detect enemies
    [SerializeField]
    private int attackDamage = 10; // Damage dealt by the attack
    */

    [SerializeField]
    private LayerMask groundMask;
    private Camera cam;

    public ParticleSystem part;
    private ParticleSystem.EmissionModule partEmit;

    //audio variable for dash
    private AudioSource audioDash;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        originSpeed = movementSpeed;
        partEmit = part.emission;
        //timeSinceLastAttack = attackCooldown;
        //looks for the audioSource comp in the player
        audioDash = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        handlePlayerInput();
        handlePlayerRotation();
        HandleShootInput();

        // Check if the player can attack and trigger attack when the player presses a designated key (e.g., Space)
        /*if (Input.GetKeyDown(KeyCode.Mouse0) && timeSinceLastAttack >= attackCooldown)
        {
            Attack();
            timeSinceLastAttack = 0f; // Reset the attack cooldown timer
        }

        // Update the attack cooldown timer
        timeSinceLastAttack += Time.deltaTime;*/

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

    /* void Attack()
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

    }*/

    void handlePlayerRotation()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, groundMask))
        {
            // Trigonometry calculations to cause character to aim at where mouse is pointing relative to projectile height
            // rather than pointing at the ground or slightly above the cursor

            // opposite side length
            Vector3 hitPoint = hitInfo.point;
            Vector3 playerDirection = new Vector3(hitInfo.point.x, -0.5f, hitInfo.point.z);
            float oppositeLength = Vector3.Distance(playerDirection, hitPoint);

            // radian of angle between hypotenuse and adjacent sides
            float rad = cam.transform.rotation.eulerAngles.x * Mathf.Deg2Rad;

            // calculating hypotenuse length using SOH formula
            float hypotenuseLength = oppositeLength / Mathf.Sin(rad);

            // final position
            Vector3 position = ray.GetPoint(hitInfo.distance - hypotenuseLength);

            // adjust character facing direction
            var direction = position - transform.position;
            direction.y = 0;
            transform.forward = direction;

            //Debug.DrawRay(transform.position, position - transform.position, Color.red);
        }
    }

    void HandleShootInput()
    {
        if (Input.GetButton("Fire1"))
        {
            PlayerGun.Instance.Shoot();
        }
        if (Input.GetButton("Reload"))
        {
            PlayerGun.Instance.Reload();
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
        //Gizmos.DrawWireSphere(transform.position, attackRange);
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