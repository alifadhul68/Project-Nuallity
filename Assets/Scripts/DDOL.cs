using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DDOL : MonoBehaviour
{

    public AudioClip[] audioClips; // Array of audio clips
    private AudioSource audioSource; // Reference to the AudioSource component

    private int currentIndex = 0; // Index of the current audio clip

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component attached to the same GameObject

        if (audioClips.Length > 0)
        {
            PlayAudioClip(currentIndex); // Start playing the first audio clip
        }
        SceneManager.LoadScene("Scenes/Manin Menu");
    }

    private void PlayAudioClip(int index)
    {
        if (index >= 0 && index < audioClips.Length)
        {
            audioSource.clip = audioClips[index]; // Assign the current audio clip to the AudioSource
            audioSource.Play(); // Play the audio clip

            currentIndex++; // Move to the next audio clip index

            if (currentIndex >= audioClips.Length)
            {
                currentIndex = 0; // Reset index if it exceeds the array length
            }
        }
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayAudioClip(currentIndex); // Play the next audio clip when the current one finishes
        }
    }

}
