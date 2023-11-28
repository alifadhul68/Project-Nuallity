using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class LerpColor : MonoBehaviour
{
    // Fades the text out
    public Color targetColor = new Color(0, 0, 0, 0);
    Text textToFade;
    void Start()
    {
        textToFade = gameObject.GetComponent<Text>();
        StartCoroutine(LerpFunction(targetColor, 5));
    }
    IEnumerator LerpFunction(Color endValue, float duration)
    {
        float time = 0;
        Color startValue = textToFade.color;
        while (time < duration)
        {
            textToFade.color = Color.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        textToFade.color = endValue;
    }
}
