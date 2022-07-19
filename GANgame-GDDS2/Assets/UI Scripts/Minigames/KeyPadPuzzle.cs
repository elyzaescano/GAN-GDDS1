using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPadPuzzle : MonoBehaviour
{
    [Header("Keypad")]
    //public ItemSpawn itemSpawn;
    public GameObject keyPadUI;

    bool inRange = false;
    EventManager em;

    //For Door
    public RoomTeleporter room4TP;
    public ItemObject itemToBlockRoom4;

    // Start is called before the first frame update
    void Start()
    {
        em = FindObjectOfType<EventManager>();
        if(room4TP != null)
        {
            room4TP.itemRequired = itemToBlockRoom4;
            keyPadUI.GetComponent<KeypadScreen>().RoomTPBlock = room4TP;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            EventManager.InteractEvent += UnlockKey;
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            EventManager.InteractEvent -= UnlockKey;
            inRange = false;
        }
    }


    public void UnlockKey()
    {
        if (inRange)
        {
            keyPadUI.SetActive(true);
            GetComponent<KeyPadPuzzle>().enabled = true;
        }
    }

    public void CloseKey()
    {
        keyPadUI.SetActive(false);
        GetComponent<KeyPadPuzzle>().enabled = false;
    }

}
