using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Action RecycleAction;

    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerPosition"))
        {
            Debug.Log(" colider with player pisition");
            StartCoroutine(InvokeActionAfterDelay());
        }
    }
    private IEnumerator InvokeActionAfterDelay()
    {
        yield return new WaitForSeconds(3);
       
            RecycleAction?.Invoke();
        


    }
    public void ReturnToPool(Action _recycleActyion)
    {
        RecycleAction = _recycleActyion;
    }
    
}
