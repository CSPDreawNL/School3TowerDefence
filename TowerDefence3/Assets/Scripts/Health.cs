using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TD3.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] public float currentHealthPoints;
        [SerializeField] TextMeshProUGUI healthUI;

        private void Start()
        {
            UpdateHealth();
        }

        bool isDead = false;

        public void TakeDamage(float damage)
        {
            currentHealthPoints = Mathf.Max(currentHealthPoints - damage, 0f);

            if (currentHealthPoints == 0f && isDead == false)
            {
                PlayerManager.instance.UpdateCoins(5);
                EventManager.instance.EnemyDied();
                Destroy(gameObject);
            }

            UpdateHealth();
        }

        private void UpdateHealth()
        {
            if (healthUI)
            {
                healthUI.text = currentHealthPoints.ToString();
            }
        }
    }
}
