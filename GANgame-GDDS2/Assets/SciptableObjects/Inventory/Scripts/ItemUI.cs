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
        buttonSound = FindObjectOfType<AudioSource>();
        descriptionText = GameObject.Find("Description").GetComponent<Text>();

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
        if (!selected)
        {
            spriteImage.color = Color.black;
            inventoryButtons[0].onClick.AddListener(GiveItemObject);
            inventoryButtons[1].onClick.AddListener(DropItemFromUI);
            inventoryButtons[2].onClick.AddListener(EquipItemFromUI);
            selected = !selected;
        }
        else
        {
            spriteImage.color = Color.white;
            foreach (Button butt in inventoryButtons)
            {
                butt.onClick.RemoveAllListeners();
            }
            selected = !selected;
        }
    }
    public void GiveItemObject()
    {
        ItemObject item = playerInventory.Container[itemSlotID].item;
        buttonSound.Play();
        descriptionText.text = item.description;
        //print(item);
        pc.GetCraftingItems(item,this);
        selected = true;    
    }

    public void DeSelect()
    {
        spriteImage.color = Color.white;
        descriptionText.text = null;
        selected = false;
       
    }

    public void EnableButtons()
    {

    }

    public void DropItemFromUI()
    {
        playerInventory.DropItem(itemSlotID, pc.gameObject.transform.position, 0);
        buttonSound.Play();

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

    }

    
}
