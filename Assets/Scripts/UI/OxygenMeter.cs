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
        maxTopMsk = barRect.rect.height - mask.padding.y - mask.padding.w;
        initialTopMsk = mask.padding.w;
    }

    public void SetValue(int newValue)
    {
        var targetHeight = newValue * maxTopMsk / 50;
        var newTopMask = maxTopMsk - targetHeight + initialTopMsk;
        var padding = mask.padding;
        padding.w = newTopMask;
        mask.padding = padding;
    }
}
