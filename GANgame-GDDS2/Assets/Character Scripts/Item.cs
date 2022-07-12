using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[System.Serializable]
public class Item : MonoBehaviour
{
    public ItemObject item;
    //Item hitbox variables


    SpriteRenderer sr;
    private void Start()
    {
        // Retreive properties from ItemObject and populate it iinto this item.
        // E.g. populate sprite, as well as other item attributes.
        sr = GetComponent<SpriteRenderer>();
        //sr.sprite = item.s
    }
    bool isused =false;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerController>();
        if(player && !isused)
        {
            EventManager.InteractEvent += player.startAddItemCoroutine;
            player._item = this.item;
            player.itemGO = this.gameObject;
            print("added");
            isused = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerController>();
        if (player)
        {
            EventManager.InteractEvent -= player.startAddItemCoroutine;
            isused = false;
        }
    }

}
