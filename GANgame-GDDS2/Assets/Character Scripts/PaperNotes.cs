using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaperNotes : MonoBehaviour
{
    public Text noteText;
    public string note; //ur input 
    public GameObject paperNote;
    bool viewNote = false;

    public InventoryObject playerInventory; //checks with player inventory 
    public ItemObject itemRequired; //item that need to be used
    public bool itemNeeded; //check if item is required to interact 
    bool canOpen = true; //checks if player can open note(can by default)
    EventManager em;
    // Start is called before the first frame update
    void Start()
    {
        //noteText.text = note;
        em = FindObjectOfType<EventManager>();
    }

    // Update is called once per frame
    void Update()
    {
          
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (itemNeeded) //tick in inspector
            {
                canOpen = false; //disable player to open
                ItemObject o = playerInventory.equippedItem;
                if (o == itemRequired)
                {
                    canOpen = true;
                }
                EventManager.InteractEvent += OpenNote;
            }   

            else
            {
                canOpen = true;
                EventManager.InteractEvent += OpenNote;
            }
            viewNote = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            EventManager.InteractEvent -= OpenNote;
            viewNote = false;
        }
    }


    public void OpenNote()
    {
        if (viewNote && canOpen)
        {
            paperNote.SetActive(true);
            noteText.text = note;
            viewNote = false;
        }
    }
    public void Close()
    {
       canOpen = false;
       paperNote.SetActive(false);
    }

    private void OnDisable()
    {
        EventManager.InteractEvent -= OpenNote;
    }

}
