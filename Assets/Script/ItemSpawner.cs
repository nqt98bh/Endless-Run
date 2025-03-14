using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    public static ItemSpawner Instance;
    [SerializeField] PoolManager itemPool;
    private void Awake()
    {
        Instance = this;
    }


    public void SpawnItem(Vector3 position)
    {
        Vector3 Extraposition = new Vector3(0, 0.5f, 0);
        GameObject ItemGo = itemPool.GetObject(position + Extraposition, Quaternion.identity);
        ItemGo.transform.position = position;
        SpeedUp speedUp = ItemGo.GetComponent<SpeedUp>();
        speedUp.ReturnItemAction(() =>
        {
            RecycleItem(ItemGo);
        });
        Init(ItemGo);


    }


    private void RecycleItem(GameObject go)
    {

        itemPool.ReturnPool(go);
    }

    private void Init(GameObject go)
    {
        itemPool.Init(go);
    }

}

