using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToturialDialogue : MonoBehaviour
{
    
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public GameObject dialoguePanel;
    public string[] lines;
    private int index;
    public float textSpeed;
    void Start()
    {
        dialogueText.text = string.Empty;
        nameText.text = "CRISTOS COLUMBUS";
        startDialogue();
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.T))
        {
            if(dialogueText.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = lines[index];
            }
        }
    }

    void startDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    void NextLine()
    {
        if (index < lines.Length -1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLine());
            
        }
        else
        {
            dialoguePanel.SetActive(false);
        }
    }
    
    IEnumerator TypeLine()
    {
        foreach(char c in lines[index].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

}
/*void Start()
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
*/