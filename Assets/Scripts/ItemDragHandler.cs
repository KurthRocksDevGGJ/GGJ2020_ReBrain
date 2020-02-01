using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
   
{    
    public bool IsMoved { get; private set; }
    [SerializeField]
    private GameObject myPrefab;
   

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
            Instantiate(myPrefab, new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y),myPrefab.transform.rotation);
            
            ResetPosition();

        }
    }

    public void ResetPosition()
    {
        transform.localPosition = Vector2.zero;
        IsMoved = false;
    }
}
