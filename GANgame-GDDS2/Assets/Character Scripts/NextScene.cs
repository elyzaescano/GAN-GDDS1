using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue;
using UnityEngine.SceneManagement;

public class NextScene : LockDoor
{
    public string sceneToMoveTo;

    GameObject dialog;
    public Conversation _converse;

    public ItemObject itemRequired;

    private void Start()
    {
        dialog = GameObject.FindGameObjectWithTag("Dialog").transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        other.TryGetComponent<InventoryObject>(out InventoryObject inventory);
        if (inventory.equippedItem == itemRequired)
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
        EventManager.InteractEvent -= GoToNextScene;

        SceneManager.LoadScene(sceneToMoveTo);
    }

    public void PlayDialog()
    {
        dialog.SetActive(true);

        DialogDisplay dd = dialog.GetComponent<DialogDisplay>();
        
        dd.conversation = _converse;
        dd.simulateClick = true;
    }

    private void OnDisable()
    {
        EventManager.InteractEvent -= GoToNextScene;
        EventManager.InteractEvent -= PlayDialog;
    }
}
