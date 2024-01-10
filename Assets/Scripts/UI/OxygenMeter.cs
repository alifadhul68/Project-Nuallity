using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OxygenMeter : MonoBehaviour
{
    [SerializeField]
    private RectTransform barRect;
    [SerializeField]
    private RectMask2D mask;

    private float maxTopMsk;
    private float initialTopMsk;

    // Start is called before the first frame update
    void Start()
    {
        // Calculate the maximum top mask position and the initial top mask position
        maxTopMsk = barRect.rect.height - mask.padding.y - mask.padding.w;
        initialTopMsk = mask.padding.w;
    }

    // Set the oxygen meter value and update the mask accordingly
    public void SetValue(int newValue)
    {
        // Calculate the target height for the mask based on the new value
        var targetHeight = newValue * maxTopMsk / 50;

        // Calculate the new top position for the mask
        var newTopMask = maxTopMsk - targetHeight + initialTopMsk;

        // Update the padding of the mask
        var padding = mask.padding;
        padding.w = newTopMask;
        mask.padding = padding;
    }
}