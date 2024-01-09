using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class YogurtItem : MonoBehaviour, IInteractable
{
    public int increaseHPBy = 10;
    private HealthBar healthBar;
    private string title = "Yogurt";
    private string description = "Max HP increased... You do not feel full";
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
                PlayerHealth.maxHealth += increaseHPBy;
                healthBar.SetValue(PlayerHealth.currentHealth);
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
