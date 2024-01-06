using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField]
    private Transform firingPoint;
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private float fireRate;
    [SerializeField]
    private int maxAmmo;
    [SerializeField]
    private float reloadTime;

    private float nextTimeToShoot = 0;
    private int ammoInMag;

    private void Start()
    {
        ammoInMag = maxAmmo;
    }

    public void Shoot()
    {
        if (ammoInMag <= 0)
        {
            Reload();
            return;
        }

        if (Time.time >= nextTimeToShoot)
        {
            nextTimeToShoot = Time.time + fireRate;
            GameObject projectile = Instantiate(projectilePrefab, firingPoint.position, firingPoint.rotation);
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            if (projectileScript != null)
            {
                projectileScript.SetShooter(transform.parent.gameObject); // Set this GameObject as the shooter
            }
            ammoInMag -= 1;
        }
    }

    public void Reload()
    {
        if (ammoInMag < maxAmmo)
        {
            nextTimeToShoot = Time.time + reloadTime;
            ammoInMag = maxAmmo;
        }
    }

}
