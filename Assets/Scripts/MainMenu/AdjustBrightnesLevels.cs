using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class AdjustBrightnesLevels : MonoBehaviour
{
    [Header("Graphics Settings")]
    [SerializeField] private PostProcessProfile brightnessProfile;
    [SerializeField] private PostProcessLayer layer;

    private AutoExposure exposure;

    private void Awake()
    {
        brightnessProfile.TryGetSettings(out exposure);

        if (PlayerPrefs.HasKey("masterBrightness"))
        {
            float localBrightness = PlayerPrefs.GetFloat("masterBrightness");
            exposure.keyValue.value = localBrightness;
        }
        else
        {
            exposure.keyValue.value = 5f;
        }

    }
}

