using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float secondsPerTurn = 0.5f;
    private float lastTurnTime = 0;
    private Quaternion targetRotation;
    private bool isTurning = false;

    private void Start()
    {
        targetRotation = transform.rotation;
    }
    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        //transform.Translate(Vector3.forward*speed * Time.deltaTime);

        if (Time.time - lastTurnTime < secondsPerTurn)
        {
            float maxDegreeDelta = 90f / secondsPerTurn * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, maxDegreeDelta);
            return;
        }
        //float horizonInput = Input.GetAxis("Horizontal"); 
        float horizonInput = Input.GetAxisRaw("Horizontal");

       
        if (horizonInput > 0 && !isTurning)
        {
            StartTurn(90);
            isTurning = true;
        }
        if (horizonInput < 0 && !isTurning)
        {
            StartTurn(-90);
            isTurning = true;
        }
        if (horizonInput == 0)
        {
            isTurning = false;
        }

    }
    private void StartTurn(float angle)
    {
        targetRotation *= Quaternion.Euler(0,angle, 0);
        lastTurnTime = Time.time;
    }
    
}
