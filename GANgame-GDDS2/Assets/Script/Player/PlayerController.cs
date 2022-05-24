
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
    public InventoryUI inventoryUI;

    public Animator playerAnim;

    private void Start()
    {
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        if (Input.GetKeyDown(KeyCode.E)) inventory.Save();

        if (Input.GetKeyDown(KeyCode.Space)) inventory.Load(); 
        //First iteration of item interactions

        //----------

        //Drops item in first slot
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            inventory.DropItem(0, this.transform.position);
            inventoryUI.RemoveItem(inventory.GetItemObject(0));

        }

        //Takes item in first and second slot and combines them
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ItemObject reactor = inventory.GetItemObject(0); ItemObject reagent = inventory.GetItemObject(1);
            if (reactor && reagent != null){ StartCoroutine(Crafting(reactor, reagent)); }
        }

        //Reloads inventory UI
        if (Input.GetKeyDown(KeyCode.T))
        {
            foreach(InventorySlot x in inventory.Container)
            {
                print(x.item);
                print(inventory.Container.Count);
                StartCoroutine(inventoryUI.UpdateUIFromLoad(x));            
            }

        }
        playerAnim.SetFloat("Speed.X", movement.x);
        playerAnim.SetFloat("Speed.Y", movement.y);

    }

    //Crafting Coroutine
    public IEnumerator Crafting(ItemObject reactor, ItemObject reagent)
    {
        //Access reactor ItemObject Combine script. 
        //Passes reagent ItemObject into it.
        ItemObject io = reactor.Combine(reagent);
        //Updates UI
        inventoryUI.AddNewItem(io);
        inventoryUI.RemoveItem(reactor); inventoryUI.RemoveItem(reagent);
        yield return 1;       
        //Updates Inventory
        inventory.AddItem(io, 1);
        inventory.RemoveItem(reactor.id); inventory.RemoveItem(reagent.id);

    }


    private void FixedUpdate()
    {
        //print(Input.GetAxisRaw("Horizontal") + "Hori");
        //print(Input.GetAxisRaw("Vertical")+ " Verti");
        //movement = Vector2.zero;
        movement.x = Input.GetAxisRaw("Horizontal"); //left and right  
        movement.y = Input.GetAxisRaw("Vertical"); //up and down 
        if (movement.x > 0)
        {
            movement = Vector2.right;
        }
        else if (movement.x < 0)
        {
            movement = Vector2.left;
        }

        if (movement.y > 0)
        {
            movement = Vector2.up;
        }
        else if (movement.y < 0)
        {
            movement = Vector2.down;
        }

        if (movement.x != 0 || movement.y != 0) { playerAnim.SetBool("IsMoving", true); } else { playerAnim.SetBool("IsMoving", false); }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var item = collision.GetComponent<Item>();
        if (item)
        {
            inventory.AddItem(item.item, 1);
            inventoryUI.AddNewItem(item.item);
            Destroy(collision.gameObject);
        }
    }

    //Empties inventory upon application quit
    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }


    public void Move(float dirX, float dirY) //Joystick link
    {
        if(Mathf.Abs(dirX) > Mathf.Abs(dirY))
        {
            movement.x = dirX;
        }else if (Mathf.Abs(dirX) < Mathf.Abs(dirY))
        {
            movement.y = dirY;
        }
        //movement.x = dirX;
        //movement.y = dirY;

        //print(movement);
    }

    public void AnimationController()
    {

        //if(movement == new Vector2(0,0)) playerAnim
        //if (movement.y > 0) playerAnim.SetBool("WalkUp", true);
        //if (movement.y < 0) playerAnim.SetBool("WalkDown", true);
        //if (movement.x > 0) playerAnim.SetBool("WalkRight", true);
        //if (movement.x < 0) playerAnim.SetBool("WalkLeft", true);

    }

}