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

    private void Start()
    {
        ButtonContinue.onClick.AddListener(Continue);
        ButtonHome.onClick.AddListener(Home);
        ButtonRestart.onClick.AddListener(Restart);
        ButtonPause.onClick.AddListener(Pause);
    }
  
    public void Pause()
    {
        //Time.timeScale = 0f;
        Content.SetActive(true);
        Setting.SetActive(true);
        GameManager.Instance.SetPauseGame(true);

    }
    private void Continue()
    {
        Content.SetActive(false);
        Setting.SetActive(false);
        Time.timeScale = 1.0f;
        GameManager.Instance.SetPauseGame(false);



    }
    private void Home()
    {
        string mainMenuScene = "MainMenu";
        if (Application.CanStreamedLevelBeLoaded(mainMenuScene))
        {
            SceneManager.LoadScene(mainMenuScene);
            SoundFXManager.Instance.StopBackgroundMusic();
        }
    }
    private void Restart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
