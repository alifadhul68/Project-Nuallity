using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    [SerializeField] public GameObject endScreen;
    private TMP_Text titleText;
    private TMP_Text sentenceText;
    [SerializeField]
    private Image reloadIndicator;
    private void Start()
    {
        if (endScreen != null)
        {
            titleText = endScreen.transform.Find("Title").GetComponent<TextMeshProUGUI>();
            sentenceText = endScreen.transform.Find("sentence").GetComponent<TextMeshProUGUI>();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Set the text to congratulate the player
            titleText.text = "Congratulations!";
            sentenceText.text = "You have won the level!";

            // Activate the endScreen
            endScreen.SetActive(true);

            // Freeze the time
            Time.timeScale = 0f;
            PauseMenu.isPaused = true;
            reloadIndicator.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("error");
        }
    }
}
