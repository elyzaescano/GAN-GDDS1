using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSimon : MonoBehaviour
{
    public GameObject simonGame;
    public GameObject simonPanel;
    bool isNear = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isNear = false;
        }
    }


    public void SimonGame()
    {
        if (isNear)
        {
            simonPanel.SetActive(true);
        }
        
    }

    public void Close()
    {
        simonPanel.SetActive(false);
        Destroy(simonGame);

    }
}
