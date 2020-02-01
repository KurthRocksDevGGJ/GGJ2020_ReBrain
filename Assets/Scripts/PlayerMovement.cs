using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Photon.Pun.Demo.PunBasics
{
    public class PlayerMovement : MonoBehaviourPunCallbacks
    {
        public PlayerController playerController;

        public float moveSpeed;

        private float _horizontalMove = 0f;
        private bool _jump = false;

        // Update is called once per frame
        void Update()
        {
            if (photonView.IsMine)
            {
                _horizontalMove = moveSpeed;

                if(Input.GetButtonDown("Jump")) 
                {
                    _jump = true;
                }
            }
        }

        void FixedUpdate() {
            playerController.Move(_horizontalMove * Time.fixedDeltaTime, false, _jump);
            _jump = false;
        }
    }
}
