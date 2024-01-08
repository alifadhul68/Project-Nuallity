using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLevelInfo : MonoBehaviour
{
    [SerializeField] private string title;
    [SerializeField] private string description;

    void Start()
    {
        StartCoroutine(ShowPopupAfterDelay());
    }

    private IEnumerator ShowPopupAfterDelay()
    {
        // Wait for a short delay to ensure everything is initialized
        yield return new WaitForSeconds(0.1f);

        if (PopupManager.Instance != null)
        {
            PopupManager.Instance.ShowPopup(title, description, 3.5f);
        }
        else
        {
            Debug.LogError("PopupManager.Instance is not set. Make sure PopupManager is initialized.");
        }
    }
}
