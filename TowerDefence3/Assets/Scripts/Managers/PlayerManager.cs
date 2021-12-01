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

    public int PlayerHealth = 1;
    public int PlayerCoins = 50;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<Health>()) {
            PlayerHealth--;
            UIManager.instance.UpdateHealthUI();

            if (PlayerHealth <= 0) {
                UIManager.instance.GameOver();
            }
        }
    }

    public bool UpdateCoins(int _coins) {
        bool _return = false;

        if (_coins > 0) {
            PlayerCoins += _coins;
            _return  = true;
        }
        else if (_coins < 0) {
            if (PlayerCoins + _coins < 0)
                _return = false;
            else {
                PlayerCoins += _coins;
                _return = true;
            }
        }
        UIManager.instance.UpdateCoinsUI();
        return _return;
    }
}
