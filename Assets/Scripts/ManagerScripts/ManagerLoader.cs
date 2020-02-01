using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerLoader : MonoBehaviour {
    [Header("Manager Preferences")]
    [SerializeField]
    private GameManager _gameManager = null;
    [SerializeField]
    private AudioManager _audioManager = null;
    [SerializeField]
    private UIManager _uiManager = null;

    // Start is called before the first frame update
    void Start() {
        if (_uiManager == null)
            Debug.Log("ManagerLoader::Start(): UiManager instance is null");
        if (_gameManager == null)
            Debug.Log("ManagerLoader::Start(): GameManager instance is null");
        if (_audioManager == null)
            Debug.Log("ManagerLoader::Start(): AudioManager instance is null");
    }

    // Update is called once per frame
    void Update() {

    }
}