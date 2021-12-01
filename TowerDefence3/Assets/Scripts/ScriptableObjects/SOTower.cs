using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower", menuName = "ScriptableObjects/Tower")]
public class SOTower : ScriptableObject {
    public string towerName;
    public GameObject towerPrefab;
    public SOBullet bullet;

    [Header("Info")]
    public float towerAttackSpeed;
    public int towerPrice;
}
