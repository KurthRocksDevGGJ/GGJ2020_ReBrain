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
    private Text _hammerText = null;
    [SerializeField]
    private Text _travelDistanceText = null;
    [SerializeField]
    private Text _recordHighscoreText = null;
    [SerializeField]
    private Text _recordCoinsText = null;
    [SerializeField]
    private GameObject _gameOvahPanel = null;
    [SerializeField]
    private GameObject _pauseMenuPanel = null;
    // Move to GameManager??
    [SerializeField]
    private bool _isGameOver = false;
    [SerializeField]
    private bool _isPauseMenu = false;

    private float _scoreDistance = 0F;
    private int _scoreCoins = 0;

    private void Awake() {
        if (Instance == null)
            Instance = this;
        else if(Instance != this)
            Destroy(this);

        //DontDestroyOnLoad(this.gameObject);
    }

    private void Start() {
        if (_playerLivesText == null)
            Debug.LogWarning("UIManager::Start(): Lives text element not set.");
        if (_playerCoinsText == null)
            Debug.LogWarning("UIManager::Start(): Coins text element not set.");
        /*
        if (_hammerText == null)
            Debug.LogWarning("UIManager::Start(): Hammer text element not set.");
        */
        if (_travelDistanceText == null)
            Debug.LogWarning("UIManager::Start(): Traveling text element not set.");
        if (_recordHighscoreText == null)
            Debug.LogWarning("UIManager::Start(): Record score text element not set.");
        if (_recordCoinsText == null)
            Debug.LogWarning("UIManager::Start(): Record coins text element not set.");

        if (_gameOvahPanel == null)
            Debug.LogWarning("UIManager::Start(): GameOver panel not set.");

        _scoreDistance = PlayerPrefs.GetInt("Record_Distance", 0);
        UpdateRecordDistance(_scoreDistance);

        _scoreCoins = PlayerPrefs.GetInt("Record_Coins", 0);
        UpdateRecordCoins(_scoreCoins);
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

        if (_coins > _scoreCoins) {
            PlayerPrefs.SetInt("Record_Coins", _coins);
            UpdateRecordCoins(_coins);
            _scoreCoins = _coins;
        } else {
            // Debug.Log("" + _coins + " - " + _scoreCoins);
        }
    }
    private void UpdateRecordCoins(int _coins) {
        _recordCoinsText.text = _coins.ToString() + " coins";
    }

    /*
    public void UpdateHammerUsage(int _hammerUsage) {
        _hammerText.text = _hammerUsage.ToString();
    }
    */

    public void UpdateTravelDistance(int _travelDistance) {
        _travelDistanceText.text = _travelDistance.ToString() + " m";

        if (_travelDistance > _scoreDistance) {
            PlayerPrefs.SetInt("Record_Distance", _travelDistance);
            UpdateRecordDistance(_travelDistance);
            _scoreDistance = _travelDistance;
        } else {
            // Debug.Log("" + _travelDistance + " - " + _scoreDistance);
        }
    }
    public void UpdateRecordDistance(float _recordDistance) {
        _recordHighscoreText.text = _recordDistance.ToString() + " m";
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