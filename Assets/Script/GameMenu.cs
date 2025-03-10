using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public Button ButtonContinue;
    public Button ButtonHome;
    public Button ButtonRestart;
    public Button ButtonPause;


    [SerializeField] private GameObject Content;
    [SerializeField] private GameObject Setting;

    private bool isPaused = false;
    private void Start()
    {
        ButtonContinue.onClick.AddListener(Continue);
        ButtonHome.onClick.AddListener(Home);
        ButtonRestart.onClick.AddListener(Restart);
        ButtonPause.onClick.AddListener(Pause);
    }
    private void Update()
    {
        
    }
    private void Pause()
    {
        Time.timeScale = 0f;
        Content.SetActive(true);
        Setting.SetActive(true);

    }
    private void Continue()
    {
        Content.SetActive(false);
        Setting.SetActive(false);
        Time.timeScale = 1.0f;


    }
    private void Home()
    {
        string mainMenuScene = "MainMenu";
        if (Application.CanStreamedLevelBeLoaded(mainMenuScene))
        {
            SceneManager.LoadScene(mainMenuScene);

        }
    }
    private void Restart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
