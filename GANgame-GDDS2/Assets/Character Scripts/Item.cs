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




}
