using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    Vector2 movement;

    public InventoryObject inventory;

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);


        //if (Input.GetKey(KeyCode.E))
        //{
        //    if(inventory.Container.Count != 0)
        //    {
        //        inventory.DropItem(inventory.Container[0].item, 1);
        //    }
        //}
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
