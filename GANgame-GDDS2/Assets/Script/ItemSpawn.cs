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
        if(other.tag == "Player")
        {
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
