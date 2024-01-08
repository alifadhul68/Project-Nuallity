using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private GameObject popupUI;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;

    private static PopupManager instance;

    private void Awake()
    {
        // Ensure the popup is initially hidden
        popupUI.SetActive(false);

        // Set the instance to this object
        instance = this;
    }

    public static PopupManager Instance
    {
        get
        {
            // Return the instance if it exists, otherwise find it in the scene
            return instance ?? FindObjectOfType<PopupManager>();
        }
    }

    public void ShowPopup(string titleText, string descriptionText, float duration)
    {
        // Activate the popup
        popupUI.SetActive(true);

        // Set title and description
        title.text = titleText;
        description.text = descriptionText;

        // Start a coroutine to hide the popup after a delay
        StartCoroutine(HidePopupAfterDelay(duration));
    }

    private IEnumerator HidePopupAfterDelay(float duration)
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Deactivate the popup after the duration
        popupUI.SetActive(false);
    }
}

