using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PaintingPuzzle : MonoBehaviour
{
    [Header("Painting UI")]
    public GameObject paintingPanel;
    public GameObject closeButton;
    [SerializeField] GameObject[] frames;
    [SerializeField] GameObject[] pieces;
    [SerializeField] List<Dialogue.Conversation> fillPuzzleDialogues;
    public AudioClip winSound;
    PauseScreen pause;

    [Header("Painting In Game")]
    public InventoryObject playerInventory;
    public ItemObject itemRequired;
    [SerializeField] AudioSource au;

    public EventManager em;
    int triggerID;

    public GameObject[] panels;
    [SerializeField] private int activePanelIndex;
    public NextScene endDoor;
    bool panelTrue; //if painting is completed, sets to true
    bool canShow;
    public static int count;

    GameObject dialogBox;
    public UnityEvent DialogWithoutPaintingEquipped;
    public UnityEvent OnPuzzleOpenEvent;
    public UnityEvent OnPuzzleCloseEvent;

    private void Start()
    {
        pause = FindObjectOfType<PauseScreen>();
        au = GetComponent<AudioSource>();
        EventManager.MinigameCompleted += Close;
        dialogBox = EventManager.dialogBox;
        activePanelIndex = -1;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            print("subscribed");
            EventManager.EquipItem += RefreshColliders;
            ItemObject o = playerInventory.equippedItem;
            if (o == itemRequired || itemRequired == null)
            {
                canShow = true;
                EventManager.InteractEvent += Show;

            }
            else EventManager.InteractEvent += PlayDialogOnTap;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        EventManager.InteractEvent -= PlayDialogOnTap;
        EventManager.InteractEvent -= Show;
        EventManager.InteractEvent -= OpenPuzzle;

        EventManager.EquipItem -= RefreshColliders;
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

            PlayDialog(fillPuzzleDialogues[0]);



            if(activePanelIndex == 1)
            {
                PlayDialog(fillPuzzleDialogues[1]);
                itemRequired = null;
            }    
            canShow = false;
            EventManager.InteractEvent -= Show;
        }
    }

    void SetPanelActive(int panelIndex)
    {
        if (panelIndex == 2) OpenPuzzle();else panels[panelIndex].SetActive(true);
    }

    void OpenPuzzle()
    {
        paintingPanel.SetActive(true);
        OnPuzzleOpenEvent?.Invoke();
    }

    public void UnlockDoor()
    {
        endDoor.isLocked = false;
        foreach (Collider2D col in this.gameObject.GetComponents<Collider2D>())
        {
            col.enabled = false;
        }
    }

    private void Update()
    {
        if (paintingPanel.activeInHierarchy)
        {
            if (pieces[0].transform.IsChildOf(frames[0].transform) && pieces[1].transform.IsChildOf(frames[1].transform)
            && pieces[2].transform.IsChildOf(frames[2].transform))
            {
                UnlockDoor();
                EventManager.ConquerMinigame();
            }
        }
    }

    public void Close()
    {
        pause.isPaused = false;
        OnPuzzleCloseEvent?.Invoke();
        au.PlayOneShot(winSound, 0.3f);
        paintingPanel.SetActive(false);
        EventManager.MinigameCompleted -= Close;
        EventManager.InteractEvent -= PlayDialogOnTap;
        EventManager.InteractEvent -= Show;
    }

    void PlayDialogOnTap()
    {
        DialogWithoutPaintingEquipped?.Invoke();
    }

    public void PlayDialog(Dialogue.Conversation convo)
    {
        dialogBox.SetActive(true);
        Dialogue.DialogDisplay dd = dialogBox.GetComponent<Dialogue.DialogDisplay>();

        dd.conversation = convo;
        dd.simulateClick = true;


    }

    void RefreshColliders()
    {
        Collider2D[] col = GetComponents<Collider2D>();
        StartCoroutine(RefreshColliderRoutine(col));
    }

    IEnumerator RefreshColliderRoutine(Collider2D[] cols)
    {
        foreach (Collider2D co in cols)
        {
            co.enabled = false;
        }
        yield return null;
        foreach (Collider2D co in cols)
        {
            co.enabled = true;
        }
    }

    private void OnDisable()
    {
        EventManager.InteractEvent -= PlayDialogOnTap;
        EventManager.InteractEvent -= Show;
        EventManager.InteractEvent -= OpenPuzzle;

        EventManager.EquipItem -= RefreshColliders;
    }

}