using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UIHelper_LevelSelection : MonoBehaviour {
    [Header("UI Elements")]
    [SerializeField]
    private GameObject _scrollViewObject = null;
    [SerializeField]
    private GameObject _viewportContent = null;
    [SerializeField]
    private GameObject _gridContainer = null;
    [SerializeField]
    private Button _prefabButton = null;

    [Header("Debug Info")]
    [SerializeField]
    private string _filePath = "";
    [SerializeField]
    private string[] _unityLevelFullNames = null;
    [SerializeField]
    private string[] _unityLevelNames = null;

    // Start is called before the first frame update
    void Start() {
        if (_prefabButton == null)
            Debug.Log("Prefab Button is null.");

        LoadUnityLevelInfo();
        ProceduralButtonGenerator();
    }

    private void LoadUnityLevelInfo() {
        // directories have a directory separator system depending!!
        char pathDelimiter = Path.DirectorySeparatorChar;

        // Scenes Folder
        _filePath = Application.dataPath + pathDelimiter + "Scenes";

        // These are the fullNames
        _unityLevelFullNames = Directory.GetFiles(_filePath, "Level*.unity");

        // Retrieving filenames from file info. Kinda long, maybe searching for replacement later.
        FileInfo[] fileInfo = new DirectoryInfo(_filePath).GetFiles("Level*.unity");
        _unityLevelNames = new string[fileInfo.Length];

        // saving the file names in an array
        for (int i = 0; i < fileInfo.Length; i++) {
            _unityLevelNames[i] = (fileInfo[i].Name.Replace(".unity", ""));
        }
    }

    private void ProceduralButtonGenerator() {
        _prefabButton.gameObject.SetActive(false);

        RectTransform _buttonRectTrans = _prefabButton.GetComponent<RectTransform>();
        Vector3 _btnPosition = _buttonRectTrans.position;
        Rect _btnDimension = _buttonRectTrans.rect;

        // ScrollViewer Content Size
        RectTransform _scrollViewRect = _scrollViewObject.GetComponent<RectTransform>();


        for (int i = 0; i < _unityLevelNames.Length; i++) {
            Button button = (Button)Instantiate(_prefabButton);
            button.gameObject.SetActive(true);
            button.transform.SetParent(_gridContainer.transform);

            button.onClick.AddListener(() => OnUIButtonClick(button));

            button.transform.GetChild(0).GetComponent<Text>().text = _unityLevelNames[i];

            button.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            //button.GetComponent<RectTransform>().position = new Vector3(_btnPosition.x + (i * (_btnDimension.width + 60)), _btnPosition.y, _btnPosition.z);
            button.GetComponent<RectTransform>().sizeDelta = new Vector2(_btnDimension.width, _btnDimension.height);

            // Button colors...
            button.GetComponent<Image>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 0f, 1f, 1F, 1F);
            Color color = button.GetComponent<Image>().color;
            button.transform.GetChild(0).GetComponent<Text>().color = new Color(1.0F - color.r, 1.0F - color.g, 1.0F - color.b);
        }

        /*
        Debug.Log(_scrollViewRect.rect.width + " / (30 + " + _btnDimension.width + ") = " + (_scrollViewRect.rect.width / (30 + _btnDimension.width)));
        for (int i = 0; i < _unityLevelNames.Length; i++) {
            Button button = (Button)Instantiate(_prefabButton);
            button.gameObject.SetActive(true);

            button.transform.SetParent(_viewportContent.transform);

            //button.GetComponent<Button>().onClick.AddListener(delegate { OnUIButtonClick(button); });
            button.onClick.AddListener(() => OnUIButtonClick(button));

            button.transform.GetChild(0).GetComponent<Text>().text = _unityLevelNames[i];

            button.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            button.GetComponent<RectTransform>().position = new Vector3(_btnPosition.x + (i * (_btnDimension.width + 60)), _btnPosition.y, _btnPosition.z);
            button.GetComponent<RectTransform>().sizeDelta = new Vector2(_btnDimension.width, _btnDimension.height);

            // Button colors...
            button.GetComponent<Image>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 0f, 1f, 1F, 1F);
            Color color = button.GetComponent<Image>().color;
            button.transform.GetChild(0).GetComponent<Text>().color = new Color(1.0F - color.r, 1.0F - color.g, 1.0F - color.b);

            // values for other lines...
            float a = _btnPosition.x + (i * (_btnDimension.width + 60));
            Debug.Log("" + a + " # " + _scrollViewRect.rect);
            Debug.Log("" + _btnDimension);
        }
        */
    }

    private void OnUIButtonClick(Button button) {
        string levelName = button.transform.GetChild(0).GetComponent<Text>().text;
        if(UIManager.Instance)
            UIManager.Instance.LoadScene(levelName);
    }
}













