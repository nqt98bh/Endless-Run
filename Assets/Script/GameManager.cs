using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameMenu gameMenu;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject StartingPanel;
    [SerializeField] private TextMeshProUGUI ScoreText;
    public bool isGameOver;
    public bool isGameStarting;
    private bool pauseGame  = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        bool isGameOver = false;
        bool isGameStarting = false;
        Time.timeScale = 1;
        SetPauseGame(false);
        
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
        gameMenu.Pause();
        SetPauseGame(true);
        

    }
    private void UpdateScore()
    {
        if (ScoreText == null)
        {
            return;
        }
        ScoreText.text = $" {CharacterController.Instance.Score}";
    }

    public void SetPauseGame(bool isPause)
    {
        pauseGame = isPause;
    }
    public bool IsPausedGame()
    {
        return pauseGame;
    }
    
}

