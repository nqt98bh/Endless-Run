using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public static CharacterController Instance;
    public int Score = 0;
    private void Awake()
    {
       
            Instance = this;
      
        
      
    }
    public void GetPoint(int amount)
    {
        Score += amount;
    }
}
