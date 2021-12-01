using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TD3.Core;

public class TowerBuilder : MonoBehaviour {
    public static TowerBuilder instance;

    private void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    [SerializeField] private SOTower m_CurrentTower;

    [Header("Info")]
    [SerializeField] private KeyCode m_PlaceTowerKey = KeyCode.Mouse0;
    [SerializeField] private LayerMask m_RaycastMask;
    [SerializeField] private Transform m_TowerList;
    [SerializeField] private Vector3 m_TowerSpawnOffset = new Vector3(0f, 1f, 0f);


    void Update() {
        if (Input.GetKeyDown(m_PlaceTowerKey)) {
            PlaceTower();
        }
    }

    private void PlaceTower() {
        RaycastHit hit;
        bool hasHit = Physics.Raycast(GetMouseRay(), out hit, Mathf.Infinity, m_RaycastMask, QueryTriggerInteraction.Ignore);

        //Has hit the surface of an object and has anough money to buy a tower.
        if (hasHit) {
            if (hit.collider.tag == "Placable" && PlayerManager.instance.UpdateCoins(-10)) {
                GameObject tower = Instantiate(m_CurrentTower.towerPrefab, hit.point += m_TowerSpawnOffset, Quaternion.identity, m_TowerList);
                tower.GetComponent<Tower>().InstantiateSettings(m_CurrentTower);
            }
        }
    }

    private static Ray GetMouseRay() {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }

    public void SelectTower(SOTower _SO) {
        m_CurrentTower = _SO;
    }
}
