using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BurgerItem : MonoBehaviour, IInteractable
{
    public int IncreaseHPBy = 30;
    public float DecreaseSpeedBy = -3f;
    private HealthBar healthBar;
    private string title = "A Healthy Burger";
    private string description = "Max HP increased but you do not feel good... Speed Down.";
    [SerializeField] public int price = 7;
    [SerializeField] private TMP_Text priceText;

    void Start()
    {
        GameObject healthBarObject = GameObject.Find("HealthBar");
        healthBar = healthBarObject.GetComponent<HealthBar>();
        if (healthBar == null)
        {
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
                PlayerHealth.maxHealth += IncreaseHPBy;
                PlayerHealth.currentHealth = PlayerHealth.maxHealth;
                healthBar.SetValue(PlayerHealth.currentHealth);
                GameObject playerObject = GameObject.FindWithTag("Player");
                if (playerObject != null)
                {
                    PlayerMovement playerMovement = playerObject.GetComponent<PlayerMovement>();

                    if (playerMovement != null)
                    {
                        // Example: Call a method in PlayerMovement
                        playerMovement.EditSpeed(DecreaseSpeedBy);

                    }
                    else
                    {
                        Debug.LogError("PlayerMovement script not found on the player object.");
                    }
                }
                else
                {
                    Debug.LogError("Player object not found with the 'Player' tag.");
                }
                PopupManager.Instance.ShowPopup(title, description, 2.5f);
                transform.parent.gameObject.SetActive(false);
                Coin.coins -= price;
                Coin.UpdateCoinCountText();
            }
            else
            {
                PopupManager.Instance.ShowPopup("Not Enough Coins", "Collect More Coins", 2.5f);
            }
        }
    }

    public bool CanInteract()
    {
        // Add conditions if the item can be bought
        return true;
    }
}
