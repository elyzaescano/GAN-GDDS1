using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    Vector2 movement;

    public InventoryObject inventory;
    public ItemDatabaseObject database;


    
    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        if (Input.GetKeyDown(KeyCode.E)) inventory.Save();
    
        if (Input.GetKeyDown(KeyCode.Space)) inventory.Load();

        if (Input.GetKeyDown(KeyCode.Alpha1)) inventory.DropItem(0);
        
        if (Input.GetKeyDown(KeyCode.Alpha2)) inventory.DropItem(1);

        
    }

    public void GetID(int _id)
    {
        foreach(InventorySlot _is in inventory.Container)
        {
            print(_is.item.id);
            if(_is.item.id == _id)
            {
                //Instantiate(database.)
                //Instantiate(_is., transform.position, Quaternion.identity);
            }
        }

    }

    private void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); //left and right  
        movement.y = Input.GetAxisRaw("Vertical"); //up and down 
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var item = collision.GetComponent<Item>();
        if (item)
        {
            inventory.AddItem(item.item, 1);
            Destroy(collision.gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }


    public void Move(float dirX, float dirY) //Joystick link
    {
        movement.x = dirX;
        movement.y = dirY;

        print(movement);
    }

}
