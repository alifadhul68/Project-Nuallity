using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    [SerializeField] private float boostAmount = 3f; // Adjust as needed
    [SerializeField] private float duration = 15f;   // Adjust as needed

    [SerializeField] private GameObject popupUI;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                // Show popup with information
                ShowPopup();

                // Apply speed boost
                playerMovement.ApplySpeedBoost(boostAmount, duration);

                // Destroy powerup object
                Destroy(gameObject);
            }
        }
    }

    private void ShowPopup()
    {
        // Activate the popup
        popupUI.SetActive(true);

        // Set title and description
        title.text = "Speed Boost";
        description.text = "Increases speed for a duration.";

        // Start a coroutine to hide the popup after a delay
        StartCoroutine(HidePopupAfterDelay());
    }

    private IEnumerator HidePopupAfterDelay()
    {
        Debug.Log("Waiting to hide popup...");
        yield return new WaitForSeconds(5f);  // Extend the duration for testing

        popupUI.SetActive(false);
        Debug.Log("Popup hidden.");
    }
}