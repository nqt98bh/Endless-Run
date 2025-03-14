using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    Action RecycleAction;

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RecycleAction?.Invoke();

            CharacterMovement.Instance.SpeedUp(5);
            SoundFXManager.Instance.PlaySoundFX(SoundType.GetCoin);
        }
        else if (other.CompareTag("BehindPlayer"))
        {
            RecycleAction?.Invoke();
        }
    }

    public void ReturnItemAction(Action _recycleActyion)
    {
        RecycleAction = _recycleActyion;
    }
}
