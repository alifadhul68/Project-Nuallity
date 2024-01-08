using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBoost : MonoBehaviour
{
    [SerializeField] private float boostAmount = 20f; // Adjust as needed
    [SerializeField] private float duration = 15f;   // Adjust as needed
    private string powerupTitle = "Damage Boost";
    private string powerupDescription = "Your Gun feels More Powerful Temporarily.";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerGun playerMovement = PlayerGun.Instance;

            if (playerMovement != null)
            {
                // Show popup with information
                PopupManager.Instance.ShowPopup(powerupTitle, powerupDescription, 2.5f);

                // Apply speed boost
                playerMovement.ApplyDamageBoost(boostAmount, duration);

                // Destroy powerup object
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("error");
            }
        }
    }
}
