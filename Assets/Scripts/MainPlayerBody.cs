using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerBody : MonoBehaviour {
    [SerializeField]
    private bool _isHittingWall = false;

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground") && !_isHittingWall) {
            _isHittingWall = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground") && _isHittingWall) {
            _isHittingWall = false;
        }
    }
    public bool isHittingWall() {
        return _isHittingWall;
    }
}