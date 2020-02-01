using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public static UIManager Instance { get; private set; } = null;

    [Header("UI Preferences")]
    [SerializeField]
    private Text _playerLivesText = null;
    [SerializeField]
    private GameObject _gameOvahPanel = null;
    // Move to GameManager??
    [SerializeField]
    private bool _isGameOver = false;

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
        if (_gameOvahPanel == null)
            Debug.LogError("UIManager::Start(): GameOver panel not set.");
    }

    private void Update() {
        if(_isGameOver) {
            if(Input.GetKeyDown(KeyCode.R)) {
                Debug.Log("Reload current scene");
                HideGameOvahPanel();

                // TODO: Move to GameManager!?
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
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

    public void ShowGameOvahPanel() {
        _isGameOver = true;
        _gameOvahPanel.gameObject.SetActive(true);
        Time.timeScale = 0F;
    }

    public void HideGameOvahPanel() {
        _isGameOver = false;
        _gameOvahPanel.gameObject.SetActive(false);
        Time.timeScale = 1F;
    }
}