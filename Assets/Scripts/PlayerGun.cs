using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public static PlayerGun Instance;

    private float nextTimeToShoot = 0;

    [SerializeField]
    public Transform firingPoint;
    [SerializeField]
    GameObject projectilePrefab;
    [SerializeField]
    float fireRate;
    [SerializeField]
    int maxAmmo;
    int ammoInMag;
    [SerializeField]
    float reloadTime;
    

    void Awake()
    {
        Instance = GetComponent<PlayerGun>();
    }
    private void Start()
    {
        
    }
    // instantiates projectile on player input 
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
            Instantiate(projectilePrefab, firingPoint.position, firingPoint.rotation);
            ammoInMag -= 1;
        }
    }

    public void Reload()
    {
        if (ammoInMag < maxAmmo)
        {
            Debug.Log("reload triggered");
            nextTimeToShoot = Time.time + reloadTime;
            ammoInMag = maxAmmo;
        }
    }
}
