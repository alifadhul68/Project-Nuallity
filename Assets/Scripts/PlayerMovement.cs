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

    public ParticleSystem part;
    // Start is called before the first frame update
    void Start()
    {


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

        part.Play();
        part.enableEmission = true;
        float startTime = Time.time;
        while (Time.time - startTime < dashTime)
        {
            rb.velocity = dashDirection * dashSpeed;
            yield return null;
        }
        rb.velocity = Vector3.zero;
        part.enableEmission = false;
        part.Stop();
        isDashing = false;
    }
}
