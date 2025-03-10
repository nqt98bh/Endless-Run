using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSoundFX : MonoBehaviour
{
    public void PlayMenuSound()
    {
        SoundFXManager.Instance.PlaySoundFX(SoundType.MenuButtonFX);
    }
    public void PlaySelectMenuSound()
    {
        SoundFXManager.Instance.PlaySoundFX(SoundType.SelectMenu);
        Debug.Log("Select Sound");
    }
}
