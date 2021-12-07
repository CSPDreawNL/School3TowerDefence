using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD3.Core
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private SOBullet m_BulletSettings;

        public Transform Target;
        public float Speed = 25;
        public float Damage;

        private void FixedUpdate()
        {
            if (Target)
            {
                Vector3 _dir = Target.position - transform.position;
                GetComponent<Rigidbody>().velocity = _dir.normalized * Speed;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>())
            {
                Health _enemyHealth = other.GetComponent<Health>();
                if (_enemyHealth)
                {
                    _enemyHealth.TakeDamage(Damage);
                }
                Destroy(gameObject);
            }
        }

        public void InstantiateSettings(SOBullet _settings) {
            m_BulletSettings = _settings;
            Damage = m_BulletSettings.bulletDamage;
        }
    }
}