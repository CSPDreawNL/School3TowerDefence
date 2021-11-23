using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD3.Core
{

    public class Bullet : MonoBehaviour
    {
        public Transform Target;
        public float Speed;

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
            if (other.CompareTag("Enemy"))
            {
                Health _enemyHealth = other.GetComponent<Health>();
                if (_enemyHealth)
                {
                    _enemyHealth.TakeDamage(1);
                }
                Destroy(gameObject);
            }
        }
    }

}