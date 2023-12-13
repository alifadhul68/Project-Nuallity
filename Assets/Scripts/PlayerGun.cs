using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public static PlayerGun Instance;

    private float lastTimeShot = 0;

    [SerializeField]
    public Transform firingPoint;
    [SerializeField]
    GameObject projectilePrefab;
    [SerializeField]
    float fireRate;

    void Awake()
    {
        Instance = GetComponent<PlayerGun>();
    }

    // instantiates projectile on player input 
    public void Shoot()
    {
        if (lastTimeShot + fireRate <= Time.time)
        {
            lastTimeShot = Time.time;
            Instantiate(projectilePrefab, firingPoint.position, firingPoint.rotation);
        }
    }
}
