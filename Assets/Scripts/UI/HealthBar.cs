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
        maxRightMsk = barRect.rect.width - mask.padding.x - mask.padding.z;
        hpIndicator.SetText($"{PlayerHealth.currentHealth}/{PlayerHealth.maxHealth}");
        initialRightMsk = mask.padding.z;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetValue(int newValue)
    {
        var tagetWithWidth = newValue * maxRightMsk / PlayerHealth.maxHealth;
        var newRightMask = maxRightMsk + initialRightMsk - tagetWithWidth;
        var padding = mask.padding;
        padding.z = newRightMask;
        mask.padding = padding;
        hpIndicator.SetText($"{newValue}/{PlayerHealth.maxHealth}");
    }
}
