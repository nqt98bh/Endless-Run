using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class ObstacleSpawner : MonoBehaviour
{
    public static ObstacleSpawner Instance;
    [SerializeField] List<PoolManager> obstacles = new List<PoolManager>();
    public Transform player;
    private void Awake()
    {
        Instance = this;
    }


    public void SpawnObstacles(Vector3 position)
    {
        int ramdomType = Random.Range(0, obstacles.Count);

        float randomPosX = Random.Range(-4, 4);
        float randomPosZ = Random.Range(-4, 4);
        Vector3 randomPosition = position + new Vector3(randomPosX, 0, randomPosZ);
        GameObject obstacleGO = obstacles[ramdomType].GetObject(randomPosition, Quaternion.identity);
        Obstacle obstacle = obstacleGO.GetComponent<Obstacle>();
        obstacleGO.transform.position = randomPosition;
        obstacle.ReturnToPool(() =>
        {
            ReturnObstacle(ramdomType, obstacleGO);
        });
        Init(ramdomType,obstacleGO);
    }

    private void ReturnObstacle(int i ,GameObject go)
    {
        
        obstacles[i].ReturnPool(go);
    }
    private void Init(int i ,GameObject go)
    {
        obstacles[i].Init(go);
    }

}

