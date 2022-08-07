using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemSpawn : MonoBehaviour
{

    public GameObject[] itemPrefab;
    public Transform spawnPoint;
    public bool canSpawn = false;
    public bool itemNeeded = false; //if we need any item to interact with 

    //checks with inventory 
    public InventoryObject playerInventory;
    public ItemObject itemRequired;

    public EventManager em;
    public UnityEvent ItemSpawnedEvent;
    public UnityEvent InteractWithoutItemSpawn;
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
            print("Spawned");
            canSpawn = false;
            ItemSpawnedEvent?.Invoke();
            EventManager.InteractEvent -= this.Spawn;
            Destroy(GetComponent<ItemSpawn>());
        }
        else if(!itemNeeded)
        {
            InteractWithoutItemSpawn?.Invoke();
        }
    }
}
