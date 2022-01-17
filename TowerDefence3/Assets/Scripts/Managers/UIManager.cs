using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public static UIManager instance;

    private void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    [SerializeField] private KeyCode m_PauseKey = KeyCode.Escape;

    [Header("Panels")]
    [SerializeField] private GameObject m_GameOverPanel;
    [SerializeField] private GameObject m_YouWonPanel;
    [SerializeField] private GameObject m_PausePanel;

    [Header("TMP")]
    [SerializeField] private TMPro.TextMeshProUGUI m_WaveTMP;
    [SerializeField] private TMPro.TextMeshProUGUI m_HealthTMP;
    [SerializeField] private TMPro.TextMeshProUGUI m_CoinsTMP;

    [Header("TowerSelect")]
    [SerializeField] private Image m_SelectedTowerPanel;
    [SerializeField] private Color m_SelectedTowerColor;
    [SerializeField] private Color m_UnselectedTowerColor;

    public bool isGamePaused = false;

    private void Start() {
        m_GameOverPanel.SetActive(false);
        m_YouWonPanel.SetActive(false);
        m_PausePanel.SetActive(false);
        m_SelectedTowerPanel.color = m_SelectedTowerColor;
    }

    private void Update() {
        if (Input.GetKeyDown(m_PauseKey)) {
            PauseGame();
        }
    }

    public void GameOver() {
        m_GameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void YouWon() {
        m_YouWonPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void PauseGame() {
        if (!isGamePaused) {
            m_PausePanel.SetActive(true);
            isGamePaused = true;
            Time.timeScale = 0f;
        }
        else if (isGamePaused) {
            m_PausePanel.SetActive(false);
            isGamePaused = false;
            Time.timeScale = 1f;
        }
    }

    public void SelectTower(SOTower _SO) {
        PlayerManager.instance.SelectTower(_SO);
    }

    public void UpdateSelectedTowerUI(Image _selected) {
        if (m_SelectedTowerPanel) {
            m_SelectedTowerPanel.color = m_UnselectedTowerColor;
        }
            m_SelectedTowerPanel = _selected;
            m_SelectedTowerPanel.color = m_SelectedTowerColor;
    }

    public void UpdateWaveUI(int _currentWave, int _maxWaves) {
        m_WaveTMP.text = $"Wave: {_currentWave}/{_maxWaves}";
    }

    public void UpdateCoinsUI(int _coins) {
        m_CoinsTMP.text = $"Coins: {_coins}";
    }

    public void UpdateHealthUI(int _health) {
        m_HealthTMP.text = $"Health: {_health}";
    }
}
