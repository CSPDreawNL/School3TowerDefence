using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD3.Core
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private SOTower m_TowerSettings;

        [SerializeField] float timeBetweenAttacks;
        [SerializeField] GameObject projectile;

        [SerializeField] Health target;
        [SerializeField] float timeSinceLastAttack = 0;

        private void Start()
        {
            projectile = m_TowerSettings.bullet.bulletPrefab;
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target && timeSinceLastAttack >= timeBetweenAttacks)
            {
                GameObject bullet = Instantiate(projectile, transform.position, transform.rotation);
                bullet.GetComponent<Bullet>().InstantiateSettings(m_TowerSettings.bullet);
                projectile.GetComponent<Bullet>().Target = target.transform;
                timeSinceLastAttack = 0f;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.GetComponent<Health>())
            {
                target = other.GetComponent<Health>();
                transform.LookAt(target.transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<Health>() == target)
            {
                target = null;
            }
        }
        public void InstantiateSettings(SOTower _settings)
        {
            m_TowerSettings = _settings;
            timeBetweenAttacks = m_TowerSettings.towerAttackSpeed;
        }
    }
}