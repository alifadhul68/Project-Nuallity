using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BananaItem : MonoBehaviour, IInteractable
{
    public float multiplyDmgBy = 2f;
    public float increaseRangeBy = 2f;
    public int newPlayerHP = 10;
    public float multiplyFireRate = 2f;
    public float decreaseReloadTimeBy = 2f;
    public float decreaseSpreadBy = 0.2f;
    public int multiplyBulletsBy = 2;

    private HealthBar healthBar;
    private string title = "Banana";
    private string description = "You feel Like a Glass-Cannon";
    [SerializeField] public int price = 10;
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
            Destroy(transform.parent.gameObject);
        }
    }

    public bool CanInteract()
    {
        // Add conditions if the item can be bought
        return true;
    }
}
