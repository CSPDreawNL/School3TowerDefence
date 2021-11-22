using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilder : MonoBehaviour {

    [SerializeField] private Vector3 m_MouseInWorldPosition;
    [SerializeField] private SOTower m_CurrentTower;


    void Update() {
        m_MouseInWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Instantiate(m_CurrentTower.towerPrefab, m_MouseInWorldPosition, Quaternion.identity);
        }
    }
}
