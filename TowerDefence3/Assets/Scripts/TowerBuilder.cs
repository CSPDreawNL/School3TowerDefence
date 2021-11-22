using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilder : MonoBehaviour {
    [SerializeField] private SOTower m_CurrentTower;

    [Header("Info")]
    [SerializeField] private KeyCode m_PlaceTowerKey = KeyCode.Mouse0;
    [SerializeField] private LayerMask m_RaycastLayerMask;


    void Update() {
        if (Input.GetKeyDown(m_PlaceTowerKey)) {
            PlaceTower();
        }
    }

    private void PlaceTower() {

        RaycastHit hit;
        bool hasHit = Physics.Raycast(GetMouseRay(), out hit, float.PositiveInfinity , m_RaycastLayerMask.value);

        //Has hit the surface of an object.
        if (hasHit) {
            Instantiate(m_CurrentTower.towerPrefab, hit.point, Quaternion.identity);
        }
    }

    private static Ray GetMouseRay() {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}
