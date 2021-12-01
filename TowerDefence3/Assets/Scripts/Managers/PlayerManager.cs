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

    [SerializeField] private int health = 5;
    [SerializeField] public int coins = 20;

    private void Start() {
        UIManager.instance.UpdateHealthUI(health);
        UIManager.instance.UpdateCoinsUI(coins);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<Health>()) {
            //TODO: use enemy dmg
            health--;
            UIManager.instance.UpdateHealthUI(health);
            Destroy(other.gameObject);

            if (health <= 0) {
                UIManager.instance.GameOver();
            }
        }
    }

    public bool UpdateCoins(int _coins) {
        bool _return = false;

        if (_coins > 0) {
            coins += _coins;
            _return  = true;
        }
        else if (_coins < 0) {
            if (coins + _coins < 0)
                _return = false;
            else {
                coins += _coins;
                _return = true;
            }
        }
        UIManager.instance.UpdateCoinsUI(coins);
        return _return;
    }
}
