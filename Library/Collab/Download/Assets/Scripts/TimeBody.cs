using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour {
    [Header("RewindTime Preferences")]
    public float recordTime = 5F;

    [Header("Debugging Watch")]
    [SerializeField]
    private bool _isRewinding = false;
    [SerializeField]
    List<PointInTime> _pointsInTime = null;
    [SerializeField]
    private Rigidbody2D _rigidbody2D = null;

    // Start is called before the first frame update
    void Start() {
        _pointsInTime = new List<PointInTime>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Backspace))
            StartRewind();
        if (Input.GetKeyUp(KeyCode.Backspace))
            StopRewind();
    }
    private void FixedUpdate() {
        if (_isRewinding)
            Rewind();
        else
            Record();
    }

    private void Rewind() {
        if(_pointsInTime.Count > 0) {
            PointInTime _thatPointInTime = _pointsInTime[0];
            transform.position = _thatPointInTime.position;
            //transform.rotation = _thatPointInTime.rotation;
            //transform.velocity = _thatPointInTime.velocity;
            //transform.angularVelocity  = _thatPointInTime.angularVelocity ;
            _pointsInTime.RemoveAt(0);
        } else {
            StopRewind();
        }
    }

    private void Record() {
        if(_pointsInTime.Count > Mathf.Round(recordTime / Time.deltaTime)) {
            _pointsInTime.RemoveAt(_pointsInTime.Count - 1);
        }

        _pointsInTime.Insert(0, new PointInTime(transform.position));
    }

    private void StartRewind() {
        _isRewinding = true;
        _rigidbody2D.isKinematic = true;
    }

    private void StopRewind() {
        _isRewinding = false;
        _rigidbody2D.isKinematic = false;
    }
}