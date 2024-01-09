using System.Collections;
using UnityEngine;

public class PlayerOxygen : MonoBehaviour
{
    public static int maxOxygen = 50;
    public static int currentOxygen = maxOxygen;
    private HealthBar healthBar;
    private OxygenMeter oxygenMeter;
    private Transform player;
    private static bool isInOxygenArea = false;
    private Coroutine oxygenDrainCoroutine;
    private Coroutine oxygenIncreaseCoroutine; 

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentOxygen = maxOxygen;
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
            isInOxygenArea = true;
            oxygenDrainCoroutine = StartCoroutine(DrainOxygenContinuously());
            StopCoroutine(oxygenIncreaseCoroutine);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player out of oxygen area");
            // Player exited the trigger, stop draining oxygen
            StopCoroutine(oxygenDrainCoroutine);

            // Start increasing oxygen over time
            oxygenIncreaseCoroutine = StartCoroutine(IncreaseOxygenGradually());
        }
    }

    private IEnumerator DrainOxygenContinuously()
    {
        while (true)
        {
            // Drain oxygen every second (adjust the time as needed)
            yield return new WaitForSeconds(1f);
            DrainOxygen();
        }
    }

    private void DrainOxygen()
    {
        if (currentOxygen > 0)
        {
            currentOxygen -= 5;
        }

        Debug.Log("currentOxygen is" + currentOxygen);
        if (oxygenMeter != null)
        {
            oxygenMeter.SetValue(currentOxygen);
        }

        if (currentOxygen <= 0)
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            Debug.Log("Player out of Oxygen!!!");
            if (PlayerHealth.currentHealth > 0)
            {
                playerHealth.TakeDamage(5);
            }
        }
    }

    private IEnumerator IncreaseOxygenGradually()
    {
        float timeToIncrease = 2f; // Adjust as needed
        float elapsedTime = 0f;

        int initialOxygen = currentOxygen;
        int targetOxygen = maxOxygen;

        while (elapsedTime < timeToIncrease)
        {
            yield return null;
            elapsedTime += Time.deltaTime;

            // Gradually increase oxygen
            currentOxygen = (int)Mathf.Lerp(initialOxygen, targetOxygen, elapsedTime / timeToIncrease);

            if (oxygenMeter != null)
            {
                oxygenMeter.SetValue(currentOxygen);
            }
        }

        // Ensure oxygen is at max after the increase
        currentOxygen = maxOxygen;
        oxygenMeter.SetValue(currentOxygen);

        // Player is no longer in the oxygen area
        isInOxygenArea = false;
    }

}
