using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject GameOverPanel;
    public GameObject StartingPanel;
    public TextMeshPro GameOverText;
    public bool isGameOver;
    private bool isGameStarting;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        bool isGameOver = false;
        bool isGameStarting = false;
        Time.timeScale = 0;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartingGame();
        }
        if (isGameOver)
        {
            GameOver();
        }

    }
    private void StartingGame()
    {
        Time.timeScale = 1;
        isGameStarting = true;
        StartingPanel.SetActive(false);
    }
    private void GameOver()
    {
        GameOverPanel.SetActive(true);
        Time.timeScale = 0;

    }
}

