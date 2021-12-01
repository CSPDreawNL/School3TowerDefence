using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    public static WaveManager instance;

    private void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    [SerializeField] private GameObject m_Enemy;
    [SerializeField] private Transform m_EnemyList;
    [SerializeField] private Transform m_SpawnPosition;
    [SerializeField] private int[] m_Waves;
    [SerializeField] private int m_SpawnInterval = 1;
    [SerializeField] private int m_WaveInterval = 5;

    private Coroutine waveSpawner;
    private int waveCounter = 0;
    private int enemyCounter = 0;

    private void Start() {
        waveSpawner = StartCoroutine(WaveSpawner());
    }

    IEnumerator WaveSpawner() {
        while (waveCounter != m_Waves.Length) {
            yield return new WaitForSeconds(m_WaveInterval);
            for (int i = 0; i < m_Waves[waveCounter]; i++) {
                GameObject enemy = Instantiate(m_Enemy, m_SpawnPosition.position, Quaternion.identity, m_EnemyList);
                enemy.SetActive(true);
                enemyCounter++;

                yield return new WaitForSeconds(m_SpawnInterval);
            }
            waveCounter++;
        }
        StopCoroutine(waveSpawner);
    }

    public void EnemyDied() {
        enemyCounter--;
        if (enemyCounter == 0 && waveCounter == m_Waves.Length) {
            StopCoroutine(waveSpawner);
            UIManager.instance.YouWon();
        }
    }
}
