using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenBubble : MonoBehaviour
{
    private OxygenMeter oxygenMeter;

    void Start()
    {
        GameObject oxygenMeterObject = GameObject.Find("OxygenMeter");
        oxygenMeter = oxygenMeterObject.GetComponent<OxygenMeter>();
        if (oxygenMeter == null)
        {
            Debug.LogError("oxygenMeter script not found in the scene.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerOxygen.currentOxygen = PlayerOxygen.maxOxygen;
            oxygenMeter.SetValue(PlayerOxygen.currentOxygen);

            // Deactivate the GameObject for 6 seconds
            StartCoroutine(DeactivateAndReactivate(10f));
        }
        else
        {
            Debug.Log("error");
        }
    }

    private IEnumerator DeactivateAndReactivate(float delay)
    {
        // Deactivate the child GameObject
        transform.GetChild(0).gameObject.SetActive(false);

        // Wait for the specified delay
        yield return new WaitForSeconds(delay);
        Debug.Log("Delay ended");

        // Reactivate the child GameObject
        transform.GetChild(0).gameObject.SetActive(true);
    }
}