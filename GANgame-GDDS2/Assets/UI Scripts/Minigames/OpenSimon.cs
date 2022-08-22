using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSimon : MonoBehaviour
{
    public GameObject simonGame;
    public GameObject simonPanel;
    EventManager em;
    [SerializeField]RoomTeleporter gameDoor;
    bool isNear = false;

    // Start is called before the first frame update
    void Start()
    {
        em = FindObjectOfType<EventManager>();
        gameDoor = this.gameObject.GetComponent<RoomTeleporter>();
        EventManager.SimonWon += gameDoor.Teleport;
    }

    // Update is called once per frame
    void Update()
    {
        if(simonPanel != null && simonGame != null)
        {
            if(simonPanel.GetComponent<SimonSays>().win)
            {
                EventManager.InteractEvent -= SimonGame;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isNear = true;
            if (simonPanel.GetComponent<SimonSays>().win == false) EventManager.InteractEvent += SimonGame;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            EventManager.InteractEvent -= SimonGame;
            isNear = false;
        }
    }


    public void SimonGame()
    {
        if (isNear)
        {
            bool hasScript = simonGame ? true : false;
            if(hasScript) simonPanel.SetActive(true);
        }
        
    }

    public void Close()
    {
        simonPanel.SetActive(false);
        //Destroy(simonGame);

    }

    private void OnDisable()
    {
        EventManager.InteractEvent -= SimonGame;

    }

}
