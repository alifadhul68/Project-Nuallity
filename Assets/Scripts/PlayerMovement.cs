using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;
    private float originSpeed;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    public float dashSpeed = 10f;
    private bool isDashing = false;
    [SerializeField]
    public float dashTime = 0.5f;

    private Deflect shield;

    [SerializeField]
    public GameObject SpeedBarier;

    private Animator animator;
    private bool isMoving;
    private PlayerGun gun;

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


    //audio variables for dash and hit
    private AudioSource audioC;
    public AudioClip audioDash;

    private Rigidbody rb;

    [SerializeField] private float dashCooldown = 2f; // Cooldown duration in seconds
    private float lastDashTime = -Mathf.Infinity; // Initialize to a time in the past

    // Start is called before the first frame update
    void Start()
    {
        shield = GetComponentInChildren<Deflect>();
        gun = GetComponentInChildren<PlayerGun>();
        cam = Camera.main;
        originSpeed = movementSpeed;
        partEmit = part.emission;
        //timeSinceLastAttack = attackCooldown;
        //looks for the audioSource comp in the player
        audioC = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            if(PlayerHealth.currentHealth > 0)
            {
                handlePlayerInput();
                HandleShootInput();
                if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastDashTime + dashCooldown && !isDashing)
                {
                    StartCoroutine(Dash());
                }
            }
        }

        // Check if the player can attack and trigger attack when the player presses a designated key (e.g., Space)
        /*if (Input.GetKeyDown(KeyCode.Mouse0) && timeSinceLastAttack >= attackCooldown)
        {
            Attack();
            timeSinceLastAttack = 0f; // Reset the attack cooldown timer
        }

        // Update the attack cooldown timer
        timeSinceLastAttack += Time.deltaTime;*/

        // Dash mechanism

    }

    public void EditSpeed(float speed)
    {
        this.movementSpeed += speed;
        this.dashSpeed += speed;
    }

    void handlePlayerInput()
    {
        float _horizontal = Input.GetAxis("Horizontal");
        float _vertical = Input.GetAxis("Vertical");
        bool isMovingNow = Mathf.Abs(_horizontal) > 0.1f || Mathf.Abs(_vertical) > 0.1f;
        Vector3 _movement = new Vector3(_horizontal, 0, _vertical).normalized * movementSpeed;

        // Apply movement using Rigidbody
        if (isMovingNow)
        {
            // Move the player using Rigidbody
            rb.velocity = new Vector3(_movement.x, rb.velocity.y, _movement.z);

            // Update rotation to face the movement direction
            RotateTowardsMovementDirection(_movement);
        }
        else
        {
            // Stop the player's movement
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

        if (isMovingNow != isMoving)
        {
            isMoving = isMovingNow;
            animator.SetBool("isRunning", isMoving);
        }
    }

    //void handlePlayerInput()
    //{
    //    float _horizontal = Input.GetAxis("Horizontal");
    //    float _vertical = Input.GetAxis("Vertical");
    //    bool isMovingNow = Mathf.Abs(_horizontal) > 0.1f || Mathf.Abs(_vertical) > 0.1f;
    //    Vector3 _movement = new Vector3(_horizontal, 0, _vertical);
    //    transform.Translate(_movement * movementSpeed * Time.deltaTime, Space.World);

    //    // Update rotation to face the movement direction if moving
    //    if (isMovingNow)
    //    {
    //        RotateTowardsMovementDirection(_movement);
    //    }

    //    if (isMovingNow != isMoving)
    //    {
    //        isMoving = isMovingNow;
    //        animator.SetBool("isRunning", isMoving);
    //    }
    //}

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

    //void handlePlayerRotation()
    //{
    //    Ray ray = cam.ScreenPointToRay(Input.mousePosition);

    //    if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, groundMask))
    //    {
    //        // Trigonometry calculations to cause character to aim at where mouse is pointing relative to projectile height
    //        // rather than pointing at the ground or slightly above the cursor

    //        // opposite side length
    //        Vector3 hitPoint = hitInfo.point;
    //        Vector3 playerDirection = new Vector3(hitInfo.point.x, -0.5f, hitInfo.point.z);
    //        float oppositeLength = Vector3.Distance(playerDirection, hitPoint);

    //        // radian of angle between hypotenuse and adjacent sides
    //        float rad = cam.transform.rotation.eulerAngles.x * Mathf.Deg2Rad;

    //        // calculating hypotenuse length using SOH formula
    //        float hypotenuseLength = oppositeLength / Mathf.Sin(rad);

    //        // final position
    //        Vector3 position = ray.GetPoint(hitInfo.distance - hypotenuseLength);

    //        // adjust character facing direction
    //        var direction = position - transform.position;
    //        direction.y = 0;
    //        transform.forward = direction;

    //        Debug.DrawRay(transform.position, position - transform.position, Color.red);
    //    }
    //}
    private void FixedUpdate()
    {
        if (gameObject.transform.position.y <= -30)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    void HandleShootInput()
    {
        if (Input.GetButton("Fire1"))
        {
            ShootInMovementDirection();
            gun.Shoot();
        }
        if (Input.GetButton("Reload"))
        {
            gun.Reload();
        }
    }
    void ShootInMovementDirection()
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

            Debug.DrawRay(transform.position, position - transform.position, Color.red);
        }
    }

    void RotateTowardsMovementDirection(Vector3 _movement)
    {
        if (_movement.magnitude > 0.1f)
        {
            Quaternion newRotation = Quaternion.LookRotation(_movement);
            rb.rotation = Quaternion.Slerp(rb.rotation, newRotation, Time.deltaTime * rotationSpeed);
        }
    }

    //void RotateTowardsMovementDirection(Vector3 movementDirection)
    //{
    //    if (movementDirection != Vector3.zero)
    //    {
    //        Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
    //        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
    //    }
    //}

    IEnumerator Dash()
    {
        isDashing = true;
        lastDashTime = Time.time; // Set the timestamp for the last dash

        float _horizontal = Input.GetAxis("Horizontal");
        float _vertical = Input.GetAxis("Vertical");
        Vector3 dashDirection = new Vector3(_horizontal, 0, _vertical).normalized;

        partEmit.enabled = true;
        // Enable the audio and play it
        audioC.clip = audioDash;
        audioC.enabled = true;
        audioC.pitch = 3f;
        audioC.Play();

        animator.SetBool("isRolling", true);

        float startTime = Time.time;
        while (Time.time - startTime < dashTime)
        {
            part.Emit(2);
            rb.velocity = dashDirection * dashSpeed; // Apply dash velocity
            yield return null;
        }

        animator.SetBool("isRolling", false);
        audioC.enabled = false;
        rb.velocity = Vector3.zero; // Stop the dash
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
        if (other.CompareTag("Projectile"))
        {
            HandleProjectileCollision(other);
        }
        else if (other.CompareTag("slow"))
        {
            ApplySlowEffect(other);
        }
    }
    private void HandleProjectileCollision(Collider projectileCollider)
    {
        Projectile projectile = projectileCollider.GetComponent<Projectile>();

        if (projectile == null || projectile.shooter == this.gameObject)
        {
            return; // Exit if there's no Projectile component or if this gameObject is the shooter
        }

        DeflectProjectile(projectileCollider);
    }

    private void DeflectProjectile(Collider projectileCollider)
    {
        Rigidbody projectileRb = projectileCollider.GetComponent<Rigidbody>();
        if (projectileRb != null && shield.isDeflecting)
        {
            // Calculate deflection direction, for example, back to where it came from
            Vector3 deflectionDirection = -projectileCollider.transform.forward;
            float deflectionForce = 30f; // Adjust the force as needed

            // Apply the deflection force
            projectileRb.velocity = deflectionDirection * deflectionForce;
        }
    }

    private void ApplySlowEffect(Collider other)
    {
        if (other != null) // This check might be redundant as 'other' should always be non-null in OnTriggerEnter
        {
            // Reduce the player's speed and dash speed
            movementSpeed *= 0.5f;
            dashSpeed *= 0.5f;
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