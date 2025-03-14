using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Segment : MonoBehaviour
{
    [SerializeField] private List<GameObject> decorationObjects;
    public int unitID { get;set; }
    public bool segmentTurn = false;
    float XLimit = 4f;
    float ZLimit = 4f;
    [SerializeField] private List<GameObject> walls;

    Action RecycleAction;
    private void Start()
    {
        RandomdecorationObjects();
      
    }
    void RandomdecorationObjects()
    {
        foreach (GameObject obj in decorationObjects)
        {
            float randomX = Random.Range(-XLimit, XLimit);
            float randomZ = Random.Range(-ZLimit, ZLimit);
            float randomRotationY = Random.Range(0, 360);
            obj.transform.localPosition = new Vector3(randomX,transform.localPosition.y,randomZ);
            obj.transform.localRotation = Quaternion.Euler(transform.localRotation.x, randomRotationY, transform.localRotation.z);
        }
    }
    public void WallSetUp(float angle)
    {
        // Ensure we have 4 walls in the list
        if (walls.Count < 4) return;

        // Assign walls based on list order (0 = Left, 1 = Right, 2 = Front, 3 = Back)
        GameObject wallLeft = walls[0];
        GameObject wallRight = walls[1];
        GameObject wallFront = walls[2];
        GameObject wallBack = walls[3];

        // Reset all walls first (deactivate them)
        foreach (GameObject wall in walls)
        {
            wall.SetActive(false);
        }

        if (  angle == 90  )
        {
            wallLeft.SetActive(true);
            wallBack.SetActive(true);
        }
        else if (angle == -90 )
        {
            wallRight.SetActive(true);
            wallBack.SetActive(true);
        }
        else
        {
            wallLeft.SetActive(true);
            wallRight.SetActive(true);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision == null) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            PathGenerator.Instance.currentSegmentPlayerReach += 1;
            PathGenerator.Instance.CheckSpawnSegment();
            
        }
      

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BehindPlayer"))
        {
            RecycleAction?.Invoke();

        }
    }
 
    public void ReturnAction (Action _recycleAction)
    {
        RecycleAction = _recycleAction;
    }
}
