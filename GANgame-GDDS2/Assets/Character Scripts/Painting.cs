using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : MonoBehaviour
{
    //for painting
    [Header("Painting")]
    public GameObject frontPaint;
    public GameObject backPaint;
    bool viewPaint = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            viewPaint = true;
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        viewPaint = false;
    }

    public void FlipBack() //flip towards back 
    {
        if (viewPaint == false) return;
        frontPaint.SetActive(false);
        backPaint.SetActive(true);
        //viewPaint = false;
        print("flipped back");
    }
    public void FlipFront() //flip towards front 
    {
        if(viewPaint == false) return;
        frontPaint.SetActive(true);
        backPaint.SetActive(false);
        //viewPaint = false;
        print("flipped front");
    }

    public void Close()
    {
        frontPaint.SetActive(false);
        backPaint.SetActive(false);
    }
}
