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

    // Start is called before the first frame update
    void Start()
    {
        em = FindObjectOfType<EventManager>();
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
