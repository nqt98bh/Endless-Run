using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public Button ButtomPlay;
    public Button ButtomSetting;
    public Button ButtomQuit;

    private void Start()
    {
        Time.timeScale = 1f;
        ButtomPlay.onClick.AddListener(PlayGame);
        ButtomSetting.onClick.AddListener(Setting);
        ButtomQuit.onClick.AddListener(Quit);
    }
    private void PlayGame()
    {
        string sceneGame = "JungleRun";
        if (Application.CanStreamedLevelBeLoaded(sceneGame))
        {
            SceneManager.LoadScene(sceneGame);
        }
        PlayStartSound();
        SoundFXManager.Instance.PlayBackgroundMusic();
       
    }

    private void Setting()
    {
        Debug.Log("Open Setting");
        SoundFXManager.Instance.PlaySoundFX(SoundType.MenuButtonFX);
    }
    private void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // Stop play mode in the editor
#else
        Application.Quit();  // Quit the game in a build
#endif
    }

    private void PlayStartSound()
    {
        SoundFXManager.Instance.PlaySoundFX(SoundType.StartMusic);

    }

}
