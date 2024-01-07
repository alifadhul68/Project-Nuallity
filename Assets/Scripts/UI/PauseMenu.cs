using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    [SerializeField]
    private Image reloadIndicator;

    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        reloadIndicator.gameObject.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        reloadIndicator.gameObject.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void BackToMenu()
    {
        Resume();
        SceneManager.LoadScene("Manin Menu");
    }
}
