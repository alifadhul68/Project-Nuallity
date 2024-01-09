using System.Collections;
using TMPro;
using UnityEngine;

public class TomatoItem : MonoBehaviour, IInteractable
{
    private string title = "Tomato";
    private string description = "A Tomato with unpredictable effects";
    [SerializeField] public int price = 1;
    [SerializeField] private TMP_Text priceText;

    [Header("Effect Probabilities")]
    private float buffProbability = 0.5f;
    private float debuffProbability = 0.5f;

    void Start()
    {
        
        // Set the price text
        priceText.text = price + "C";
    }

    public void Interact()
    {
        if (CanInteract())
        {
            ApplyRandomEffect();
            PopupManager.Instance.ShowPopup(title, description, 2.5f);
            Destroy(transform.parent.gameObject);
        }
    }

    public bool CanInteract()
    {
        // Add conditions if the item can be bought
        return true;
    }

    private void ApplyRandomEffect()
    {

        PlayerGun playerGun = PlayerGun.Instance;

        float randomValue = Random.value;

        switch (GetEffectType(randomValue))
        {
            case EffectType.Buff:
                title = "Tomato Buff";
                ApplyRandomBuff(playerGun);
                break;

            case EffectType.Debuff:
                title = "Rotten Tomato";
                ApplyRandomDebuff(playerGun);
                break;
        }
    }

    private EffectType GetEffectType(float randomValue)
    {
        if (randomValue < buffProbability)
        {
            return EffectType.Buff;
        }
        else if (randomValue < buffProbability + debuffProbability)
        {
            return EffectType.Debuff;
        }

        return EffectType.None;
    }

    private void ApplyRandomBuff(PlayerGun playerGun)
    {
        BuffType buffType = GetRandomBuffType();

        switch (buffType)
        {
            case BuffType.Damage:
                playerGun.damage += Random.Range(0.3f, 0.5f);
                description = "Damage Up!";
                break;

            case BuffType.Range:
                playerGun.travelTime += Random.Range(0.5f, 3f);
                description = "Range Up!";
                break;


            case BuffType.ReloadTime:
                playerGun.reloadTime -= Random.Range(0.5f, 1.5f);
                description = "Reload Time Down!";
                break;

            case BuffType.Spread:
                playerGun.spread -= Random.Range(0.1f, 0.3f);
                description = "Spread Down!";
                break;

            case BuffType.Bullets:
                playerGun.bullets += Random.Range(2, 3);
                description = "Bullet Count Up!";
                break;
        }
    }

    private void ApplyRandomDebuff(PlayerGun playerGun)
    {
        DebuffType debuffType = GetRandomDebuffType();

        switch (debuffType)
        {
            case DebuffType.Damage:
                playerGun.damage -= Random.Range(0.3f, 0.5f);
                description = "Damage Down!";
                break;

            case DebuffType.Range:
                playerGun.travelTime -= Random.Range(0.3f, 0.5f);
                description = "Range Down!";
                break;

            case DebuffType.ReloadTime:
                playerGun.reloadTime += Random.Range(0.3f, 1f);
                description = "Reload Time Up!";
                break;

            case DebuffType.Spread:
                playerGun.spread += Random.Range(0.1f, 0.3f);
                description = "Spread Up!";
                break;
            default:
                break;
        }
    }

    private BuffType GetRandomBuffType()
    {
        return (BuffType)Random.Range(0, System.Enum.GetValues(typeof(BuffType)).Length);
    }

    private DebuffType GetRandomDebuffType()
    {
        return (DebuffType)Random.Range(0, System.Enum.GetValues(typeof(DebuffType)).Length);
    }

    private enum EffectType
    {
        None,
        Buff,
        Debuff
    }

    private enum BuffType
    {
        Damage,
        Range,
        ReloadTime,
        Spread,
        Bullets
    }

    private enum DebuffType
    {
        Damage,
        Range,
        ReloadTime,
        Spread,
    }
}
