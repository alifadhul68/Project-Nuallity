using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    [SerializeField] private float boostAmount = 3f; // Adjust as needed
    [SerializeField] private float duration = 15f;   // Adjust as needed
    private string powerupTitle = "Speed Boost";
    private string powerupDescription = "Temporary Boosts Player speed.";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                // Show popup with information
                PopupManager.Instance.ShowPopup(powerupTitle, powerupDescription);

                // Apply speed boost
                playerMovement.ApplySpeedBoost(boostAmount, duration);

                // Destroy powerup object
                Destroy(gameObject);
            }
        }
    }
}