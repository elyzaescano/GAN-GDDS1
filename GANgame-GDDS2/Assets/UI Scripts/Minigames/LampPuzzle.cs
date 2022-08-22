using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class LampPuzzle : MonoBehaviour
{
    [Header("Lamp In Game")]
    public EventManager em;
    [SerializeField]InventoryObject io;
    [SerializeField] ItemObject itemRequired;
    bool canInteract;
    AudioSource au;
    [Header("Events")]
    public UnityEvent FailedInteractEvent;
    public UnityEvent FinishedPuzzleEvent;
    public UnityEvent UnfinishedPuzzleEvent;

    [SerializeField] int maxCount;
    static int count;


    private void Awake()
    {
        io = FindObjectOfType<InventoryObject>();
        au = GetComponent<AudioSource>();
        canInteract = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canInteract = true;
        if (collision.CompareTag("Player") && canInteract)
        {
            EventManager.EquipItem += RefreshColliders;
            if (io.equippedItem != itemRequired)
            {
                //Event should include: Audio & Dialog
                EventManager.InteractEvent += PlayFailedDialog;
                return;
            }else EventManager.InteractEvent += Interacted;
        }
    
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        EventManager.InteractEvent -= Interacted;
        EventManager.EquipItem -= RefreshColliders;
        EventManager.InteractEvent -= PlayFailedDialog;

    }


    public void Interacted()
    {
        print(count);
        DisableInput();
        io.RemoveItem(io.equippedItem);
        io.equippedItem = null;
        bool completed = AddToCount();
        if (completed) FinishedPuzzleEvent?.Invoke();
        else UnfinishedPuzzleEvent?.Invoke();
    }

    bool AddToCount()
    {
        count++;
        bool puzzleComplete = count == 5 ? true : false;
        EventManager.ItemEquip();
        return puzzleComplete;
    }

    void DisableInput()
    {
        EventManager.InteractEvent -= Interacted;
        canInteract = false;
    }

    void PlayFailedDialog()
    {
        //Event should include: Audio & Dialog

        FailedInteractEvent?.Invoke();
        DisableInput();
    }


    private void OnDisable()
    {
        EventManager.InteractEvent -= Interacted;
        EventManager.EquipItem -= RefreshColliders;
        EventManager.InteractEvent -= PlayFailedDialog;

    }

    void RefreshColliders()
    {
        Collider2D[] col = GetComponents<Collider2D>();
        StartCoroutine(RefreshColliderRoutine(col));
    }

    IEnumerator RefreshColliderRoutine(Collider2D[] cols)
    {
        foreach(Collider2D co in cols)
        {
            co.enabled = false;
        }
        yield return null;
        foreach (Collider2D co in cols)
        {
            co.enabled = true;
        }
    }

}
