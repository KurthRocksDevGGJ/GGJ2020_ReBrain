using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Photon.Pun.Demo.PunBasics
{
    public class DeathCheck : MonoBehaviourPunCallbacks
    {
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        void OnTriggerEnter2D(Collider2D col) {
            if(col.gameObject.GetComponent<PlayerManager>() != null)
            {
                GameManager.Instance.LeaveRoom();
            }
        }
    }
}
