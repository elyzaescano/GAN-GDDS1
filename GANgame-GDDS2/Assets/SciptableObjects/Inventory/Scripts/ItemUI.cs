using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public ItemObject item;
    public Image spriteImage;
    public InventoryObject playerInventory;
    InventoryUI UIScript;
    public Image UI;
    public int itemSlotID;
    public bool selected = false;
    public Text descriptionText;

    public PlayerController pc;
    public EventManager em;
    GameObject[] inventoryButtonObject;
    [SerializeField] List<Button> inventoryButtons = new List<Button>();

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
        buttonSound = GameObject.Find("Button Press").GetComponent<AudioSource>();
        descriptionText = GameObject.Find("Description").GetComponent<Text>();
        playerInventory = GameObject.Find("Player").GetComponent<InventoryObject>();

        inventoryButtonObject = GameObject.FindGameObjectsWithTag("InventoryButtons");
        foreach(GameObject go in inventoryButtonObject)
        {
            Button butt =  go.GetComponent<Button>();
            inventoryButtons.Add(butt);
        }
    }

    private void Start()
    {
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

    public void Select()
    {
        if (item == null) return;
        if (!selected)
        {
            if (CombineButton.readyToCraft) { GiveItemObject(); return; }
            UIScript.UnselectItems(itemSlotID);
            foreach(Button butt in inventoryButtons)
            {
                butt.gameObject.SetActive(true);
            }
            foreach (Button butt in inventoryButtons)
            {
                butt.onClick.RemoveAllListeners();
            }
            spriteImage.color = Color.black;
            GiveItemObject();
            inventoryButtons[2].onClick.AddListener(DropItemFromUI);
            inventoryButtons[1].onClick.AddListener(EquipItemFromUI);
            descriptionText.text = item.description;
            selected = !selected;
        }
        else
        {
            spriteImage.color = Color.white;
            foreach (Button butt in inventoryButtons)
            {
                butt.onClick.RemoveAllListeners();
            }
            foreach (Button butt in inventoryButtons)
            {
                butt.gameObject.SetActive(false);
            }
            selected = !selected;
        }
    }


    public void GiveItemObject()
    {
        //Aivated upon being selected. Gives the item and its ui to the combine button for it to be accessed.
        ItemObject item = playerInventory.Container[itemSlotID].item;
        var combineVariables = FindObjectOfType<CombineButton>();
        combineVariables.UpdateItemVariables(item,this);
        buttonSound.Play();
        //print(item);
    }

    public void DeSelect()
    {

        foreach (Button butt in inventoryButtons)
        {
            butt.gameObject.SetActive(false);
        }
        descriptionText.text = null;
        selected = false;
       
        if(spriteImage.sprite != null)
        {
            spriteImage.color = Color.white;
        }
    }

    public void EnableButtons()
    {

    }

    public void DropItemFromUI()
    {
        if(spriteImage != null)
        playerInventory.Drop(itemSlotID, item);
        buttonSound.Play();

        //UpdateImage(null);
        UIScript.StartUICoroutine();
        DeSelect();
        
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
        selected = false;
    }

    
}
