
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerFeet : MonoBehaviour {
    [SerializeField]
    private bool _isGrounded = false;

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            _isGrounded = true;
            GameObject.Find("Player").GetComponent<MainPlayer>().IsGrounded(_isGrounded);
        }
    }
    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            _isGrounded = false;
            GameObject.Find("Player").GetComponent<MainPlayer>().IsGrounded(_isGrounded);
        }
    }
    public bool isGrounded() {
        return _isGrounded;
    }
}