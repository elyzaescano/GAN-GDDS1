using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockBox : MonoBehaviour
{
    public ItemSpawn box; //refer to box that needs to be unlocked

    bool canSpawn = false;

    public InventoryObject playerInventory;
    public ItemObject itemRequired;
    public EventManager em;
    // Start is called before the first frame update
    void Start()
    {
        
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
            box.itemNeeded = false;
            Destroy(GetComponent<UnlockBox>());
            Destroy(GetComponent<ItemSpawn>());
        }
    }
}

