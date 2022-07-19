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
    public RoomTeleporter RoomTPBlock;

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
        itemSpawn.gameObject.GetComponent<AudioSource>().PlayOneShot(feedback[3]);
    }

    public void EnterCode()
    {
        if (codeTextValue == passCode)
        {
            if(itemSpawn != null) itemSpawn.enabled = true; itemSpawn.Spawn();

            if (RoomTPBlock != null) RoomTPBlock.itemRequired = null;
            itemSpawn.gameObject.GetComponent<AudioSource>().PlayOneShot(feedback[1]);
            codeTextValue = "";
            
        }
        else
        {
            itemSpawn.gameObject.GetComponent<AudioSource>().PlayOneShot(feedback[0]);
            codeTextValue = "";
            
        }
    }
}
