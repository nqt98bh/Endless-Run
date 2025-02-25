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
    
    private void Start()
    {
        int randomSpawn = Random.Range(0, 3);
        if(randomSpawn == 0)
        {
            SpawnStraightLine();

        }



    }

   private void SpawnStraightLine()
    {
        
        foreach (GameObject segment in PathGenerator.Instance.segmentList)
        {
            int randomLane = Random.Range(0, 3);
            Vector3 lane = new Vector3(lanes[randomLane],1,0);

                for (int i = 0; i < coinCount; i++)
                {
                    Spawner(segment.transform.position+ lane  + segment.transform.forward * i * SEGMENT_WIDTH);
                }
            

        }
    }
    private void SpawnByArc(int numCoins, float radius, float arcAngle)
    {
        foreach (GameObject segment in PathGenerator.Instance.segmentList)
        {
            Vector3 startPos = segment.transform.position;  
            for (int i = 0; i < numCoins; i++)
            {
                float angle = Mathf.Lerp(-arcAngle / 2, arcAngle / 2, (float)i / (numCoins - 1)); // Spread coins evenly

                float x = startPos.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
                float y = startPos.y + radius * Mathf.Cos(angle * Mathf.Deg2Rad); // Fix the arc to curve upwards
                float z = startPos.z+ i*SEGMENT_WIDTH; // Keep it on the same forward path
                Vector3 position = new Vector3(x, y, z);
                Spawner(position);
            }
        }
    }


    private void Spawner(Vector3 position)
    {
       
        GameObject CoinGo = coinPool.GetObject(position,Quaternion.identity);
        CoinGo.transform.position = position ; 
        Coin coin = gameObject.GetComponent<Coin>();
        coinPool.Init(gameObject);

    }


}
