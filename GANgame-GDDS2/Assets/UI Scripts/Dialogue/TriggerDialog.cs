using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue;

public class TriggerDialog : MonoBehaviour
{
    public GameObject dialogBox;
    public Conversation convoToPlay;

    [Header("Settings")]
    public bool playOnce; //should the conversation play only once?
    public bool mustInteract; //must the player interact for the conversation to play? btw if it's MUST INTERACT then DO NOT CLICK PLAY ONCE

    private void Start() 
    {
        dialogBox = EventManager.dialogBox;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            if (convoToPlay != null) //prevents no conversation errors
            {
                if (mustInteract)
                {
                    EventManager.InteractEvent += PlayDialog;
                }
                else
                {
                    dialogBox.SetActive(true);
                    dialogBox.GetComponent<DialogDisplay>().conversation = convoToPlay;
                    PlayDialog();
                }
            }
        }   
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        EventManager.InteractEvent -= PlayDialog;
        if (other.tag == "Player")
        {
            if (playOnce)
            {
                Destroy(gameObject);
            }
        }
    }

    public void PlayDialog()
    {
        dialogBox.SetActive(true);
        DialogDisplay dd = dialogBox.GetComponent<DialogDisplay>();
        
        dd.conversation = convoToPlay;
        dd.simulateClick = true;
    }

    public void PlayDialogFromEvent(Conversation convoToPlay)
    {
        dialogBox.SetActive(true);
        DialogDisplay dd = dialogBox.GetComponent<DialogDisplay>();

        dd.conversation = convoToPlay;
        dd.simulateClick = true;
    }

}
