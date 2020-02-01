using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
   
{    
    public bool IsMoved { get; private set; }
   

    public void OnDrag(PointerEventData eventData)
    {
        if (!IsMoved)
        {         
            transform.position = Input.mousePosition;            
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!IsMoved)
        {
            IsMoved = true;            
        }
    }

    public void ResetPosition()
    {
        transform.localPosition = Vector2.zero;
        IsMoved = false;
    }
}
