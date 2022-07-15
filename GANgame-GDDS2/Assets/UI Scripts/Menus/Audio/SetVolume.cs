using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider = null;

    public AudioMixer mixer;
    float volume = 1.0f;

    private void Start()
    {
        LoadValues();
    }
    public void MenuBGM(float sliderValue)
    {

        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20); //Represents slider value as a logarithm with base of 10, then multiplies it by 20

    }

    public void SoundEffects(float sliderValue)
    {
        mixer.SetFloat("SoundEffects", Mathf.Log10(sliderValue) * 20);
    }

    public void SaveVolume()
    {
        float volumeValue = volumeSlider.value;
        PlayerPrefs.SetFloat("VolumeLevel", volume);
        LoadValues();

    }

    public void LoadValues()
    {
        float volumeValue = PlayerPrefs.GetFloat("VolumeValue");
        volumeSlider.value = volumeValue;
        AudioListener.volume = volumeValue;
    }
}
