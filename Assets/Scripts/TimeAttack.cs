using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.PostProcessing;

public class TimeAttack : MonoBehaviour
{
    public float levelTime = 60f; // Time in seconds for the level
    private float timeRemaining;
    private bool timerIsRunning = false;

    public TMP_Text timeText; // Reference to a UI Text element to display the time

    void Start()
    {
        timeRemaining = levelTime;
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining <= 10f)
            {
                timeText.color = Color.red;
            }
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;

                // Handle the end of the level (e.g., show results, stop player movement)
                EndLevel();
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void EndLevel()
    {
        GameObject player = GameObject.Find("Player");
        PlayerHealth health = player.GetComponent<PlayerHealth>();
        PlayerHealth.currentHealth = 0;
        health.Die();
    }
}
