
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int doorTriggerID;

    public Rigidbody2D rb;

    Vector2 movement;

    public InventoryObject inventory;
    public ItemDatabaseObject database;
    public InventoryUI inventoryUI;
    public EventManager em;

    [SerializeField]
    public ItemObject _item;
    [SerializeField]
    private GameObject itemGO;
    bool inRangeOfItem;

    public Animator playerAnim;

    private void Start()
    {
        playerAnim = GetComponent<Animator>();
        inRangeOfItem = false;
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
            inventoryUI.RemoveItem(inventory.GetItemObject(0));
            inventory.DropItem(0, this.transform.position);            
            RefreshUI();
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
            RefreshUI();
        }
        playerAnim.SetFloat("Speed.X", movement.x);
        playerAnim.SetFloat("Speed.Y", movement.y);
    }

    //Mobile code for crafting
    ItemObject firstVariable;
    ItemObject secondVariable;
    public void GetCraftingItems(ItemObject craftingItem)
    {
        if (!firstVariable)
        {
            firstVariable = craftingItem;
        } else
        {
            secondVariable = craftingItem;
            StartCoroutine(Crafting(firstVariable, secondVariable));
        }
        print("first = " + firstVariable);
        print("Second = " + secondVariable);
    }

    //Crafting Coroutine
    public IEnumerator Crafting(ItemObject reactor, ItemObject reagent)
    {
        //Access reactor ItemObject Combine script. 
        //Passes reagent ItemObject into it.
        ItemObject io = reactor.Combine(reagent);
        if (inventory.equippedItem == reactor || reagent ) { inventory.equippedItem = null; }
        if (io == null) { firstVariable = null; secondVariable = null; yield break; } //If there is no resultant item, clear both variables and break the coroutine.
                                                                                      //Updates UI
        inventory.AddItem(io, 1);
        yield return 1;

        yield return 2;
        inventory.RemoveItem(reactor); inventory.RemoveItem(reagent);
        //Updates Inventory

        inventoryUI.AddNewItem(io);
        inventoryUI.RemoveItem(reactor); inventoryUI.RemoveItem(reagent);
        inventoryUI.StartUICoroutine();
        EventManager.ItemEquip();
        firstVariable = null;
        secondVariable = null;
    }

    public void RefreshUI()
    {
        foreach (InventorySlot x in inventory.Container)
        {
            StartCoroutine(inventoryUI.UpdateUI());
        }
    }

    public bool usingKBM;

    private void FixedUpdate()
    {
        //print(movement);
        //movement = Vector2.zero;
        
        if (usingKBM) { movement.x = Input.GetAxisRaw("Horizontal"); movement.y = Input.GetAxisRaw("Vertical"); } else {
            HorizontalMovement(Mathf.RoundToInt(movement.x)); VerticalMovement(Mathf.RoundToInt(movement.y));
        }
        if (movement.x != 0 || movement.y != 0) { playerAnim.SetBool("IsMoving", true); } else { playerAnim.SetBool("IsMoving", false); }
    }

    public void HorizontalMovement(int directionX) //Joystick link
    {
        //if(Mathf.Abs(dirX) > Mathf.Abs(dirY))
        //{
        //    movement.x = Mathf.Round(dirX);
        //}else if (Mathf.Abs(dirX) < Mathf.Abs(dirY))
        //{
        //    movement.y = Mathf.Round(dirY);
        //}
        movement.x = directionX;
        //movement.y = direction.y;

        //print(movement);
    }

    public void VerticalMovement(int DirectionY)
    {
        movement.y = DirectionY;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var item = collision.GetComponent<Item>();
        if (item)
        {
            inRangeOfItem = true;
            _item = item.item;
            itemGO = item.gameObject;
        }
    }

    public void startAddItemCoroutine()
    {
        StartCoroutine(AddItemToInventory());
    }

    public IEnumerator AddItemToInventory()
    {
        if (inRangeOfItem) { 
            inventory.AddItem(_item, 1);
            inventoryUI.AddNewItem(_item);
            Destroy(itemGO);
            yield return 1;
            inRangeOfItem = false;
            _item = null;
            itemGO = null;
        }
    }

    //Empties inventory upon application quit
    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }



 



}