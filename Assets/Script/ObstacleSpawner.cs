using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class ObstacleSpawner : MonoBehaviour
{
    public static ObstacleSpawner Instance;
    [SerializeField] List<PoolManager> obstacles = new List<PoolManager>();
    private List<GameObject> obstacleSpawned = new List<GameObject>();
    public Transform player;
    private void Awake()
    {
        Instance = this;
    }
  
    public void SpawnObstacles(Transform segment)
    {
            int ramdom = Random.Range(0, obstacles.Count);
            if (ramdom == 0)
            {

                float randomPosX = Random.Range(-4, 4);
                float randomPosZ = Random.Range(-4, 4);
                Vector3 randomPosition = segment.transform.position + new Vector3(randomPosX, 0, randomPosZ);
                GameObject obstacle = obstacles[Random.Range(0, obstacles.Count)].GetObject(randomPosition, Quaternion.identity);
                obstacleSpawned.Add(obstacle);
                
            }

        
    }

    public void ReturnObstacle()
    {
        foreach (GameObject obstacle in obstacleSpawned)
        {
            if (obstacle.activeInHierarchy && player.transform.position.z < obstacle.transform.position.z + 3f)
            {
                obstacle.SetActive(false);
            }
        }
    }

}

