using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    public static EventManager instance;

    private void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    [SerializeField] private GameObject m_Enemy;
    [SerializeField] private Transform m_EnemyList;
    [SerializeField] private Transform m_EnemySpawnPosition;
    [SerializeField] private GameObject m_Lazer;
    [SerializeField] private Transform m_LazerSpawnPosition;
    [SerializeField] private int[] m_Waves;
    [SerializeField] private int m_SpawnInterval = 1;
    [SerializeField] private int m_WaveInterval = 5;
    [SerializeField] private Vector2 m_EventTimes = new Vector2(60, 120);

    private Coroutine waveSpawner;
    private Coroutine eventSpawner;
    private GameObject lazerTarget;
    private int waveCounter = 0;
    private int enemyCounter = 0;

    private void Start() {
        UIManager.instance.UpdateWaveUI(waveCounter + 1);

        waveSpawner = StartCoroutine(WaveSpawner());
        eventSpawner = StartCoroutine(EventSpawner());
    }

    IEnumerator WaveSpawner() {
        while (waveCounter != m_Waves.Length) {
            yield return new WaitForSeconds(m_WaveInterval);

            for (int i = 0; i < m_Waves[waveCounter]; i++) {
                GameObject enemy = Instantiate(m_Enemy, m_EnemySpawnPosition.position, Quaternion.identity, m_EnemyList);
                enemy.SetActive(true);
                enemyCounter++;

                yield return new WaitForSeconds(m_SpawnInterval);
            }
            waveCounter++;
            UIManager.instance.UpdateWaveUI(waveCounter + 1);
        }
        StopCoroutine(waveSpawner);
    }

    IEnumerator EventSpawner() {
        while (waveCounter != m_Waves.Length) {
            int randomTime = (int)Random.Range(m_EventTimes.x, m_EventTimes.y);

            yield return new WaitForSeconds(randomTime);

            GameObject[] allTowers = GameObject.FindGameObjectsWithTag("Tower");
            int randomTower = Random.Range(0, allTowers.Length);

            for (int i = 0; i < allTowers.Length; i++) {
                lazerTarget = allTowers[randomTower].gameObject;
            }
        }
        StopCoroutine(eventSpawner);
    }

    public void EnemyDied() {
        enemyCounter--;
        if (enemyCounter == 0 && waveCounter == m_Waves.Length) {
            StopCoroutine(waveSpawner);
            UIManager.instance.YouWon();
        }
    }
}
