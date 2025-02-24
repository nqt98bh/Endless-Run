using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject StartingPanel;
    [SerializeField] private TextMeshProUGUI ScoreText;
    public bool isGameOver;
    public bool isGameStarting;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        bool isGameOver = false;
        bool isGameStarting = false;
        Time.timeScale = 1;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            StartingGame();
        }
        if (isGameOver)
        {
            GameOver();
        }

        UpdateScore();
    }
    private void StartingGame()
    {
        isGameStarting = true;
        StartingPanel.SetActive(false);
    }
    private void GameOver()
    {
        GameOverPanel.SetActive(true);
        Time.timeScale = 0;

    }
    private void UpdateScore()
    {
        ScoreText.text = $" {PlayerController.Instance.Score}";
    }
}

