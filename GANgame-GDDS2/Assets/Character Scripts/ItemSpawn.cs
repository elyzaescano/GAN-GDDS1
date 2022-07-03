using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{

    public GameObject itemPrefab;
    public Transform spawnPoint;
    bool canSpawn = false;
    public bool itemNeeded = false; //if we need any item to interact with 

    //checks with inventory 
    public InventoryObject playerInventory;
    public ItemObject itemRequired;

    public EventManager em;
    public int spawnerID;
    int triggerID;

    private void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            EventManager.SpawnItem += Spawn;
            ItemObject o = playerInventory.equippedItem;
            if(o == itemRequired)
            {
                itemNeeded = true;
            }
            canSpawn = true;
        }
    }

    public void Spawn()
    {
        if (canSpawn && itemNeeded)
        {
            Instantiate(itemPrefab, spawnPoint);
            canSpawn = false;
            Destroy(GetComponent<ItemSpawn>());
        }
    }
}
