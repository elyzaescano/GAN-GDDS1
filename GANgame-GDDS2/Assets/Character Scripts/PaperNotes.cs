using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PaperNotes : MonoBehaviour
{
    public Text noteText;
    [TextArea(15, 30)]
    public string note; //ur input 
    public GameObject paperNote;
    protected bool viewNote = false;

    public InventoryObject playerInventory; //checks with player inventory 
    public ItemObject itemRequired; //item that need to be used
    public bool itemNeeded; //check if item is required to interact 
    protected bool canOpen = true; //checks if player can open note
    EventManager em;

    public UnityEvent itemNeededToInteract;
    public UnityEvent UponSuccesfulInteractEvent;
    public UnityEvent NoteCloseEvent;
    GameObject dialogBox;
    [SerializeField] bool hasConvo => itemNeeded;

    // Start is called before the first frame update
    void Start()
    {
        //noteText.text = note;
        dialogBox = EventManager.dialogBox;
        em = FindObjectOfType<EventManager>();
        playerInventory = GameObject.Find("Player").GetComponent<InventoryObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.TryGetComponent<InventoryObject>(out InventoryObject io))
        {
            ItemObject obj = io.equippedItem;
            canOpen = false;
            if ((itemNeeded && itemRequired == obj) || !itemNeeded && itemRequired == null)
            {
                canOpen = true;
                EventManager.InteractEvent += OpenNote;
            }
            else if (hasConvo) EventManager.InteractEvent += PlayFailedDialog;
        }

        //EventManager.InteractEvent += OpenNote;
        viewNote = true;

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            EventManager.InteractEvent -= OpenNote;
            EventManager.InteractEvent -= PlayFailedDialog;
            viewNote = false;
        }
    }


    public void OpenNote()
    {
        if (viewNote && canOpen)
        {
            UponSuccesfulInteractEvent?.Invoke();
            noteText.text = note;
            paperNote.GetComponentInChildren<Button>().onClick.AddListener(Close);
            viewNote = false;
        }
    }
    public void Close()
    {
        UponSuccesfulInteractEvent.RemoveAllListeners();
        NoteCloseEvent?.Invoke();
        paperNote.GetComponentInChildren<Button>().onClick.RemoveListener(Close);
        canOpen = false;
    }

    public void PlayFailedDialog()
    {
        itemNeededToInteract?.Invoke();
    }


    public void PlayDialog(Dialogue.Conversation convo)
    {
        dialogBox.SetActive(true);
        Dialogue.DialogDisplay dd = dialogBox.GetComponent<Dialogue.DialogDisplay>();

        dd.conversation = convo;
        dd.simulateClick = true;
    }

    private void OnDisable()
    {
        EventManager.InteractEvent -= OpenNote;
    }

}
