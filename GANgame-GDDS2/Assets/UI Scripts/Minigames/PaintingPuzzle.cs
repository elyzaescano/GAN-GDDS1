using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingPuzzle : MonoBehaviour
{
    public GameObject paintingPanel;
    public GameObject closeButton;
    [SerializeField] GameObject[] frames;
    [SerializeField] GameObject[] pieces;

    bool isNear = false;

    private void Update()
    {
        if (pieces[0].transform.IsChildOf(frames[0].transform) && pieces[1].transform.IsChildOf(frames[1].transform))
        {
            print("Win");
            EventManager.CompletePainting();
            paintingPanel.SetActive(false);
        }
        else
        {
            closeButton.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isNear = true;
            OpenPuzzle();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isNear = false;
        }
    }

    public void OpenPuzzle()
    {
        paintingPanel.SetActive(true);
    }

    public void Close()
    {
        paintingPanel.SetActive(false);
        Destroy(paintingPanel);
    }
}