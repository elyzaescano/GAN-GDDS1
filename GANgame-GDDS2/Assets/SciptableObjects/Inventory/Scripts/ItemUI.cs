using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public ItemObject item;
    private Image spriteImage;

    // Start is called before the first frame update
    void Awake()
    {
        spriteImage = GetComponent<Image>();
        UpdateImage(null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateImage(ItemObject item)
    {
        this.item = item;
        if (this.item != null)
        {
            spriteImage.color = Color.white;
            spriteImage.sprite = this.item.itemIcon;
        }
        else { spriteImage.color = Color.clear; }
    }
}
