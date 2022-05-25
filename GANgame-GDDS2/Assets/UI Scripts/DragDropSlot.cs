using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropSlot : MonoBehaviour, IDropHandler
{
    void Start()
    {
        StartCoroutine(PaintingPuzzleCheck());
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;

            eventData.pointerDrag.transform.SetParent(transform);
        }
    }

    IEnumerator PaintingPuzzleCheck()
    {
        yield return new WaitForSeconds(0.2f);

        if (transform.childCount > 1)
        {
            Transform itemToRemove = transform.GetChild(1);
            itemToRemove.parent = GameObject.FindGameObjectWithTag("holdingframe1").transform;
            itemToRemove.position = GameObject.FindGameObjectWithTag("holdingframe1").transform.position;
            if (GameObject.FindGameObjectWithTag("holdingframe1").transform.childCount > 1)
            {
                itemToRemove.parent = GameObject.FindGameObjectWithTag("holdingframe2").transform;
                itemToRemove.position = GameObject.FindGameObjectWithTag("holdingframe2").transform.position;

                if (GameObject.FindGameObjectWithTag("holdingframe2").transform.childCount > 1)
                {
                    itemToRemove.parent = GameObject.FindGameObjectWithTag("holdingframe3").transform;
                    itemToRemove.position = GameObject.FindGameObjectWithTag("holdingframe3").transform.position;
                }
                    if (GameObject.FindGameObjectWithTag("holdingframe3").transform.childCount > 1)
                    {
                        itemToRemove.parent = GameObject.FindGameObjectWithTag("holdingframe4").transform;
                        itemToRemove.position = GameObject.FindGameObjectWithTag("holdingframe4").transform.position;
                    }
            }

        }

    }

}
