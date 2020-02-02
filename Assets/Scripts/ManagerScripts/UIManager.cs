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
    private Text _playerCoinsText = null;
    [SerializeField]
    private GameObject _gameOvahPanel = null;
    [SerializeField]
    private GameObject _pauseMenuPanel = null;
    // Move to GameManager??
    [SerializeField]
    private bool _isGameOver = false;
    [SerializeField]
    private bool _isPauseMenu = false;

    private void Awake() {
        if (Instance == null)
            Instance = this;
        else if(Instance != this)
            Destroy(this);

        //DontDestroyOnLoad(this.gameObject);
    }

    private void Start() {
        if (_playerLivesText == null)
            Debug.LogError("UIManager::Start(): Lives text element not set.");
        if (_playerCoinsText == null)
            Debug.LogError("UIManager::Start(): Coins text element not set.");
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
        } else {
            if (_isPauseMenu) {
                if (Input.GetKeyDown(KeyCode.P)) {
                    HidePausePanel();
                    _isPauseMenu = false;
                }
            } else {
                if (Input.GetKeyDown(KeyCode.P)) {
                    ShowPausePanel();
                    _isPauseMenu = true;
                }
            }

        }
    }

    void ShowPausePanel() {
        _gameOvahPanel.gameObject.SetActive(false);
        _pauseMenuPanel.gameObject.SetActive(true);
        Time.timeScale = 0F;
    }

    void HidePausePanel() {
        _pauseMenuPanel.gameObject.SetActive(false);
        Time.timeScale = 1F;
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

    // TODO: Animated Coins Sprite
    public void UpdatePlayerCoins(int _coins) {
        _playerCoinsText.text = "" + _coins.ToString();
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

    // TODO: GameManager...
    public void LoadScene(string _sceneName) {
        SceneManager.LoadScene(_sceneName);
    }
}