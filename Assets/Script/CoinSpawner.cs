using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public static CoinSpawner Instance;
    public PoolManager coinPool;
    [SerializeField] private int coinCount = 10;
    private int SEGMENT_WIDTH = 5;
    private List<int> lanes = new List<int>() { -3, 0, 3 };
    [SerializeField] private Transform player;

    private void Awake()
    {
        Instance = this;
    }
  
    
    private void FixedUpdate()
    {
        if (GameManager.Instance.IsPausedGame()) return;
      
     

    }

 
    public void SpawnCoinRandom(Transform segment)
    {
       
                int randomSpawnType = Random.Range(0, 4);
                if (randomSpawnType == 0)
                {
                    SpawnStraightLine(segment.transform);
                }
                else if (randomSpawnType == 2)
                {
                    SpawnZigzagLine(segment.transform);
                }

    }
    
   public void SpawnStraightLine(Transform segment)
    {
        int randomLane = Random.Range(0, 3);
        Vector3 lane = new Vector3(lanes[randomLane], 1, 0);

        for (int i = 0; i < coinCount; i++)
        {
            SpawnCoin(segment.transform.position + lane + segment.transform.forward * i * SEGMENT_WIDTH);
        }
    }

    private void SpawnZigzagLine(Transform segment)
    {
        float zigzagWidth = 3f; // Adjust width of zigzag pattern
        float stepDistance = 2f; // Distance between coins
        float waveFrequency = 1f; // Controls how often the wave oscillates
        Vector3 startPosition = segment.position;

        for (int i = 0; i < 20; i++)
        {
            float offsetX = Mathf.Sin(i * waveFrequency) * zigzagWidth; // Smooth curve movement

            // Calculate local position first
            Vector3 localPosition = new Vector3(offsetX, 1, i * stepDistance);

            // Convert local position to world position using segment's rotation
            Vector3 worldPosition = segment.TransformPoint(localPosition);

            SpawnCoin(worldPosition);
        }
    }

    private void SpawnCoin(Vector3 position)
    {
       
        GameObject CoinGo = coinPool.GetObject(position,Quaternion.identity);
        CoinGo.transform.position = position ; 
        Coin coin = CoinGo.GetComponent<Coin>();
        coin.ReturnCoinAction(() =>
        {
            RecycleCoin(CoinGo);
        });
        coinPool.Init(CoinGo);
     

    }

    private void RecycleCoin(GameObject coin)
    {
        coinPool.ReturnPool(coin);
    }
}
