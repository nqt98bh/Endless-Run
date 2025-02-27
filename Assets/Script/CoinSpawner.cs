using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public ObjectPool coinPool;
    [SerializeField] private int coinCount = 10;
    private int SEGMENT_WIDTH = 5;
    private List<int> lanes = new List<int>() { -3, 0, 3 };
    [SerializeField] private Transform player;
    int lastSpawnedSegmentIndex = -1;

    private void Update()
    {
        SpawnCoinRandom();
    }
    private void SpawnCoinRandom()
    {
        for(int i = lastSpawnedSegmentIndex +1; i < PathGenerator.Instance.segmentList.Count; i++)
        {
            GameObject segment = PathGenerator.Instance.segmentList[i];
            if (segment != null && player.position.z + 10f >= segment.transform.position.z)
            {
                int randomSpawnType = Random.Range(0, 2);
                if (randomSpawnType == 0)
                {
                    SpawnStraightLine(segment.transform);
                }
                else /*if (randomSpawnType == 1)*/
                {
                   //SpawnByArc(segment.transform, coinCount, 3f, 50f);
                }
                
                lastSpawnedSegmentIndex =i;
                break;
            }
        } 
    }

   private void SpawnStraightLine(Transform segment)
    {
        int randomLane = Random.Range(0, 3);
        Vector3 lane = new Vector3(lanes[randomLane], 1, 0);

        for (int i = 0; i < coinCount; i++)
        {
            SpawnCoin(segment.transform.position + lane + segment.transform.forward * i * SEGMENT_WIDTH);
        }
        lastSpawnedSegmentIndex +=1;
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
        Coin coin = gameObject.GetComponent<Coin>();
        coinPool.Init(gameObject);

    }


}
