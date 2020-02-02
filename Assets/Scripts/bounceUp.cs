using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Photon.Pun.Demo.PunBasics {
    public class bounceUp : MonoBehaviourPunCallbacks
       
    {
        public float bounceForce = 20;

        [SerializeField]
        private Animator anim;
        private bool isTriggered = false;
        // Start is called before the first frame update
        void Start()
        {            

        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var playerM = collision.gameObject.GetComponent<PlayerController>();
            
            if (playerM != null && !isTriggered)
            {
                if(photonView.IsMine)
                    anim.SetTrigger("bounce");
                    
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, bounceForce), ForceMode2D.Impulse);                
                isTriggered = true;
            }
        }



    }
}