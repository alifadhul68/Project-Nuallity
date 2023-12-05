using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    public float dashSpeed = 10f;
    private bool isDashing = false;
    [SerializeField]
    public float dashTime = 0.5f;
    
    [SerializeField]
    public GameObject SpeedBarier;

    public ParticleSystem part;
    private ParticleSystem.EmissionModule partEmit;

    // Start is called before the first frame update
    void Start()
    {
        partEmit = part.emission;

    }

    // Update is called once per frame
    void Update()
    {
        handlePlayerInput();
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

    IEnumerator Dash()
    {
        isDashing = true;
        float _horizontal = Input.GetAxis("Horizontal");
        float _vertical = Input.GetAxis("Vertical");
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 dashDirection = new Vector3(_horizontal, 0, _vertical);

        
        partEmit.enabled = true;
        
        float startTime = Time.time;
        while (Time.time - startTime < dashTime)
        {
            part.Emit(2);
            rb.velocity = dashDirection * dashSpeed;
            yield return null;
        }
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
}
