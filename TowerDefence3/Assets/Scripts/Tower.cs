using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD3.Core {
    public class Tower : MonoBehaviour {
        [SerializeField] private SOTower m_TowerSettings;

        [SerializeField] float timeBetweenAttacks;
        [SerializeField] GameObject projectile;

        [SerializeField] Health target;
        float timeSinceLastAttack = 0;

        private Coroutine shootingCoroutine;

        private void Awake() {
            shootingCoroutine = StartCoroutine(Shoot());
        }

        private void Update() {
            timeSinceLastAttack += Time.deltaTime;
        }

        private void OnTriggerStay(Collider other) {
            if (other.GetComponent<Health>()) {
                target = other.GetComponent<Health>();
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.GetComponent<Health>() == target) {
                target = null;
            }
        }

        IEnumerator Shoot() {
            while (gameObject.activeSelf) {
                while (target == true && timeSinceLastAttack >= timeBetweenAttacks) {
                    GameObject bullet = Instantiate(projectile, transform.position, transform.rotation);

                    projectile.GetComponent<Bullet>().Target = target.transform;
                    timeSinceLastAttack = 0f;
                }
            }
            yield return null;
        }

        public void InstantiateSettings(SOTower _settings) {
            m_TowerSettings = _settings;
            timeBetweenAttacks = m_TowerSettings.towerAttackSpeed;
        }
    }
}