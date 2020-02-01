using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    [Header("Audio Preferences")]
    [SerializeField]
    private AudioSource _audioSource = null;

    // Start is called before the first frame update
    void Start() {
        if (_audioSource == null)
            Debug.LogError("AudioManager::Start(): No audio source found.");
    }
}