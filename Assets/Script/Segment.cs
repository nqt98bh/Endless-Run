using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{
    [SerializeField] private List<GameObject> decorationObjects;
    public bool isTurning = false;
    float XLimit = 3f;
    float ZLimit = 3f;
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
}
