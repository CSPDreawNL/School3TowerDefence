using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    [SerializeField] private KeyCode m_PauseKey = KeyCode.Escape;
    [SerializeField] private GameObject m_GameOverPanel;
    [SerializeField] private GameObject m_YouWonPanel;
    [SerializeField] private GameObject m_PausePanel;

    [SerializeField] private TMPro.TextMeshProUGUI m_WaveTMP;
    [SerializeField] private TMPro.TextMeshProUGUI m_HealthTMP;
    [SerializeField] private TMPro.TextMeshProUGUI m_CoinsTMP;

    private bool isGamePaused = false;

    private void Start() {
        m_GameOverPanel.SetActive(false);
        m_YouWonPanel.SetActive(false);
        m_PausePanel.SetActive(false);
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

    public void UpdateWaveUI(int _wave) {
        m_WaveTMP.text = $"Wave: {_wave}";
    }

    public void UpdateCoinsUI(int _coins) {
        m_CoinsTMP.text = $"Health: {_coins}";
    }
    public void UpdateHealthUI(int _health) {
        m_HealthTMP.text = $"Health: {_health}";
    }
}
