using System;
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
        private int coinRemover = 2;
    

        public void OnDrag(PointerEventData eventData)
        {
            if (true)
            {         
                transform.position = Input.mousePosition;            
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            PlayerController pRef = GameObject.FindGameObjectWithTag("Runner").GetComponent<PlayerController>();
            if (pRef == null)
            {
                //Debug.Log("kein player gefunden");
            }
            if (pRef.GetCoins() > coinRemover)
            {
                //Debug.Log("möchte abziehen: " + 5);
                PhotonNetwork.Instantiate(myPrefab.name, new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), myPrefab.transform.rotation);
                pRef.RemoveCoins(coinRemover);
            }
            else
            {
                Debug.Log("not enough coins");
            }
            ResetPosition();
        }

        public void ResetPosition()
        {
            transform.localPosition = Vector2.zero;
        }
    }
}
