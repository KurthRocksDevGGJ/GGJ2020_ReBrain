using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugManager : MonoBehaviour {
    private static DebugManager _instance = null;

    public static DebugManager Instance { get => _instance; }

    [Header("Debug UI Elements")]
    [SerializeField]
    private bool _showDebug;
    [SerializeField]
    private GameObject _debugPanel;
    [SerializeField]
    private Text _playerPositionText;
    [SerializeField]
    private Text _currentDebugTestText;

    private void Awake() {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this.gameObject);

        // DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha9)) {
            _showDebug = !_showDebug;
            _debugPanel.gameObject.SetActive(_showDebug);
        }
    }

    public void SetPlayerPosition(Vector3 _playerPosition) {
        if (_showDebug)
            _playerPositionText.text = "Position: " + _playerPosition.ToString();
    }

    public void SetCurrentDebugTestText(string _text) {
        if (_showDebug)
            _currentDebugTestText.text = "DEBUG: " + _text;
    }
}