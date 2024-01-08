using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOxygen : MonoBehaviour
{
    public static int maxOxygen = 50;
    public static int currentOxygen = maxOxygen;
    private HealthBar healthBar;
    private OxygenMeter oxygenMeter;
    private Animator animator;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentOxygen = maxOxygen;
        animator = GetComponent<Animator>();
        GameObject healthBarObject = GameObject.Find("HealthBar");
        healthBar = healthBarObject.GetComponent<HealthBar>();
        GameObject oxygenMeterObject = GameObject.Find("OxygenMeter");
        oxygenMeter = oxygenMeterObject.GetComponent<OxygenMeter>();
        if (healthBar == null)
        {
            Debug.LogError("HealthBar script not found in the scene.");
        }
        if (oxygenMeter == null)
        {
            Debug.LogError("oxygenMeter script not found in the scene.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player entered the trigger, drain oxygen
            DrainOxygen();
        }
    }

    public void DrainOxygen()
    {
        currentOxygen -= 5;
        Debug.LogError("currentOxygen is" + currentOxygen);
        if (oxygenMeter != null)
        {
            oxygenMeter.SetValue(currentOxygen);
        }

        if (currentOxygen <= 0)
        {
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            Debug.Log("Player out of Oxygen!!!");
            playerHealth.TakeDamage(5); 
        }
    }

}
