using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Inventory inventory;
    public GameObject itemButton;

    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            for(int i = 0; i < inventory.slots.Length; i++) //check if slot is empty 
            { 
                if(inventory.isFull[i] == false)
                {
                    //Item can be added 
                    inventory.isFull[i] = true;
                    Instantiate(itemButton, inventory.slots[i].transform, false); //spawn at the inventory slot
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
