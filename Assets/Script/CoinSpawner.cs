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
      
        ReturnCoin();

    }

 
    public void SpawnCoinRandom(Transform segment)
    {
       
                int randomSpawnType = Random.Range(0, 4);
                if (randomSpawnType == 0)
                {
                    SpawnStraightLine(segment.transform);
                    Debug.Log("Spawn straight");
                }
                else if (randomSpawnType == 2)
                {
                    SpawnZigzagLine(segment.transform);
                    Debug.Log("Spawn ZigZag");
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
    private void SpawnByArc(Transform segmentTransform, int numCoins, float radius, float arcAngle)
    {
        Vector3 segmentPosition = segmentTransform.position;
        Quaternion segmentRotation = segmentTransform.rotation; // Get rotation to align coins with path

        for (int i = 0; i < numCoins; i++)
        {
            float angle = Mathf.Lerp(-arcAngle / 2, arcAngle / 2, (float)i / (numCoins - 1)); // Spread coins evenly

            // Calculate local position
            float localX = radius * Mathf.Sin(angle * Mathf.Deg2Rad);
            float localZ = radius * Mathf.Cos(angle * Mathf.Deg2Rad); // Arc moves forward

            // Convert to world position using segment's rotation
            Vector3 localPosition = new Vector3(localX, 1f, localZ);
            Vector3 worldPosition = segmentPosition + segmentRotation * localPosition;

            SpawnCoin(worldPosition);
        }
    }



    private void SpawnCoin(Vector3 position)
    {
       
        GameObject CoinGo = coinPool.GetObject(position,Quaternion.identity);
        CoinGo.transform.position = position ; 
        Coin coin = CoinGo.GetComponent<Coin>();
        coinPool.Init(CoinGo);
     

    }

    private void ReturnCoin()
    {
        foreach (GameObject coin in coinPool.GetPoolList())
        {
            if (coin.activeInHierarchy && player.position.z > coin.transform.position.z + 4f)
            {
                coinPool.ReturnPool(coin);

            }
        }
    }

}
