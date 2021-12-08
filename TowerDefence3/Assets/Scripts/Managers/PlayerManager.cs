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

    [Header("Player Info")]
    [SerializeField] private int health = 5;
    [SerializeField] public int coins = 20;

    [Header("Tower")]
    [SerializeField] private SOTower m_CurrentTower;
    [SerializeField] private Transform m_TowerList;
    [SerializeField] private Vector3 m_TowerSpawnOffset = new Vector3(0f, 1f, 0f);

    [Header("Raycast")]
    [SerializeField] private KeyCode m_InteractKey = KeyCode.Mouse0;
    [SerializeField] private LayerMask m_RaycastMask;

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

    void Update() {
        if (Input.GetKeyDown(m_InteractKey)) {
            RayCastToMouse();
        }
    }

    private void RayCastToMouse() {
        RaycastHit hit;
        bool hasHit = Physics.Raycast(GetMouseRay(), out hit, Mathf.Infinity, m_RaycastMask, QueryTriggerInteraction.Ignore);

        //Has hit the surface of an object and has enough money to buy a tower.
        if (hasHit) {
            if (hit.collider.tag == "Placable" && instance.UpdateCoins(-m_CurrentTower.towerPrice)) {
                GameObject tower = Instantiate(m_CurrentTower.towerPrefab, hit.point += m_TowerSpawnOffset, Quaternion.identity, m_TowerList);
                tower.GetComponent<Tower>().InstantiateSettings(m_CurrentTower);
            }
            else if (hit.collider.tag == "Lazer") {
                Destroy(hit.collider.gameObject);
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

    private static Ray GetMouseRay() {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }

    public void SelectTower(SOTower _SO) {
        m_CurrentTower = _SO;
    }
}
