using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float startTime;
    private GameObject shooter;

    public float travelTime;
    public float speed;
    public float damage;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        MoveProjectile();
        HandleProjectileRange();
    }

    void MoveProjectile()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void HandleProjectileRange()
    {
        if (Time.time - startTime > travelTime)
        {
            Destroy(gameObject);
        }
    }

    public void SetShooter(GameObject shooter)
    {
        this.shooter = shooter;
        StartCoroutine(IgnoreShooterCollision());
    }

    IEnumerator IgnoreShooterCollision()
    {
        if (shooter != null)
        {
            Collider shooterCollider = shooter.GetComponent<Collider>();
            Collider projectileCollider = GetComponent<Collider>();

            if (shooterCollider != null && projectileCollider != null)
            {
                Physics.IgnoreCollision(projectileCollider, shooterCollider, true);
            }

            yield return new WaitForSeconds(0.1f); // Adjust time as needed

            if (shooterCollider != null && projectileCollider != null)
            {
                Physics.IgnoreCollision(projectileCollider, shooterCollider, false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != shooter && other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10);
                Destroy(gameObject); // Destroy the projectile
            }
        }
    }
}
