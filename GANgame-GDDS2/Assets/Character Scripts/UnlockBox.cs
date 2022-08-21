using System.Collections;
using System.Collections.Generic;
using Dialogue;
using UnityEngine;
using UnityEngine.Events;

public class UnlockBox : MonoBehaviour
{
    public ItemSpawn box; //refer to box that needs to be unlocked

    bool canSpawn = false;

    public InventoryObject playerInventory;
    public ItemObject itemRequired;
    public EventManager em;

    public GameObject dialoguebox;
    public Conversation unlockMessage;
    public UnityEvent UnlockEvent;
    public UnityEvent LockEvent;

    Collider2D[] colliders;
    // Start is called before the first frame update
    void Start()
    {
        playerInventory = GameObject.Find("Player").GetComponent<InventoryObject>();
        dialoguebox = GameObject.FindGameObjectWithTag("Dialog").transform.GetChild(0).gameObject;
        colliders = box.GetComponents<Collider2D>();
        foreach(Collider2D col in colliders)
        {
            col.enabled = false;
        }
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
            if (unlockMessage != null)
            {
                dialoguebox.SetActive(true);
                dialoguebox.GetComponent<DialogDisplay>().conversation = unlockMessage;
            }
            foreach (Collider2D col in colliders)
            {
                col.enabled = true;
            }
            box.itemNeeded = false;
        }
        else LockEvent?.Invoke();
        EventManager.InteractEvent -= this.Unlock;
    }
}

