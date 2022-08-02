using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingPuzzle : MonoBehaviour
{
    [Header("Painting UI")]
    public GameObject paintingPanel;
    public GameObject closeButton;
    [SerializeField] GameObject[] frames;
    [SerializeField] GameObject[] pieces;
    PauseScreen pause;

    [Header("Painting In Game")]
    public InventoryObject playerInventory;
    public ItemObject itemRequired;

    public EventManager em;
    int triggerID;

    public GameObject[] panels;
    [SerializeField] private int activePanelIndex;
    public NextScene endDoor;
    bool panelTrue; //if painting is completed, sets to true

    public static int count;

    private void Start()
    {
        pause = FindObjectOfType<PauseScreen>();
        
        activePanelIndex = 2;
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            print("subscribed");
            ItemObject o = playerInventory.equippedItem;
            if (o == itemRequired || itemRequired == null)
            {
                EventManager.InteractEvent += Show;
                
            }
        }
    }

    public void Show() 
    {
        activePanelIndex--;
        SetPanelActive(activePanelIndex);
        
        playerInventory.RemoveItem(playerInventory.equippedItem); //removes painting from inventory
        playerInventory.equippedItem = null;    //removes painting from equipped item slot
        EventManager.ItemEquip();   // updates the UI

        if(activePanelIndex == 0)
        {
            paintingPanel.SetActive(true);
        }

        
    }

    void SetPanelActive(int panelIndex)
    {
        panels[panelIndex].SetActive(true);
    }

    public void UnlockDoor()
    {
        endDoor.isLocked = false;
    }

    private void Update()
    {
        if (paintingPanel.activeInHierarchy)
        {
            if (pieces[0].transform.IsChildOf(frames[0].transform) && pieces[1].transform.IsChildOf(frames[1].transform)
            && pieces[2].transform.IsChildOf(frames[2].transform))
            {
                panelTrue = true;
                paintingPanel.SetActive(false);
            }
        }

        if (panelTrue)
        {
            UnlockDoor();
        }
        if (activePanelIndex < 0)
            activePanelIndex = 0;        
    }

    public void Close()
    {
        paintingPanel.SetActive(false);
        pause.isPaused = false;
        Destroy(paintingPanel);
    }

}