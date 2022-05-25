using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingPuzzle : MonoBehaviour
{
    [SerializeField] GameObject[] frames;
    [SerializeField] GameObject[] pieces;
    int pieceOrder;
    int frameOrder;

    private void Update()
    {
        if (pieces[0].transform.IsChildOf(frames[0].transform) && pieces[1].transform.IsChildOf(frames[1].transform) &&
            pieces[2].transform.IsChildOf(frames[2].transform) && pieces[3].transform.IsChildOf(frames[3].transform))
        {
            print("Win");
        }
    }
}