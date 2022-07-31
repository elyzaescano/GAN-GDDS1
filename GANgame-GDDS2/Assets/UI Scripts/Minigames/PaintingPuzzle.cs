using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingPuzzle : MonoBehaviour
{
    public GameObject paintingPanel;
    public GameObject closeButton;
    [SerializeField] GameObject[] frames;
    [SerializeField] GameObject[] pieces;
    PauseScreen pause;

    public GameObject finalDoor;
    public bool piecesCollected;
    private void Start() 
    {
        pause = FindObjectOfType<PauseScreen>();
    }
    private void Update()
    {
        if (pieces[0].transform.IsChildOf(frames[0].transform) && pieces[1].transform.IsChildOf(frames[1].transform)
        && pieces[2].transform.IsChildOf(frames[2].transform))
        {
            print("Win");
            finalDoor.GetComponent<NextScene>().isLocked = false;
            paintingPanel.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Triggered");
            OpenPuzzle();
            pause.isPaused = true;
        }
    }

    public void OpenPuzzle()
    {
        if (piecesCollected)
        {
            paintingPanel.SetActive(true);
            
        }
    }

    public void Close()
    {
        paintingPanel.SetActive(false);
        pause.isPaused = false;
        Destroy(paintingPanel);
    }
}