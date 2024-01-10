using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    // Static variable to track whether the game is currently paused
    public static bool isPaused = false;

    // Image for the reload indicator 
    [SerializeField]
    private Image reloadIndicator;

    // Reference to the UI element that represents the pause menu
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        // Check if the player is not dead and the Escape key is pressed
        if (!PlayerIsDead() && Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle between pausing and resuming the game
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

    // Resumes the game
    public void Resume()
    {
        // Deactivate the pause menu UI
        pauseMenuUI.SetActive(false);

        // Activate the reload indicator (if applicable)
        reloadIndicator.gameObject.SetActive(true);

        // Set the time scale to normal to resume game time
        Time.timeScale = 1f;

        // Update the isPaused flag
        isPaused = false;
    }

    // Pauses the game
    void Pause()
    {
        // Activate the pause menu UI
        pauseMenuUI.SetActive(true);

        // Deactivate the reload indicator (if applicable)
        reloadIndicator.gameObject.SetActive(false);

        // Set the time scale to zero to pause the game
        Time.timeScale = 0f;

        // Update the isPaused flag
        isPaused = true;
    }

    // Restarts the game by loading the main menu scene
    public void BackToMenu()
    {
        // Resume the game before transitioning to the main menu
        Resume();

        // Load the main menu scene
        SceneManager.LoadScene("Manin Menu");
    }

    // Checks if the player is dead based on their current health
    private bool PlayerIsDead()
    {
        return PlayerHealth.currentHealth <= 0;
    }
}
