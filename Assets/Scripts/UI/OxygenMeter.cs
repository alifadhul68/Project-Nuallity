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
        maxTopMsk = barRect.rect.height - mask.padding.y - mask.padding.z;
        initialTopMsk = mask.padding.z;
    }


    public void SetValue(int newValue)
    {
        var tagetWithWidth = newValue * maxTopMsk / 100;
        var newTopMask = maxTopMsk + initialTopMsk - tagetWithWidth;
        var padding = mask.padding;
        padding.z = newTopMask;
        mask.padding = padding;
    }
}
