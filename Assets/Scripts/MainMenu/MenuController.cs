using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    [Header("Levels To Load")]
    public string levelToLoad;

    [Header("Volume Settings")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider= null;
    [SerializeField] private float defaultVolume = 1.0f;

    [Header("Graphics Settings")]
    [SerializeField] private TMP_Text brightnesseTextValue = null;
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private float defaultBrightness = 0.8f;
    [SerializeField] private Toggle fullScreenTOggle;

    private bool _isFullScreen;
    private float _brightnessLevel;

    public void ExitButton()
    {
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
    }

    public void ResetToDefault(string MenueType)
    {
        switch (MenueType)
        {
            case "Audio":
                AudioListener.volume = defaultVolume;
                volumeSlider.value = defaultVolume;
                volumeTextValue.text = defaultVolume.ToString("0.0");
                VolumeApply();
                break;
            case "Graphics":
                Screen.brightness = defaultBrightness;
                brightnessSlider.value = defaultBrightness;
                brightnesseTextValue.text = defaultBrightness.ToString("0.0");
                _isFullScreen = true;
                Screen.fullScreen = true;
                fullScreenTOggle.isOn = true;
                GraphicsApply();
                break;
        }
    }

    public void SetBrightness(float brightness)
    {
        _brightnessLevel = Mathf.Clamp(brightness, 0.0f, 1.0f);
        brightnesseTextValue.text = brightness.ToString("0.0");
    }

    public void SetFullScreen(bool isFullScreen)
    {
        _isFullScreen = isFullScreen;
    }

    public void GraphicsApply()
    {
        PlayerPrefs.SetFloat("masterBrightness", _brightnessLevel);

        PlayerPrefs.SetInt("masterFullScreen", (_isFullScreen ? 1 :0));

        Screen.fullScreen = _isFullScreen;
        Screen.brightness = _brightnessLevel;

        Debug.Log("Brightness after setting: " + Screen.brightness);
    }
}
