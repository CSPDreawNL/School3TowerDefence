using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD3.Core
{
    public class TowerShoot : MonoBehaviour
    {
        [SerializeField] float weaponRange;
        [SerializeField] float timeBetweenAttacks;
        [SerializeField] GameObject projectile;

        [SerializeField] Health target;
        float timeSinceLastAttack = 0;

        private void Awake()
        {
            StartCoroutine(Shoot());
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.GetComponent<Health>())
            {
                target = other.GetComponent<Health>();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<Health>() == target)
            {
                target = null;
            }
        }

        IEnumerator Shoot()
        {
            while (gameObject.activeSelf)
            {
                yield return new WaitForSeconds(1);
                while (target == true && timeSinceLastAttack >= timeBetweenAttacks)
                {
                    Instantiate(projectile, transform.position, transform.rotation);
                    timeSinceLastAttack = 0f;
                    yield return new WaitForSeconds(1);
                }
            }
        }
    }
}