using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BananaItem : MonoBehaviour, IInteractable
{
    // Modifiers for the player's attributes
    public float multiplyDmgBy = 2f;
    public float increaseRangeBy = 2f;
    public int newPlayerHP = 10;
    public float multiplyFireRate = 2f;
    public float decreaseReloadTimeBy = 2f;
    public float decreaseSpreadBy = 0.2f;
    public int multiplyBulletsBy = 2;

    // Reference to the HealthBar script
    private HealthBar healthBar;
    // Item details
    private string title = "Banana";
    private string description = "You feel Like a Glass-Cannon";
    // Price and UI text for the price
    [SerializeField] public int price = 10;
    [SerializeField] private TMP_Text priceText;

    void Start()
    {
        // Find the HealthBar in the scene
        GameObject healthBarObject = GameObject.Find("HealthBar");
        healthBar = healthBarObject.GetComponent<HealthBar>();
        if (healthBar == null)
        {
            // Log an error if HealthBar script is not found
            Debug.LogError("HealthBar script not found in the scene.");
        }
        // Set the price text
        priceText.text = price + "C";
    }
    public void Interact()
    {
        if (CanInteract())
        {
            // Check if the player has enough coins to purchase
            if (Coin.coins >= price)
            {
                PlayerHealth.currentHealth = newPlayerHP;
                PlayerHealth.maxHealth = newPlayerHP;
                healthBar.SetValue(PlayerHealth.currentHealth);
                PlayerGun playerGun = PlayerGun.Instance;
                playerGun.damage *= multiplyDmgBy;
                playerGun.travelTime += increaseRangeBy;
                playerGun.fireRate *= multiplyFireRate;
                playerGun.reloadTime -= decreaseReloadTimeBy;
                playerGun.spread -= decreaseSpreadBy;
                playerGun.bullets *= multiplyBulletsBy;
                PopupManager.Instance.ShowPopup(title, description, 2.5f);
                transform.parent.gameObject.SetActive(false);
                Coin.coins -= price;
                Coin.UpdateCoinCountText();
            }
            else {
                PopupManager.Instance.ShowPopup("Not Enough Coins", "Collect More Coins", 2.5f);
            }
        }
        
    }

    public bool CanInteract()
    {
        return true;
    }
}
