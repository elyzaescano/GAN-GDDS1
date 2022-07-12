
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
    public GameObject itemGO;

    public Animator playerAnim;

    bool isMoving = false;
    [Header("Audio")]
    public AudioSource pickup;
    public AudioSource walking;
    private void Start()
    {    
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

       

        if (Input.GetKeyDown(KeyCode.E)) inventory.Save();

        //if (Input.GetKeyDown(KeyCode.Space)) inventory.Load(); 
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

        if (movement.x != 0 || movement.y != 0)
        {
            playerAnim.SetFloat("Speed.X", movement.x);
            playerAnim.SetFloat("Speed.Y", movement.y);

            playerAnim.SetBool("IsMoving", true);
            
        }
        else
        {
            playerAnim.SetBool("IsMoving", false);
            
        }
    }

    //Mobile code for crafting
    ItemObject firstVariable;
    ItemUI firstItemUI;
    ItemObject secondVariable;
    ItemUI secondItemUI;
    public void GetCraftingItems(ItemObject craftingItem, ItemUI itemIcon)
    {
        if (!firstVariable)
        {
            firstVariable = craftingItem;
            firstItemUI = itemIcon;
        } else
        {
            secondVariable = craftingItem;
            secondItemUI = itemIcon;
            int i = firstVariable.Combine(secondVariable) ? 1 : 0;
            
            int o = secondVariable.Combine(firstVariable) ? 1 : 0;
            print(i+o);
            if (i+o == 2)
            {
                StartCoroutine(Crafting(firstVariable, secondVariable));
            }else
            {

                StartCoroutine(UnCrafting(firstVariable,secondVariable,firstItemUI,secondItemUI));
            }
        }
            
        print("first = " + firstVariable);
        print("Second = " + secondVariable);
    }
    [SerializeField] AudioClip nope;
    IEnumerator UnCrafting( ItemObject crafting1, ItemObject crafting2, ItemUI ui1, ItemUI ui2)
    {
        firstVariable = null; secondVariable = null; firstItemUI = null; secondItemUI = null;
        yield return null;
        ui1.DeSelect(); ui2.DeSelect();
        GetComponent<AudioSource>().PlayOneShot(nope,.5f);
    }

    //Crafting Coroutine
    public IEnumerator Crafting(ItemObject reactor, ItemObject reagent)
    {
        //Access reactor ItemObject Combine script. 
        //Passes reagent ItemObject into it.
        ItemObject io = reactor.Combine(reagent);
        if (inventory.equippedItem == reactor || reagent ) { inventory.equippedItem = null; } //If there is no resultant item, clear both variables and break the coroutine.
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
        
        if (usingKBM) 
        { 
            movement.x = Input.GetAxisRaw("Horizontal"); movement.y = Input.GetAxisRaw("Vertical");
            
        } 
        else 
        {
            
            HorizontalMovement(Mathf.RoundToInt(movement.x)); VerticalMovement(Mathf.RoundToInt(movement.y));
            
            
        }
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

    //public void OnTriggerEnter2D(Collider2D collision)
    //{
    //    EventManager.InteractEvent += startAddItemCoroutine;
    //    var item = collision.GetComponent<Item>();
    //    if (item)
    //    {
    //        _item = item.item;
    //        itemGO = item.gameObject;
    //        print("Can add" + item.name);
    //    }

    //}

    //public void OnTriggerExit2D(Collider2D collision)
    //{
    //    EventManager.InteractEvent -= startAddItemCoroutine;
    //    print(collision.name + " is not in range");
    //}

    public void startAddItemCoroutine()
    {
        StartCoroutine(AddItemToInventory());
    }

    public IEnumerator AddItemToInventory()
    {

        inventory.AddItem(_item, 1);
        inventoryUI.AddNewItem(_item);
        Destroy(itemGO);
        pickup.Play();
        yield return 1;
        _item = null;
        itemGO = null;
        
    }

    //Empties inventory upon application quit
    private void OnApplicationQuit()
    {
        EventManager.InteractEvent -= startAddItemCoroutine;
        inventory.Container.Clear();
    }

    

 



}