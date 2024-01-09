using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SausagesItem : MonoBehaviour, IInteractable
{
    public float increaseDmgBy = 0.5f;
    private HealthBar healthBar;
    private string title = "Sausages";
    private string description = "Damage Up";
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

            if (Coin.coins >= price)
            {
                PlayerGun playerGun = PlayerGun.Instance;
                playerGun.damage += increaseDmgBy;
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
