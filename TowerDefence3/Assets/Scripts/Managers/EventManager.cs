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

    [SerializeField] private Waves m_Waves;

    [Header("Laser")]
    [SerializeField] private GameObject m_LaserPrefab;
    [SerializeField] private Transform m_LaserSpawnPosition;
    [SerializeField] private Vector2 m_EventTimes;

    private Coroutine waveSpawner;
    private Coroutine eventSpawner;

    public int waveCounter = 0;
    public int enemyCounter = 0;

    private void Start() {
        waveCounter++;
        UIManager.instance.UpdateWaveUI(waveCounter, m_Waves.wavePatern.Length);

        waveSpawner = StartCoroutine(WaveSpawner());
        eventSpawner = StartCoroutine(EventSpawner());
    }

    IEnumerator WaveSpawner() {
        //Waits a amount of second until the waves start spawing.
        yield return new WaitForSeconds(m_Waves.startInterval);
        while (IsGameRunning()) {

            //Spawns the number of enemies decided my m_Waves.wavePatern.
            for (int i = 0; i < m_Waves.wavePatern[waveCounter-1]; i++) {
                //The enemy to spawn
                GameObject enemyToSpawn = null;

                //Decides what enemy to spawn, and overwrites it if a higher enemy can spawn.
                foreach (EnemyStats enemyStats in m_Waves.enemiesInWaves) {
                    if (waveCounter >= enemyStats.startWave) {
                        if (Random.Range(0f, 100f) <= enemyStats.spawnChance)
                            enemyToSpawn = enemyStats.enemyPrefab;
                    }
                }

                //Spawn the enemy, add it to the parent, make it visable and add 1 to the enemyCounter.
                if (enemyToSpawn != null) {
                    GameObject enemy = Instantiate(enemyToSpawn, m_Waves.enemySpawnPosition.position, Quaternion.identity, m_Waves.enemyParent);
                    enemy.SetActive(true);
                    enemyCounter++;
                }
                else
                    Debug.LogError("!ERROR!: Something went wrong while trying to spawn an enemy.");

                //Wait a certain amount of time before spawning another enemy.
                yield return new WaitForSeconds(m_Waves.spawnInterval);
            }

            //Wait until all enemies are dead, then add one to the wave counter and update the UI.
            yield return new WaitUntil(() => enemyCounter == 0);
            if (waveCounter == m_Waves.wavePatern.Length) {
                UIManager.instance.YouWon();
                StopCoroutine(waveSpawner);
            }
            yield return new WaitForSeconds(m_Waves.waveInterval);
            waveCounter++;
            UIManager.instance.UpdateWaveUI(waveCounter, m_Waves.wavePatern.Length);
        }
    }

    IEnumerator EventSpawner() {
        while (IsGameRunning()) {
            //Wait a random amount of time before continuing.
            int randomTime = (int)Random.Range(m_EventTimes.x, m_EventTimes.y);
            yield return new WaitForSeconds(randomTime);

            //Get all towers in the scene and put them in a array.
            GameObject[] allTowers = GameObject.FindGameObjectsWithTag("Tower");

            //Set a random target.
            int randomTower = Random.Range(0, allTowers.Length);
            GameObject laserTarget = null;
            for (int i = 0; i < allTowers.Length; i++) {
                laserTarget = allTowers[randomTower].gameObject;
            }

            //Spawn the laser ship and set the target.
            if (laserTarget != null) {
                GameObject laser = Instantiate(m_LaserPrefab, m_LaserSpawnPosition.position, m_LaserSpawnPosition.rotation);
                laser.GetComponent<Steering.SimpleBrain>().followPathPoints = new List<GameObject>() { laserTarget };
                laser.GetComponent<LaserEvent>().InstantiateLaser(laserTarget);
            }
        }
    }

    /// <summary>
    /// Check if the game is running and there are currently waves spawning.
    /// </summary>
    /// <returns>Returns true if all conditions are met</returns>
    private bool IsGameRunning() {
        return (Application.isPlaying && waveCounter <= m_Waves.wavePatern.Length) ? true : false;
    }

    /// <summary>
    /// Update the enemyCounter.
    /// </summary>
    public void EnemyDied() {
        enemyCounter--;
    }

    private void OnApplicationQuit() {
        StopCoroutine(eventSpawner);
        StopCoroutine(waveSpawner);
    }

    [System.Serializable]
    public struct Waves {
        [Header("Spawning")]
        [Tooltip("The patern the units will spawn in, array length is total amount of waves")]
        public int[] wavePatern;
        [Tooltip("Time before the waves start spawing")]
        public int startInterval;
        [Tooltip("Time before wave starts.")]
        public int waveInterval;
        [Tooltip("Time between units spawned.")]
        public int spawnInterval;

        [Header("Enemies")]
        [Tooltip("The parent object in the hierarchy of the units.")]
        public Transform enemyParent;
        [Tooltip("The position of the spawned units.")]
        public Transform enemySpawnPosition;
        [Tooltip("Data about the spawned units.")]
        public List<EnemyStats> enemiesInWaves;
    }

    [System.Serializable]
    public struct EnemyStats {
        /// <summary>
        /// The starts of this enemy unit.
        /// </summary>
        /// <param name="_enemy">The prefab this unit.</param>
        /// <param name="_startWave">The wave this unit start spawing.</param>
        /// <param name="_spawnChance">The chance this unit spawn, for 0 out of 100.</param>
        public EnemyStats(GameObject _enemy, int _startWave, int _spawnChance) {
            enemyPrefab = _enemy;
            startWave = _startWave;
            spawnChance = _spawnChance;
        }

        [Tooltip("The prefab this unit.")]
        public GameObject enemyPrefab;
        [Tooltip("The wave this unit start spawing.")]
        public int startWave;
        [Tooltip("The chance this unit spawn, for 0 out of 100.")]
        [Range(0, 100)]
        public int spawnChance;
    }
}
