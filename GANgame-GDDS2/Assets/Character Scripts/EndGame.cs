using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue;
using UnityEngine.SceneManagement;

public class EndGame : LockDoor
{
    public string sceneToMoveTo;

    GameObject dialog;
    public Conversation _converse;

    private void Start()
    {
        dialog = GameObject.FindGameObjectWithTag("Dialog");
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (!isLocked)
        {
            EventManager.InteractEvent += GoToNextScene;
        }
        else
        {
            print("locked");
            EventManager.InteractEvent += PlayDialog;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        EventManager.InteractEvent -= GoToNextScene;
        EventManager.InteractEvent -= PlayDialog;
    }

    void GoToNextScene()
    {
        SceneManager.LoadScene(sceneToMoveTo);
    }

    public void PlayDialog()
    {
        dialog.transform.GetChild(0).gameObject.SetActive(true);

        dialog.GetComponentInChildren<DialogDisplay>().conversation = _converse;
        dialog.GetComponentInChildren<DialogDisplay>().AdvanceConversation();
        EventManager.InteractEvent -= PlayDialog;

    }

    private void OnDisable()
    {
        EventManager.InteractEvent -= GoToNextScene;
        EventManager.InteractEvent -= PlayDialog;
    }
}
