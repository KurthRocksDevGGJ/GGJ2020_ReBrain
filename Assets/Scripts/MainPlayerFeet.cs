
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerFeet : MonoBehaviour {
    [SerializeField]
    private bool _isGrounded = false;

    [SerializeField]
     Rigidbody2D _rb2 = null;
    [SerializeField]
    private float _springJumpForce = 1000F;

    private void Start() {
        _rb2 = GameObject.Find("Player").GetComponent<MainPlayer>().GetComponent<Rigidbody2D>();
        if (_rb2 == null)
            Debug.LogError("Feet::Start: Could not find player with Rigidbody2D", _rb2);
        Debug.LogWarning("Feet::Start(): Please check for bouncable objects... like springs...");
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            _isGrounded = true;
            GameObject.Find("Player").GetComponent<MainPlayer>().IsGrounded(_isGrounded);
        }

        if(other.gameObject.CompareTag("Bouncable")) {
            _rb2.AddForce(Vector2.up * _springJumpForce);
        }
    }
    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            _isGrounded = false;
            GameObject.Find("Player").GetComponent<MainPlayer>().IsGrounded(_isGrounded);
        }
        
        if (other.gameObject.CompareTag("spring"))
        {
            Debug.Log(other.gameObject.tag);
            Debug.Log("yeah");
            Debug.Log(other.gameObject.GetComponent<Animator>());
            other.gameObject.GetComponent<Animator>().SetBool("isSpringActivated",true);
        }
    }
    public bool isGrounded() {
        return _isGrounded;
    }

}