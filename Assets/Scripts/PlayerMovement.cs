using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Photon.Pun.Demo.PunBasics 
{
    public class PlayerMovement : MonoBehaviourPunCallbacks {
        public PlayerController playerController;

        public GameObject hammerPrefab;

        public float moveSpeed;

        private float _horizontalMove = 0f;
        private bool _jump = false;

        [SerializeField]
        private bool _overridePhotonValue = false;

        private GameObject _hammerInstance;

        private Vector2 _currentHammerEndPos;

        private bool _bhammerFlying = false;

        private void Start()
        {
            SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;
        }

        // Update is called once per frame
        void Update() {
            _horizontalMove = moveSpeed;

            if(Input.GetButtonDown("Jump")) 
            {
                _jump = true;
            }

            if(_hammerInstance != null && _bhammerFlying)
            {
                _hammerInstance.transform.position = Vector2.MoveTowards(_hammerInstance.transform.position, _currentHammerEndPos, 15 * Time.deltaTime);

                if(Vector3.Distance(_hammerInstance.transform.position, _currentHammerEndPos) <= 0)
                {
                    PhotonNetwork.Destroy(_hammerInstance);
                    _hammerInstance = null;
                    _currentHammerEndPos = Vector2.zero;
                    _bhammerFlying = false;
                }
            }
        }

        void FixedUpdate() {
            playerController.Move(_horizontalMove * Time.fixedDeltaTime, false, _jump);
            _jump = false;
        }

        private void SwipeDetector_OnSwipe(SwipeData data)
        {
            if(data.Direction == SwipeDirection.None)
            {
                _jump = true;
            }
            else
            {
                SpawnHammer(data);
            }
        }

        private void SpawnHammer(SwipeData data)
        {
            if(!_bhammerFlying)
            {
                _hammerInstance = PhotonNetwork.Instantiate(hammerPrefab.name, transform.position, hammerPrefab.transform.rotation);
                _currentHammerEndPos = Camera.main.ScreenToWorldPoint(new Vector3(data.StartPosition.x, data.StartPosition.y, 3));
                _bhammerFlying = true;
            }
        }
    }
}