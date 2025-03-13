using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PoolManager : MonoBehaviour
{

    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize;
    private Queue<GameObject> poolQueue = new Queue<GameObject>();
    private void Awake()
    {
       
        SetPool();
    }

    private void SetPool()
    {
        for (int i =0; i < poolSize; i++)
        {
            GameObject objectedPool = Instantiate(prefab);
            objectedPool.SetActive(false);
            poolQueue.Enqueue(objectedPool);
        }
    }



    public GameObject GetObject(Vector3 position, Quaternion quaternion)
    {
        if (poolQueue.Count == 0)
        {
            GameObject newGo = Instantiate(prefab);
            newGo.transform.position = position;
            newGo.transform.rotation = quaternion;
            return newGo;
        }
        GameObject go = poolQueue.Dequeue();
        go.transform.position = position;
        go.transform.rotation = quaternion;
        return go;
   
    }

    public void Init(GameObject go)
    {
        go.SetActive(true);

    }
    public void ReturnPool(GameObject go)
    {
        go.SetActive(false);
        poolQueue.Enqueue(go);
    }
    public Queue<GameObject> GetPoolList()
    {
        return poolQueue;
    }
    
}
