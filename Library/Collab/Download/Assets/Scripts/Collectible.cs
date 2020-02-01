using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {
    private enum CollectibleTypes { COIN, DIAMOND, STOPWATCH }

    [Header("Collectible Preferences")]
    [SerializeField]
    private CollectibleTypes _collectibleType = CollectibleTypes.COIN;

    private void OnTriggerEnter2D(Collider2D _other) {
        if (_other.gameObject.CompareTag("Player")) {
            MainPlayer _playerReference = GameObject.Find("Player").GetComponent<MainPlayer>();
            if (_playerReference != null) {
                _playerReference.CollectedCoin();
                Destroy(this.gameObject);
            } else {
                Debug.LogError("Coin::OnTriggerEnter2D(): Could not find the player.");
            }
        }
    }
}