using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private static GameManager _instance = null;

    public static GameManager Instance { get => _instance; }

    private void Awake() {
        if (_instance != this || _instance != null)
            Destroy(this.gameObject);
        else
            _instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    public void SlowDownTime() {
        // for calling with slowmotion collectible item
        Time.timeScale = .7F;

        // TODO: Store fixedDeltaTime.
        //Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
    }

}