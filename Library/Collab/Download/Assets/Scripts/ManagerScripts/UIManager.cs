using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public static UIManager Instance { get; private set; } = null;

    [Header("UI Preferences")]
    [SerializeField]
    private Text _playerLivesText = null;

    private void Awake() {
        if (Instance == null)
            Instance = this;
        else if(Instance != this)
            Destroy(this);

        //DontDestroyOnLoad(this.gameObject);
    }

    private void Start() {
        if (_playerLivesText == null)
            Debug.LogError("UIManager::Start(): Text not set.");
    }

    /*
     * Random Thoughts:
     * Creating 3 Hearts in a container object?
     * 
     */
    public void UpdatePlayerLives(int _playerLives) {
        //_playerLivesText.text = "Lives: " + _playerLives;
        
        // Animated Sprite
        _playerLivesText.text = "" + _playerLives;

        // Debug.Log("UIManager::UpdatePlayerLives(): Maybe checking for player lives / death.");
    }
}