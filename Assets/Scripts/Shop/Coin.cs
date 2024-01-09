using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private static TMP_Text coinCount;
    public static int coins = 0;

    private void Start()
    {
        // Find the Coin HUD canvas
        GameObject coinHUDCanvas = GameObject.Find("Coin HUD");

        if (coinHUDCanvas != null)
        {
            // Find the Coin Count text component within the Coin HUD canvas
            coinCount = coinHUDCanvas.GetComponentInChildren<TMP_Text>();

            if (coinCount == null)
            {
                Debug.LogError("Coin Count text component not found under Coin HUD!");
            }
        }
        else
        {
            Debug.LogError("Coin HUD canvas not found in the scene!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectCoin();
            Destroy(gameObject);
        }

    }

    private void CollectCoin()
    {
        
        coins++; // Increment the coin count
        Debug.Log("Current coin count" + coins);
        UpdateCoinCountText();
        // You can add any additional logic related to collecting coins here
    }

    public static void UpdateCoinCountText()
    {
        // Update the TMP_Text with the new coin count
        coinCount.text = coins + "C";
    }
}