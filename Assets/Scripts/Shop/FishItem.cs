using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FishItem : MonoBehaviour, IInteractable
{
    public float increaseDmgBy = 0.3f;
    public float increaseRangeBy = 2f;
    private string title = "Big Fish";
    private string description = "Your Gun Is feeling stronger... Damage and range up";
    [SerializeField] public int price = 7;
    [SerializeField] private TMP_Text priceText;

    void Start()
    {
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
            playerGun.damage += increaseDmgBy;
            playerGun.travelTime += increaseRangeBy;
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
