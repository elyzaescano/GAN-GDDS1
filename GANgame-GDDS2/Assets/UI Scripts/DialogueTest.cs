using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTest : MonoBehaviour
{

    public GameObject dialogueBox;
    public Text dialogueText;
    public string dialogue;
    public bool playerInRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.I) && playerInRange) //Letter will open if 'I' is pressed and playerInRange is true
    //    {
    //        //Checks if dialogueBox is active, if active, then disable.
    //        if (dialogueBox.activeInHierarchy)
    //        {
    //            dialogueBox.SetActive(false);
    //        } else
    //        {
    //            dialogueBox.SetActive(true);
    //            dialogueText.text = dialogue; //Changes text in dialogue box
    //        }
    //    }
    //}


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            dialogueBox.SetActive(false); //dialogueBox not active when out of range
        }
    }

    public void SetDiary()
    {
        if (playerInRange)
        {
            if (dialogueBox.activeInHierarchy)
            {
                dialogueBox.SetActive(false);
            }
            else
            {
                dialogueBox.SetActive(true);
                dialogueText.text = dialogue; //Changes text in dialogue box
            }
        }
    }
}
