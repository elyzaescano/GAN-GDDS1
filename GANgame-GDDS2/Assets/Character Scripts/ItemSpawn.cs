using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{

    public GameObject[] itemPrefab;
    public Transform spawnPoint;
    bool canSpawn = false;
    public bool itemNeeded = false; //if we need any item to interact with 

    //checks with inventory 
    public InventoryObject playerInventory;
    public ItemObject itemRequired;

    public EventManager em;
    public int spawnerID;
    int triggerID;

    private void Awake()
    {
        playerInventory = GameObject.Find("Player").GetComponent<InventoryObject>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            EventManager.InteractEvent += this.Spawn;
            print("subscribed");
            ItemObject o = playerInventory.equippedItem;
            if (o == itemRequired || itemRequired == null)
            {
                itemNeeded = true;
            }
            canSpawn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        EventManager.InteractEvent -= this.Spawn;
    }

    public void Spawn()
    {
        if (canSpawn && itemNeeded && this.enabled)
        {
            for(int i = 0; i < itemPrefab.Length; i++)
            {
                Instantiate(itemPrefab[i], spawnPoint.transform.position, spawnPoint.transform.rotation);
            }
            //Instantiate(itemPrefab[i], spawnPoint.transform.position, spawnPoint.transform.rotation);
            canSpawn = false;
            EventManager.InteractEvent -= this.Spawn;
            Destroy(GetComponent<ItemSpawn>());
        }
    }
}
