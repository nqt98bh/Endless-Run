using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public ObjectPool coinPool;


    public void Spawner(Vector3 position)
    {
        GameObject gameObject = coinPool.GetObject(position,Quaternion.identity);
        gameObject.transform.position = position;
        Coin coin = gameObject.GetComponent<Coin>();
        coinPool.Init(gameObject);
    }
}
