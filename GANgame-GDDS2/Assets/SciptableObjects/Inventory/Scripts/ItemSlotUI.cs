using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlotUI : MonoBehaviour
{
    public EventManager em;


    // Start is called before the first frame update
    void Start()
    {
        em = FindObjectOfType<EventManager>();
        //EventManager.OpenInventory += EnableButtons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableButtons()
    {

    }
}
