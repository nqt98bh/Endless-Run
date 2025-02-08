using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity = 1000f; // Sensitivity of the mouse movement
    private float xRotation = 0f;  // To prevent camera flip over
    Vector3 offset = new Vector3(0, 4, -8);
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        //MouseAction();
      

    }
    private void LateUpdate()
    {
        if (player != null)
        {
            // Tính vector offset theo hướng local của target
            Vector3 rotateOffset = player.TransformDirection(offset);
            Vector3 desiredPosition = player.position + rotateOffset;
            transform.position = desiredPosition;

            transform.LookAt(player); //Đảm bảo camera luôn nhìn về phía target
        }

    }
    private void MouseAction()
    {
        //Get mouse input
        float mouseX = Input.GetAxis("Mouse X")*mouseSensitivity*Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y")*mouseSensitivity*Time.deltaTime;

        player.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation= Quaternion.Euler(xRotation, 0f, 0f);

    }

}
