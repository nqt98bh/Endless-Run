using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    public GameObject segmentPrefab;
    Vector3 nextSpawnPoint;
    Quaternion nextRotation =Quaternion.identity;
    private const int SEGMENT_BEFORE_TURN = 5;
    private const int MAX_SEGMENT_COUNT = 20;
    private void Start()
    {
        for (int i = 0; i < MAX_SEGMENT_COUNT; i++)
        {
            int direction = i % 2 == 0 ? 1 : -1;
            GenerateSegment(i+1,direction);
          
            //GetRotation();
          
        }
    }
    public void GenerateSegment(int segmentCount, int direction)
    {

        GameObject newSegment = Instantiate(segmentPrefab, nextSpawnPoint, nextRotation);
        if(segmentCount % SEGMENT_BEFORE_TURN == 0)
        {
            nextRotation *= Quaternion.Euler(0, 90*direction, 0);
        }
        // Update the spawn point for the next segment
      
        nextSpawnPoint += newSegment.transform.forward*10 ;// Move 1 unit forward in the local forward direction

    }
   
}
