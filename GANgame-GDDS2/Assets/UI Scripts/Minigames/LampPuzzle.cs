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
    }

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EventManager.InteractEvent += Interacted;
        }
    
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        EventManager.InteractEvent -= Interacted;
    }


    public void Interacted()
    {
        if (io.equippedItem != itemRequired) {
            //Event should include: Audio & Dialog
            print("WrongItem");
            FailedInteractEvent?.Invoke();
            return;
        }
        else 
        io.RemoveItem(io.equippedItem);
        io.equippedItem = null;
        EventManager.ItemEquip();
        bool completed = AddToCount();
        if (completed) FinishedPuzzleEvent?.Invoke();
        else UnfinishedPuzzleEvent?.Invoke();
    }

    bool AddToCount()
    {
        count++;
        bool puzzleComplete = count == 3 ? true : false;
        return puzzleComplete;
    }


    private void OnDisable()
    {
        EventManager.InteractEvent -= Interacted;

    }

}
