using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class HealingItem : MonoBehaviour, IInteractable
{
    public int healAmount = 20;
    private HealthBar healthBar;
    private string title = "Heal Up";
    private string description = "Heal 20 HP";
    [SerializeField] public int price = 3;
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
            if (Coin.coins >= price)
            {
                // Calculate the amount to heal, considering max health
                int amountToHeal = Mathf.Min(healAmount, PlayerHealth.maxHealth - PlayerHealth.currentHealth);

                // Apply healing to the player
                PlayerHealth.currentHealth += amountToHeal;
                healthBar.SetValue(PlayerHealth.currentHealth);
                // Log the healing event (optional)
                Debug.Log("Player healed for " + amountToHeal + " health.");

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
