using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Coin : MonoBehaviour
{
    private ObjectPool Coinpool;
    private void Start()
    {
        Coinpool = GetComponent<ObjectPool>();
    }

    // Update is called once per frame
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ReturnToPool();
            PlayerController.Instance.GetPoint(1);
        }
    }
    private void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
}
