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
    bool win = false;

    [SerializeField] GameObject[] buttons;
    [SerializeField] GameObject[] lightArray;
    [SerializeField] int[] lightOrder;
    [SerializeField] GameObject gamePanel;

    public float lightSpeed;

    Color32 invisible = new Color32(4, 204, 0, 0);
    Color32 white = new Color32(255, 255, 255, 255);

    // Update is called once per frame
    void Update()
    {
        if (win)
        {
            Debug.Log("Yippee");
        }

        if (fail)
        {
            level = 0;
            buttonsPressed = 0;
            colourOrderRunCount = -1;
            level = 1;

            GenerateCode();
            StartCoroutine(ColourOrder());

            fail = false;
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            gamePanel.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            gamePanel.SetActive(false);
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
    }

    void GenerateCode()
    {
        for (int i = 0; i < lightOrder.Length; i++)
        {
            lightOrder[i] = (Random.Range(0, 4));
        }
    }

    public void ButtonClickOrder(int button)
    {
        buttonsPressed++;
        if (button == lightOrder[buttonsPressed - 1])
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
        if (buttonsPressed == level && pass == true && buttonsPressed != 5)
        {
            level++;
            pass = false;
            StartCoroutine(ColourOrder());
        }
        if (buttonsPressed == level && pass == true && buttonsPressed == 5)
        {
            level++;  
            win = true;
        }
    }

    IEnumerator ColourOrder()
    {
        buttonsPressed = 0;
        colourOrderRunCount++;
        DisableButtons();
        for (int i = 0; i <= colourOrderRunCount; i++)
        {
            if (level >= colourOrderRunCount)
            {
                yield return new WaitForSeconds(lightSpeed);
                lightArray[lightOrder[i]].GetComponent<Image>().color = white;
                yield return new WaitForSeconds(lightSpeed);
                lightArray[lightOrder[i]].GetComponent<Image>().color = invisible;
                yield return new WaitForSeconds(lightSpeed);
                lightArray[lightOrder[i]].GetComponent<Image>().color = white;
            }
        }
        EnableButtons();
    }

    void DisableButtons()
    {
         for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Button>().interactable = false;
        }
    }

    void EnableButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Button>().interactable = true;
        }
    }
}
