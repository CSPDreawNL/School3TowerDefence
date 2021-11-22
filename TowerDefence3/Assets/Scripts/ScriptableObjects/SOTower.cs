using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower", menuName = "ScriptableObjects/Tower")]
public class SOTower : ScriptableObject {
    public string towerName;
    public GameObject towerPrefab;


    [Header("Info")]
    public float damage;
    public float fireRate;
    public float reloadSpeed;
    public int price;
}
