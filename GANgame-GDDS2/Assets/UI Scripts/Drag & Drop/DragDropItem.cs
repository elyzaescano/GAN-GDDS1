using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragDropItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    RectTransform rectTrans;
    CanvasGroup canvasGroup;
    Vector3 startPos;

    private void Awake()
    {
        rectTrans = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .7f;
        canvasGroup.blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTrans.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
    
    }
}
