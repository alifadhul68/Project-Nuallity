using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FishItem : MonoBehaviour, IInteractable
{
    public float IncreaseDmgBy = 0.3f;
    public float IncreaseRangeBy = 2f;
    private HealthBar healthBar;
    private string title = "Big Fish";
    private string description = "Your Gun Is feeling stronger... Damage and range up";
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
            // Calculate the amount to heal, considering max health
            // Apply healing to the player
            PlayerGun playerGun = PlayerGun.Instance;
            playerGun.damage += IncreaseDmgBy;
            playerGun.travelTime += IncreaseRangeBy;
            PopupManager.Instance.ShowPopup(title, description, 2.5f);
            Destroy(transform.parent.gameObject);
        }
    }

    public bool CanInteract()
    {
        // Add conditions if the item can be bought
        return true;
    }
}
