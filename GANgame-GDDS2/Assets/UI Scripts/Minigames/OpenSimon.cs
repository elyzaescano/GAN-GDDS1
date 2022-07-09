using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSimon : MonoBehaviour
{
    public GameObject simonGame;
    public GameObject simonPanel;
    EventManager em;
    bool isNear = false;

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
            EventManager.InteractEvent += SimonGame;
            isNear = true;
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
        Destroy(simonGame);

    }

    private void OnDisable()
    {
        EventManager.InteractEvent -= SimonGame;

    }

}
