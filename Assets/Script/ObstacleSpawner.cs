using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    [SerializeField] List<GameObject> obstacles = new List<GameObject>();

    private void Start()
    {
        SpawnObstacles();
    }
    void SpawnObstacles()
    {
        
        foreach (GameObject segment in PathGenerator.Instance.segmentList)
        {
            
                float randomPosX = Random.Range(-4, 4);
                float randomPosZ = Random.Range(-4, 4);
                Vector3 randomPosition = segment.transform.position + new Vector3(randomPosX, 0, randomPosZ);
                GameObject Obstacle = obstacles[Random.Range(0, obstacles.Count)];
                Instantiate(Obstacle,  randomPosition, Quaternion.identity);
            
        }
    }
}
