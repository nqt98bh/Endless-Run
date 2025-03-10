using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
   public static SoundFXManager Instance;

    [SerializeField] private AudioSource soundFX;
    [SerializeField] private AudioSource BackGroundMusic;
    [SerializeField] private AudioClipName[] audioClipNames = new AudioClipName[6];
    [System.Serializable]
    class AudioClipName
    {
        public SoundType soundType;
        public AudioClip audioClip;
    }
 
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }
    public void PlaySoundFX (SoundType soundType)
    {
        soundFX.PlayOneShot(GetAudioClip(soundType));
    }
    private AudioClip GetAudioClip (SoundType soundType)
    {
        AudioClip audioClip = null;
        for(int i = 0; i < audioClipNames.Length; i++)
        {
            if(audioClipNames[i].soundType == soundType)
            {
                audioClip =audioClipNames[i].audioClip;
                break;
            }
        }
        return audioClip;
    }
    public void PlayBackgroundMusic()
    {
        BackGroundMusic.clip = GetAudioClip(SoundType.BackgroundMusic);
        BackGroundMusic.loop = true;
        BackGroundMusic.Play();
    }
}
public enum SoundType
{
    MenuButtonFX,
    BackgroundMusic,
    Jump,
    Death,
    GetCoin,
    StartMusic,
    SelectMenu,
}

