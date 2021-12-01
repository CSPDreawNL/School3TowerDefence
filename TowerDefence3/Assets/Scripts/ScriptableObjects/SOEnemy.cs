using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy")]
public class SOEnemy : ScriptableObject {
    public string enemyName;
    public GameObject enemyPrefab;

    [Header("Info")]
    public int maxHealth;
    public int damage;
    public float movementSpeed;
    public int coins;
}
