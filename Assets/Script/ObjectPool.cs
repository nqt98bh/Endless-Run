using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPool : MonoBehaviour
{

    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize;
    private List<GameObject> poolList = new List<GameObject>();
    public Action RecycleAction;

    private void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject go = Instantiate(prefab);
            go.SetActive(false);
            poolList.Add(go);
        }
    }

    public GameObject GetObject(Vector3 position, Quaternion quaternion)
    {
        foreach (GameObject go in poolList)
        {
            if (!go.gameObject.activeInHierarchy)
            {
                go.transform.position = position;
                go.transform.rotation = quaternion;
                go.SetActive(true);
                return go;
            }
        }
        // if null => init
        GameObject newGo = Instantiate(prefab);
        poolList.Add(newGo);
        newGo.transform.position = position;
        newGo.transform.rotation = quaternion;
        newGo.SetActive(true);
        return newGo;
    }

    public void Init(GameObject go)
    {
        go.SetActive(true);

    }
    public void ReturnPool(GameObject go)
    {
        go.SetActive(false);
    }
    public List<GameObject> GetPoolList()
    {
        return poolList;
    }
}
