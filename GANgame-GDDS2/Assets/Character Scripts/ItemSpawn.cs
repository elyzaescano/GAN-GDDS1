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
    [SerializeField]bool usekey;
    [SerializeField]ItemObject.Type Keytype;

    public EventManager em;
    public UnityEvent ItemSpawnedEvent;
    public UnityEvent InteractWithoutItemSpawn;
    public int spawnerID;
    int triggerID;

    private void Awake()
    {

        playerInventory = GameObject.Find("Player").GetComponent<InventoryObject>();
        if(itemRequired !=null) usekey = itemRequired.type == Keytype ? true : false;

    }
    private void OnEnable()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && this.enabled)
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
        if (canSpawn && itemNeeded)
        {
            for(int i = 0; i < itemPrefab.Length; i++)
            {
                Instantiate(itemPrefab[i], spawnPoint.transform.position+(Vector3.right*(i*(i+.5f))), spawnPoint.transform.rotation);
            }
            //Instantiate(itemPrefab[i], spawnPoint.transform.position, spawnPoint.transform.rotation);
            print("Spawned");
            canSpawn = false;
            ItemSpawnedEvent?.Invoke();
            if (usekey)
            {
                playerInventory.RemoveItem(playerInventory.equippedItem);
                playerInventory.equippedItem = null;
                EventManager.ItemEquip();

            }
            Destroy(GetComponent<ItemSpawn>());
        }
        else if(!itemNeeded)
        {
            InteractWithoutItemSpawn?.Invoke();
        }

        EventManager.InteractEvent -= this.Spawn;
    }
}
