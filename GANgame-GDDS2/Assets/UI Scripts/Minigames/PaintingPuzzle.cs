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
    public AudioClip winSound;
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
    bool canShow;
    public static int count;

    private void OnEnable()
    {
        pause = FindObjectOfType<PauseScreen>();
        
        EventManager.MinigameCompleted += Close;
        activePanelIndex = -1;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            print("subscribed");
            ItemObject o = playerInventory.equippedItem;
            if (o == itemRequired || itemRequired == null)
            {
                canShow = true;
                EventManager.InteractEvent += Show;
                
            }
        }
    }

    public void Show() 
    {
        if (canShow)
        {
            activePanelIndex++;
            SetPanelActive(activePanelIndex);
            
            playerInventory.RemoveItem(playerInventory.equippedItem); //removes painting from inventory
            playerInventory.equippedItem = null;    //removes painting from equipped item slot
            EventManager.ItemEquip();   // updates the UI

            if(activePanelIndex == 1)
            {
                paintingPanel.SetActive(true);
            }    
            canShow = false;
            EventManager.InteractEvent -= Show;
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
            }
        }

        if (panelTrue)
        {
            UnlockDoor();
            EventManager.ConquerMinigame();
        }
        
    }

    public void Close()
    {
        pause.isPaused = false;

        GetComponent<AudioSource>().PlayOneShot(winSound, 0.3f);
        paintingPanel.SetActive(false);
        EventManager.MinigameCompleted -= Close;
    }

}