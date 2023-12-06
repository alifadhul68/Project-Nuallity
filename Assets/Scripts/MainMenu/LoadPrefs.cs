using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class LoadPrefs : MonoBehaviour
{

    [Header("General Settings")]
    [SerializeField] private MenuController menuController = null;

    [Header("Volume Settings")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;

    [Header("Graphics Settings")]
    [SerializeField] private TMP_Text brightnesseTextValue = null;
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private Toggle fullScreenTOggle;
    [SerializeField] private PostProcessProfile brightnessProfile;
    [SerializeField] private PostProcessLayer layer;

    private AutoExposure exposure;

    private void Awake()
    {
        brightnessProfile.TryGetSettings(out exposure);

        if (PlayerPrefs.HasKey("masterVolume"))
        {
            float localVolume = PlayerPrefs.GetFloat("masterVolume");
            volumeTextValue.text = localVolume.ToString("0.0");
            volumeSlider.value = localVolume;
            AudioListener.volume = localVolume;
        }
        else
        {
            menuController.ResetToDefault("Audio");
        }

        if (PlayerPrefs.HasKey("masterBrightness"))
        {
            float localBrightness = PlayerPrefs.GetFloat("masterBrightness");
            brightnesseTextValue.text = localBrightness.ToString("0.0");
            brightnessSlider.value = localBrightness;
            exposure.keyValue.value = localBrightness;
        }
        else
        {
            menuController.ResetToDefault("Graphics");
        }

        if (PlayerPrefs.HasKey("masterFullScreen"))
        {
            int localFullScreen = PlayerPrefs.GetInt("masterFullScreen");
            if (localFullScreen == 1)
            {
                Screen.fullScreen = true;
                fullScreenTOggle.isOn = true;
            }
            else
            {
                Screen.fullScreen = false;
                fullScreenTOggle.isOn = true;
            }
        }
        else
        {
            menuController.ResetToDefault("Graphics");
        }
    }
}
