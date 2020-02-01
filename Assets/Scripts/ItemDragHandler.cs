﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Photon.Pun.Demo.PunBasics
{
    public class ItemDragHandler : MonoBehaviourPunCallbacks, IDragHandler, IEndDragHandler
    {    
        [SerializeField]
        private GameObject myPrefab;
    

        public void OnDrag(PointerEventData eventData)
        {
            if (true)
            {         
                transform.position = Input.mousePosition;            
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            PhotonNetwork.Instantiate(myPrefab.name, new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y),myPrefab.transform.rotation);
            ResetPosition();
        }

        public void ResetPosition()
        {
            transform.localPosition = Vector2.zero;
        }
    }
}
