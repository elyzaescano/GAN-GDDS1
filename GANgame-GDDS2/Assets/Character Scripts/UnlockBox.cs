using System.Collections;
using System.Collections.Generic;
using Dialogue;
using UnityEngine;

public class UnlockBox : MonoBehaviour
{
    public ItemSpawn box; //refer to box that needs to be unlocked

    bool canSpawn = false;

    public InventoryObject playerInventory;
    public ItemObject itemRequired;
    public EventManager em;

    public GameObject dialoguebox;
    public Conversation unlockMessage;
    // Start is called before the first frame update
    void Start()
    {
        dialoguebox = GameObject.FindGameObjectWithTag("Dialog").transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            EventManager.InteractEvent += this.Unlock;
            print("subscribed");
            ItemObject o = playerInventory.equippedItem;
            if (o == itemRequired)
            {
                canSpawn = true;
            }
        }
    }

    public void Unlock()
    {
        if (canSpawn && this.enabled)
        {
            if (unlockMessage != null) {dialoguebox.SetActive(true); 
            dialoguebox.GetComponent<DialogDisplay>().conversation = unlockMessage;}
            box.itemNeeded = false;
            Destroy(GetComponent<UnlockBox>());
            Destroy(GetComponent<ItemSpawn>());
        }
    }
}

