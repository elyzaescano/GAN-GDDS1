using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderPrefsBGM : MonoBehaviour
{
    public Slider slider;

    float sliderValueBGM = 1;

    private void Awake()
    {
        
        slider.value = PlayerPrefs.GetFloat("SliderValueBGM",sliderValueBGM);
    }

    public void OnSliderChange(float newValueBGM)
    {
        sliderValueBGM = newValueBGM;
        PlayerPrefs.SetFloat("SliderValueBGM", sliderValueBGM);
    }
}
