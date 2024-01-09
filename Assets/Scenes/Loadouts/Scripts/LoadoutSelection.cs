using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadoutSelection : MonoBehaviour
{
    public GameObject[] loadouts;
    public int selected = 0;

    [SerializeField]
    private TextMeshProUGUI unlockText;
    [SerializeField]
    private Button selectButton;

    public void SelectNext()
    {
        Quaternion rotation;
        try {
            rotation = loadouts[selected].GetComponentInChildren<Rotate>().rotation;
            loadouts[(selected + 1) % loadouts.Length].GetComponentInChildren<Rotate>().rotation = rotation;
        } catch {}

        loadouts[selected].SetActive(false);
        selected = (selected + 1) % loadouts.Length;
        loadouts[selected].SetActive(true);

        ChangeUnlockText();
    }

    public void SelectPrevious()
    {
        // makes
        Quaternion rotation;
        try
        {
            rotation = loadouts[selected].GetComponentInChildren<Rotate>().rotation;
            loadouts[(selected-1) < 0 ? selected - 1 + loadouts.Length : selected - 1].GetComponentInChildren<Rotate>().rotation = rotation;
        }
        catch { }

        loadouts[selected].SetActive(false);
        selected--;
        if (selected < 0)
        {
            selected += loadouts.Length;
        }
        loadouts[selected].SetActive(true);

        ChangeUnlockText();
    }

    private void ChangeUnlockText()
    {
        switch (selected)
        {
            case 0: // pistol
                selectButton.enabled = true;
                unlockText.text = string.Empty;
                break;
            case 1: // submachine
                Scoring.Instance.LoadData();
                int killed = Scoring.Instance.enemiesKilled;
                selectButton.enabled = killed >= 10;
                unlockText.text = "Kill 10 enemies to unlock. Enemies killed: " + killed.ToString();
                break;
            case 2: // sawedoff
                Scoring.Instance.LoadData();
                int broken = Scoring.Instance.barriersDestroyed;
                selectButton.enabled = broken >= 10;
                unlockText.text = "Break 10 barriers to unlock. Barriers broken: " + broken.ToString();
                break;
        }
    }

    public void Select()
    {
        string loadoutName = loadouts[selected].name;
        Debug.Log(loadoutName);
        PlayerPrefs.SetString("Loadout", loadoutName);
        SceneManager.LoadScene("Manin Menu", LoadSceneMode.Single);
    }
}
