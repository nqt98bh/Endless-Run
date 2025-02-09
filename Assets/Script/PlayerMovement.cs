using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float speed = 5f;
    private bool isTurning = false;
    private void Update()
    {
        //float horizonInput = Input.GetAxis("Horizontal"); 
        float horizonInput = Input.GetAxisRaw("Horizontal");
        if (!isTurning)
        {
            if (horizonInput > 0 )
            {
                Debug.Log("Turning Right");
                transform.Rotate(0, 90, 0);
                isTurning = true;
            }
            if (horizonInput < 0)
            {
                Debug.Log("Turning Left");
                transform.Rotate(0, -90, 0);
                isTurning = true;

            }
        }

        transform.position += transform.forward * speed * Time.deltaTime;
        if (horizonInput == 0)
        {
            isTurning = false;
        }

    }
    
}
