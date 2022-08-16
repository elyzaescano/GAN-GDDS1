using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderPrefsBGM : MonoBehaviour
{
    public Slider slider;

    private void Awake()
    {
        slider.value = PlayerPrefs.GetFloat("Slider value");
    }

    public void OnSliderChange(float newValue)
    {
        PlayerPrefs.SetFloat("Slider value", newValue);
    }
}
