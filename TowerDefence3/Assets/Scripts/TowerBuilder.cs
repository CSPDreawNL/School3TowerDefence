using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilder : MonoBehaviour {
    [SerializeField] private SOTower m_CurrentTower;

    [Header("Info")]
    [SerializeField] private KeyCode m_PlaceTowerKey = KeyCode.Mouse0;
    [SerializeField] private Vector3 m_TowerSpawnOffset = new Vector3(0f, 1f, 0f);


    void Update() {
        if (Input.GetKeyDown(m_PlaceTowerKey)) {
            PlaceTower();
        }
    }

    private void PlaceTower() {
        RaycastHit hit;
        bool hasHit = Physics.Raycast(GetMouseRay(), out hit, Mathf.Infinity);

        //Has hit the surface of an object.
        if (hasHit) {
            if (hit.collider.tag == "Placable") {
                Instantiate(m_CurrentTower.towerPrefab, hit.point += m_TowerSpawnOffset, Quaternion.identity);
                Debug.Log("HasHit");
            }
        }
    }

    private static Ray GetMouseRay() {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}
