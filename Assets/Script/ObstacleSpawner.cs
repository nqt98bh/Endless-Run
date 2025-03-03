using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] List<PoolManager> obstacles = new List<PoolManager>();
   
    private void Start()
    {


        SpawnObstacles();
    }
    void SpawnObstacles()
    {
        
        foreach (GameObject segment in PathGenerator.Instance.segmentList)
        {

            int ramdom = Random.Range(0,obstacles.Count);
            if (ramdom == 0)
            {

                float randomPosX = Random.Range(-4, 4);
                float randomPosZ = Random.Range(-4, 4);
                Vector3 randomPosition = segment.transform.position + new Vector3(randomPosX, 0, randomPosZ);
                GameObject obstacle = obstacles[Random.Range(0, obstacles.Count)].GetObject(randomPosition,Quaternion.identity);

            }
            
        }
    }
    
}

