using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Photon.Pun.Demo.PunBasics 
{
    public class SwipeLogger : MonoBehaviourPunCallbacks
    {
        // Start is called before the first frame update
        void Awake()
        {
            SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;
        }

        // Update is called once per frame
        void SwipeDetector_OnSwipe(SwipeData data)
        {
            Debug.Log("Swipe in Direction: " + data.Direction);
        }
    }
}
