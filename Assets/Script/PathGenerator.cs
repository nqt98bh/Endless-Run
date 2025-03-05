using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    public PoolManager segmentPool;
    public static PathGenerator Instance;
    public UnityEngine.GameObject segmentPrefab;
    Vector3 nextSpawnPoint;
    Quaternion nextRotation = Quaternion.identity;
    private const int SEGMENT_BEFORE_TURN = 5;
    private const int MAX_SEGMENT_COUNT = 500;
    private const int SEGMENT_EACH_RENDER = 60;
    private int DistanceToNextSpawn = 30;
    const int SEGMENT_SQUARE_SIZE = 10;
    private int currentTotalRotation = 0;
    public List<GameObject> segmentList = new List<GameObject>();
    [SerializeField] private Transform player;
    public int currentSegmentPlayerReach { get; set; } = 0;
    public int maxCurrentSegmentIndex { get; set; }
    

    private void Awake()
    {
        if (Instance == null) Instance = this;

    }
    private void Start()
    {
        SpawnSegment(0);

    }
    
    
    void SpawnSegment(int startIndex)
    {
        int endIndex = startIndex + SEGMENT_EACH_RENDER;
        for (int i = startIndex; i < endIndex; i++)
        {
            int segmentCount = i + 1;
            SpawnSegmentFromPool(segmentCount);
            if (segmentCount == endIndex)
            {
                maxCurrentSegmentIndex = segmentCount;
            }

        }
    }
    public void CheckSpawnSegment()
    {
        bool isValid = maxCurrentSegmentIndex - currentSegmentPlayerReach == DistanceToNextSpawn;
        if (isValid)
        {
            SpawnSegment(maxCurrentSegmentIndex);

        }

    }
    private void SpawnSegmentFromPool (int segmentCount)
    {
        int rotationAngle = 0;
        //GameObject newSegment = Instantiate(segmentPrefab, nextSpawnPoint, nextRotation);
        GameObject segmentGO = segmentPool.GetObject(nextSpawnPoint, nextRotation);
        Segment segment = segmentGO.GetComponent<Segment>();
        segment.unitID = segmentCount;
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


