using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "ScriptableObjects/Bullet")]
public class SOBullet : ScriptableObject
{
    public string bulletType;
    public GameObject bulletPrefab;

    [Header("Info")]
    public Sprite bulletSprite;
    public int bulletDamage;
}
