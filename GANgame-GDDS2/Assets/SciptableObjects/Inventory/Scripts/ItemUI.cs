using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public ItemObject item;
    private Image spriteImage;
    public InventoryObject playerInventory;
    InventoryUI UIScript;
    public Image UI;
    public int itemSlotID;
    public bool selected = false;
    public Text descriptionText;

    public PlayerController pc;
    public EventManager em;
    public GameObject[] inventoryButtons;

    public AudioSource buttonSound;

    // Start is called before the first frame update
    void Awake()
    {
        spriteImage = GetComponent<Image>();
        UpdateImage(null);
        pc = FindObjectOfType<PlayerController>();
        UIScript = FindObjectOfType<InventoryUI>();
        em = FindObjectOfType<EventManager>();
        UI = UIScript.GetComponent<Image>();
        buttonSound = FindObjectOfType<AudioSource>();
        descriptionText = GameObject.Find("Description").GetComponent<Text>();

    }

    private void Start()
    {
        foreach(GameObject go in inventoryButtons) //elyza says Basically sets all the inventory UI inactive
        {
            go.SetActive(false);
        }
        EventManager.OpenInventory += EnableButtons;
    }

    public void UpdateImage(ItemObject item) //elyza says As the function says, displays the sprite of the item in the inventory
    {
        this.item = item;
        if (this.item != null)
        {
            spriteImage.sprite = this.item.itemIcon;
            if (UI.color == Color.white) spriteImage.color = Color.white; 
            else if(UI.color == Color.clear) spriteImage.color = Color.clear;         //elyza says What is the point of all the Color. references?
        }
        else { spriteImage.sprite = null; spriteImage.color = Color.clear; }
    }

    public void GiveItemObject()
    {
        if (!selected)
        {
            spriteImage.color = Color.black;
            ItemObject item = playerInventory.Container[itemSlotID].item;
            buttonSound.Play();
            descriptionText.text = item.description;
            //print(item);
            pc.GetCraftingItems(item,this);
            selected = true;
            
        }
    }

    public void DeSelect()
    {
        spriteImage.color = Color.white;
        descriptionText.text = null;
        selected = false;
       
    }

    public void EnableButtons()
    {
        print("Enabling Buttons");
        foreach(GameObject go in inventoryButtons)
        {
            go.SetActive(true);
        }
    }

    public void DropItemFromUI()
    {
        buttonSound.Play();
        playerInventory.DropItem(itemSlotID,pc.gameObject.transform.position);
        //UpdateImage(null);
        UIScript.StartUICoroutine();
        
    }

    public void EquipItemFromUI()
    {
        buttonSound.Play();
        playerInventory.equippedItem = item;
        StartCoroutine(EquipCoroutine());
        
    }

    public IEnumerator EquipCoroutine()
    {
        yield return 1;
        EventManager.ItemEquip();
    }

    private void OnDisable()
    {
        EventManager.OpenInventory -= EnableButtons;
    }

    
}
