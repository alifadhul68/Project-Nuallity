using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPill : MonoBehaviour
{
    [SerializeField] private int healAmount = 20; // Adjust as needed
    private string powerupTitle = "Heal Up";
    private string powerupDescription = "Heal 20 HP";
    private HealthBar healthBar;
    void Start()
    {
        GameObject healthBarObject = GameObject.Find("HealthBar");
        healthBar = healthBarObject.GetComponent<HealthBar>();
        if (healthBar == null)
        {
            Debug.LogError("HealthBar script not found in the scene.");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Check if healing will exceed max health
            if (PlayerHealth.currentHealth < PlayerHealth.maxHealth)
            {
                // Calculate the amount to heal, considering max health
                int amountToHeal = Mathf.Min(healAmount, PlayerHealth.maxHealth - PlayerHealth.currentHealth);

                // Apply healing to the player
                PlayerHealth.currentHealth += amountToHeal;
                healthBar.SetValue(PlayerHealth.currentHealth);
                // Log the healing event (optional)
                Debug.Log("Player healed for " + amountToHeal + " health.");

            }
            else
            {
                // Player is already at max health, handle accordingly
                Debug.Log("Player is already at max health.");
            }
            PopupManager.Instance.ShowPopup(powerupTitle, powerupDescription, 2.5f);
            Destroy(gameObject);
        }
    }
}