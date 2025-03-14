using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Action RecycleAction;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BehindPlayer"))
        {
            RecycleAction?.Invoke();
        }
    }
  
    public void ReturnObstacleAction(Action _recycleActyion)
    {
        RecycleAction = _recycleActyion;
    }
    
}
