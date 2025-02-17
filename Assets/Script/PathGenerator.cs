using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    public UnityEngine.GameObject segmentPrefab;
    Vector3 nextSpawnPoint;
    Quaternion nextRotation =Quaternion.identity;
    private const int SEGMENT_BEFORE_TURN = 5;
    private const int MAX_SEGMENT_COUNT = 500;
    const int SEGMENT_SQUARE_SIZE = 10;

    private int currentTotalRotation = 0;

    private void Start()
    {
        for (int i = 0; i < MAX_SEGMENT_COUNT; i++)
        {
            // Lấy góc quay mới bằng hàm RandomTurnWithConstraint
            GenerateSegment(i+1);
          
            //GetRotation();
          
        }
    }
    public void GenerateSegment(int segmentCount)
    {

        UnityEngine.GameObject newSegment = Instantiate(segmentPrefab, nextSpawnPoint, nextRotation);
        if(segmentCount % SEGMENT_BEFORE_TURN == 0)
        {
            int rotationAngle = RandomTurnWithConstraint();
            //Debug.Log("Rotation angle: "+rotationAngle);
            nextRotation *= Quaternion.Euler(0, rotationAngle, 0);
            currentTotalRotation += rotationAngle;
            //Debug.Log("CurrentTotalrotation: "+currentTotalRotation);
        }
        // Update the spawn point for the next segment
      
        nextSpawnPoint += newSegment.transform.forward*SEGMENT_SQUARE_SIZE ;// Move 1 unit forward in the local forward direction

    }
    int RandomTurnWithConstraint()
    {
        int randomTurn;
        switch(currentTotalRotation)
        {
            case -90:
                randomTurn = Random.Range(0, 2)*90; break;
            case 90:
                randomTurn = Random.Range(-1, 1)*90; break;
            default:
                randomTurn = Random.Range(-1, 2) * 90; break;
        }
        return randomTurn;
    }


}
