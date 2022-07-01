using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
   
    public void SetLevel(float sliderValue)
    {

        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20); //Represents slider value as a logarithm with base of 10, then multiplies it by 20

    }
}
