using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
//using static UnityEngine.Rendering.DebugUI;

public class ToturialDialogue : MonoBehaviour
{
    
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public GameObject dialoguePanel;
    public string[] lines;
    private int index;
    public float textSpeed;

    [SerializeField]
    private GameObject pills;
    [SerializeField]
    private GameObject tables;
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject portal;
    void Start()
    {
        dialogueText.text = string.Empty;
        nameText.text = "CRISTOS COLUMBUS";
        startDialogue();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.T))
        {//when the typing is finished go to next line
            if (dialogueText.text == lines[index])
            {
                NextLine();
                WithFlow();
            }
            else
            {//print all the letters immediatly
                StopAllCoroutines();
                dialogueText.text = lines[index];
            }
        }
    }
    //active objects with the flow of dialogue
    private void WithFlow()
    {
        switch (index)
        {
            case 5:
                tables.SetActive(true);
                break;

            case 6:
                enemy.SetActive(true);
                break;

            case 7:
                pills.SetActive(true);
                break;
            case 9:
                portal.SetActive(true);
                break;

            default:
                break;
        }
    }

    //starts the dialogue
    void startDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }
    //goes to next element
    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLine());

        }
        else
        {//if no more elemnts in lines[]
            dialoguePanel.SetActive(false);
        }
    }

    //type writer effect
    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {//tpye one character at a time
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }


}