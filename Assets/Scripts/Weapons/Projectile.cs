using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float startTime;

    public float travelTime;
    public float speed;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        MoveProjectile();
        handleProjectileRange();
    }

    // handles projectile movement
    void MoveProjectile()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    // handles projectile disappearing after certain amount of time passes
    void handleProjectileRange()
    {
        if (Time.time - startTime > travelTime)
        {
            Destroy(gameObject);
        }
    }
}
