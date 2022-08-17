using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderPrefsSFX : MonoBehaviour
{
    public Slider slider;

     float sliderValueSFX = 1;
    private void Awake()
    {
        slider.value = PlayerPrefs.GetFloat("SliderValueSFX",sliderValueSFX);
    }

    public void OnSliderChange(float newValueSFX)
    {
        sliderValueSFX = newValueSFX;
        PlayerPrefs.SetFloat("SliderValueSFX", sliderValueSFX);
    }
}
