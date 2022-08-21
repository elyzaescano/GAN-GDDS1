using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class KeypadScreen : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI codeText;
    [SerializeField]string codeTextValue = "";
    public string passCode;
    int digitLimit;
    bool canAddDigit;

    [SerializeField]AudioSource audioSource;
    public AudioClip[] feedback;

    public ItemSpawn itemSpawn; //refer from itemSpawn
    public RoomTeleporter RoomTPBlock;

    public UnityEvent FailedPasscode;
    public UnityEvent SuccesfulPasscode;

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

            //Disables interaction with keypad
            if (itemSpawn != null) {
                itemSpawn.canSpawn = true;
                itemSpawn.itemNeeded = true;
                itemSpawn.gameObject.GetComponent<AudioSource>().PlayOneShot(feedback[1]);
                if (!itemSpawn.gameObject.TryGetComponent<KeyPadPuzzle>(out KeyPadPuzzle keyPad)) { return; } 
                else {  keyPad.completed = true; }
                itemSpawn.Spawn();
                SuccesfulPasscode?.Invoke();
            }

            if (RoomTPBlock != null) 
            {
                EventManager.UnlockDoor4();
                print("Working");
                RoomTPBlock.itemRequired = null; 
                this.gameObject.SetActive(false);
                SuccesfulPasscode?.Invoke();
            }

            codeTextValue = "";

        }
        else
        {
            if(itemSpawn!= null)itemSpawn.gameObject.GetComponent<AudioSource>().PlayOneShot(feedback[0]);
            codeTextValue = "";
            FailedPasscode?.Invoke();
        }
    }
}
