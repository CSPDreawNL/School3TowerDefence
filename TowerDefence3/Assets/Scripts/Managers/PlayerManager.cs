using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TD3.Core;

public class PlayerManager : MonoBehaviour {

    public static PlayerManager instance;

    private void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public int playerHealth = 1;
    public int playerCoins = 50;

    private void Start() {
        UIManager.instance.UpdateHealthUI(playerHealth);
        UIManager.instance.UpdateCoinsUI(playerCoins);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<Health>()) {
            playerHealth--;
            UIManager.instance.UpdateHealthUI(playerHealth);

            if (playerHealth <= 0) {
                UIManager.instance.GameOver();
            }
        }
    }

    public bool UpdateCoins(int _coins) {
        bool _return = false;

        if (_coins > 0) {
            playerCoins += _coins;
            _return  = true;
        }
        else if (_coins < 0) {
            if (playerCoins + _coins < 0)
                _return = false;
            else {
                playerCoins += _coins;
                _return = true;
            }
        }
        UIManager.instance.UpdateCoinsUI(playerCoins);
        return _return;
    }
}
