using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Segment : MonoBehaviour
{
    [SerializeField] private List<GameObject> decorationObjects;
    public bool isTurning = false;
    float XLimit = 3f;
    float ZLimit = 3f;
    [SerializeField] private List<GameObject> walls;
 
    private void Start()
    {
        RandomdecorationObjects();
        WallSetUp();
      
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
    private void WallSetUp()
    {
        float yRotation = transform.eulerAngles.y; // Get segment rotation

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


       
        if (Mathf.Approximately(yRotation, 90f) && isTurning == true)
        {
            wallLeft.SetActive(true);
            wallBack.SetActive(true);
        }
        if (Mathf.Approximately(yRotation, -90f) && isTurning == true)
        {
            wallRight.SetActive(true);
            wallBack.SetActive(true);
        }
        wallLeft.SetActive(true);
        wallRight.SetActive(true);
    }

}
