using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadoutSelection : MonoBehaviour
{
    private string loadoutName;
    public GameObject[] loadouts;
    public int selected = 0;

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
    }

    public void Select()
    {
        loadoutName = "sawedoff";
        PlayerPrefs.SetString("Loadout", loadoutName);
        SceneManager.LoadScene("SampleScenee", LoadSceneMode.Single);
    }
}
