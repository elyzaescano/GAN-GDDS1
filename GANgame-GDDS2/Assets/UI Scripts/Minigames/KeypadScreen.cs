using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeypadScreen : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI codeText;
    [SerializeField]string codeTextValue = "";
    public string passCode;
    int digitLimit;
    bool canAddDigit;

    AudioSource audioSource;
    public AudioClip[] feedback;

    public ItemSpawn itemSpawn; //refer from itemSpawn
    public RoomTeleporter RoomTPBlock;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        digitLimit = passCode.Length;
    }
    private void Update()
    {
        codeText.text = codeTextValue;

        if (codeTextValue.Length == digitLimit)
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
            print("Working");
            if (itemSpawn != null) { itemSpawn.enabled = true; itemSpawn.Spawn(); 
                itemSpawn.gameObject.GetComponent<AudioSource>().PlayOneShot(feedback[1]); }

            if (RoomTPBlock != null) { this.gameObject.SetActive(false); EventManager.UnlockDoor4(); RoomTPBlock.itemRequired = null; }
            
            codeTextValue = "";
            //Disables interaction with keypad
            itemSpawn.gameObject.TryGetComponent<KeyPadPuzzle>(out KeyPadPuzzle keyPad);
            keyPad.completed = true;

        }
        else
        {
            itemSpawn.gameObject.GetComponent<AudioSource>().PlayOneShot(feedback[0]);
            codeTextValue = "";
            
        }
    }
}
