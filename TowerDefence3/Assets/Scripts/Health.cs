using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TD3.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] public float maxHealthPoints;
        [SerializeField] public float currentHealthPoints;

        bool isDead = false;

        public void TakeDamage(float damage)
        {
            currentHealthPoints = Mathf.Max(currentHealthPoints - damage, 0f);

            if (currentHealthPoints == 0f && isDead == false)
            {
                Destroy(gameObject);
            }
        }
    }
}
