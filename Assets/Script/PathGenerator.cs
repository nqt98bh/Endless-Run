using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    public PoolManager segmentPool;
    public static PathGenerator Instance;
    public UnityEngine.GameObject segmentPrefab;
    Vector3 nextSpawnPoint;
    Quaternion nextRotation =Quaternion.identity;
    private const int SEGMENT_BEFORE_TURN = 5;
    private const int MAX_SEGMENT_COUNT = 500;
    const int SEGMENT_SQUARE_SIZE = 10;
    private int currentTotalRotation = 0;
    public List<GameObject> segmentList = new List<GameObject>();
    public List<GameObject> segmentTurn = new List<GameObject>();
    [SerializeField] private Transform player;

    private void Awake()
    {
        if (Instance == null) Instance = this;

    }
    private void Start()
    {
        for (int i = 0; i < MAX_SEGMENT_COUNT; i++)
        {
            
            Spawnsegment(i+1);
        }

    }
    


    private void Spawnsegment (int segmentCount)
    {
        int rotationAngle = 0;
        //GameObject newSegment = Instantiate(segmentPrefab, nextSpawnPoint, nextRotation);
        GameObject segmentGO = segmentPool.GetObject(nextSpawnPoint, nextRotation);
        Segment segment = segmentGO.GetComponent<Segment>();

        if (segmentCount % SEGMENT_BEFORE_TURN == 0)
        {
            rotationAngle = RandomTurnWithConstraint();
            currentTotalRotation += rotationAngle;
            if (rotationAngle != 0)
            {
                segment.segmentTurn = true;
            }
        }
        nextRotation *= Quaternion.Euler(0, rotationAngle, 0);
        segmentGO.transform.rotation = nextRotation;
        segmentList.Add(segmentGO);
        segment.WallSetUp(rotationAngle);

        // Update the spawn point for the next segment

        nextSpawnPoint += segmentGO.transform.forward*SEGMENT_SQUARE_SIZE ;// Move 1 unit forward in the local forward direction
        segment.ReturnAction(() =>
        {
            RecycleSegment(segmentGO);
        });
        InitSegment(segmentGO);
        
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
    private void InitSegment(GameObject segment)
    {
        segmentPool.Init(segment);
    }
    private void RecycleSegment(GameObject segment)
    {
        segmentList.Remove(segment);
        segmentPool.ReturnPool(segment);
    }


}


