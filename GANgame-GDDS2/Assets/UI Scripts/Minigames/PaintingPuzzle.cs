using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingPuzzle : MonoBehaviour
{
    public GameObject paintingPanel;
    [SerializeField] GameObject[] frames;
    [SerializeField] GameObject[] pieces;

    private void Update()
    {
        if (pieces[0].transform.IsChildOf(frames[0].transform) && pieces[1].transform.IsChildOf(frames[1].transform))
        {
            print("Win");
            paintingPanel.SetActive(false);
        }
    }
}