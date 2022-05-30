using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeypadScreen : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI codeText;
    string codeTextValue = "";
    public string passCode;
    bool canAddDigit;

    AudioSource audioSource;
    public AudioClip[] feedback;

    public ItemSpawn itemSpawn; //refer from itemSpawn

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        codeText.text = codeTextValue;

        if (codeTextValue.Length == 4)
        {
            canAddDigit = false;
        }
        else
        {
            canAddDigit = true;
        }

        
    }

    public void AddDigit(string digit)
    {
        if (canAddDigit)
        {
            codeTextValue += digit;
            audioSource.clip = feedback[2];
            audioSource.Play();
        }
    }
     public void ClearNumber()
    {
        codeTextValue = "";
        audioSource.clip = feedback[3];
        audioSource.Play();
    }

    public void EnterCode()
    {
        if (codeTextValue == passCode)
        {
            itemSpawn.Spawn();
            audioSource.clip = feedback[1];
            audioSource.Play();
        }
        else
        {
            audioSource.clip = feedback[0];
            audioSource.Play();
            codeTextValue = "";
        }
    }
}
