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

    [SerializeField] private GameObject m_GameOverPanel;
    [SerializeField] private GameObject m_YouWonPanel;

    private void Start() {
        m_GameOverPanel.SetActive(false);
        m_YouWonPanel.SetActive(false);
    }

    public void GameOver() {
        m_GameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void YouWon() {
        m_YouWonPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
