using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private RectTransform barRect;
    [SerializeField]
    private RectMask2D mask;
    [SerializeField]
    private TMP_Text hpIndicator;

    private float maxRightMsk;
    private float initialRightMsk;

    // Start is called before the first frame update
    void Start()
    {
        // Calculate the maximum right mask position and the initial right mask position
        maxRightMsk = barRect.rect.width - mask.padding.x - mask.padding.z;

        // Set the initial text for the health indicator
        hpIndicator.SetText($"{PlayerHealth.currentHealth}/{PlayerHealth.maxHealth}");

        // Set the initial right mask position
        initialRightMsk = mask.padding.z;
    }


    // Set the health bar value and update the mask and text accordingly
    public void SetValue(int newValue)
    {
        // Calculate the target width for the mask based on the new value
        var targetWidth = newValue * maxRightMsk / PlayerHealth.maxHealth;

        // Calculate the new right position for the mask
        var newRightMask = maxRightMsk + initialRightMsk - targetWidth;

        // Update the padding of the mask
        var padding = mask.padding;
        padding.z = newRightMask;
        mask.padding = padding;

        // Update the text of the health indicator
        hpIndicator.SetText($"{newValue}/{PlayerHealth.maxHealth}");
    }
}