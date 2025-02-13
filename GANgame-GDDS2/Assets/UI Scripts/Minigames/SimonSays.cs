using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimonSays : MonoBehaviour
{
    int level = 0;
    int buttonsPressed;
    int colourOrderRunCount;
    bool pass = false;
    bool fail;
    public bool win = false;
    AudioSource audios;
    [SerializeField] AudioClip[] buttonSounds;
    [SerializeField] GameObject[] buttons;
    [SerializeField] GameObject[] lightArray;
    [SerializeField] GameObject[] lightArraySpriteSwap;
    [SerializeField] int[] lightOrder;
    [SerializeField] GameObject gamePanel;

    public float lightSpeed;

    Color32 invisible = new Color32(4, 204, 0, 0);
    Color32 white = new Color32(255, 255, 255, 255);

    public GameObject closeSimon;
    public GameObject gameDoor;

    // Update is called once per frame
    void Update()
    {
        if (win)
        {
            closeSimon.SetActive(true);
            EventManager.WinSimon();
            if (gameDoor != null) 
            {
                gameDoor.GetComponent<RoomTeleporter>().isLocked = false;//make the door false
                gameDoor.GetComponent<RoomTeleporter>().itemNeeded = false;
                gameDoor.GetComponent<RoomTeleporter>().itemRequired = null;
            } 
        }

        if (fail)
        {
            level = 0;
            buttonsPressed = 0;
            colourOrderRunCount = -1;
            level = 1;

            EventManager.FailSimon();
            GenerateCode();
            StartCoroutine(ColourOrder());

            fail = false;
        }
    }

    private void OnEnable()
    {
        level = 0;
        buttonsPressed = 0;
        colourOrderRunCount = -1;
        win = false;

        level = 1;
        GenerateCode();
        StartCoroutine(ColourOrder());

        audios = GetComponent<AudioSource>();
        EventManager.SimonWon += PlayWinAudio;
        EventManager.SimonFailed += PlayFailAudio;
    }

    void GenerateCode() //Generates a random sequence
    {
        for (int i = 0; i < lightOrder.Length; i++)
        {
            lightOrder[i] = (Random.Range(0, 4));
        }
    }

    public void ButtonClickOrder(int button)
    {
        buttonsPressed++;
        if (button == lightOrder[buttonsPressed - 1]) //Checks if player has tapped on correct button
        {
            Debug.Log("pass");
            pass = true;
        }
        else
        {
            Debug.Log("failed");
            fail = true;
            pass = false;
            win = false;
        }

        if (buttonsPressed == level && pass == true) //If player successfully completes this level, and...
        {
            if (buttonsPressed != 4) //...if player has not reached the final level, proceed to the next level
            {
                level++; //Proceed level
                pass = false; //Reset level progress
                StartCoroutine(ColourOrder()); //Show order of colours
            }
            if (buttonsPressed == 4) //Player has reached final level
            {
                level++; 
                win = true; //Guess //No
            }
        }
    }

    IEnumerator ColourOrder() //Shows the order of lights for the player to press
    {
        buttonsPressed = 0; //Resets buttonPressed for the level
        colourOrderRunCount++; //Adds one more colour to show

        DisableButtons();
        for (int i = 0; i <= colourOrderRunCount; i++)
        {
            if (level >= colourOrderRunCount)
            {
                yield return new WaitForSeconds(lightSpeed);
                lightArray[lightOrder[i]].GetComponent<Image>().color = white;
                yield return new WaitForSeconds(lightSpeed);

                AudioClip clip;
                clip = buttonSounds[1];
                audios.PlayOneShot(clip);

                lightArray[lightOrder[i]].GetComponent<Image>().color = invisible;
                yield return new WaitForSeconds(lightSpeed);
                lightArray[lightOrder[i]].GetComponent<Image>().color = white;
                
            }
        }
        EnableButtons();
    }

    public void PlayButtonSound()
    {
        AudioClip clip;
        clip = buttonSounds[0];
        audios.PlayOneShot(clip);
    }

    //Disables the buttons lol
    void DisableButtons()
    {
         for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Button>().interactable = false;
            buttons[i].GetComponent<Image>().color = new Color32(255, 255, 255, 90);
        }
    }

    //Enables the buttons lol
    void EnableButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Button>().interactable = true;
            buttons[i].GetComponent<Image>().color = white;
        }
    }

    void PlayWinAudio()
    {
        AudioClip clip;
        clip = buttonSounds[2];
        audios.PlayOneShot(clip);

        EventManager.SimonWon -= PlayWinAudio;
    }

    void PlayFailAudio()
    {
        AudioClip clip;
        clip = buttonSounds[3];
        audios.PlayOneShot(clip);

        EventManager.SimonFailed -= PlayFailAudio;
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

}
