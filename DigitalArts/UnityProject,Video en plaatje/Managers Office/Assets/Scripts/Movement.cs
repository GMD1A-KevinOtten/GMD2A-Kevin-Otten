using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float movementSpeed;
    public float mouseSensitivity = 10.0f;
    public float upDownRange = 60.0f;
    float verticalRotation = 0;
    float rotLeftRight = 0;
    public float upSpeed;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        //Rotation
        rotLeftRight += Input.GetAxis("Mouse X") * mouseSensitivity;
        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, rotLeftRight, 0);


        //movement
        float forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
        float sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;

        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
        transform.Translate(Vector3.right * sideSpeed * Time.deltaTime);

        if(Input.GetButton("Space"))
        {
            print("Works");
            transform.Translate(Vector3.up * upSpeed * Time.deltaTime);
        }
        
        if(Input.GetButton("Control"))
        {
            transform.Translate(Vector3.up * -upSpeed * Time.deltaTime);
        }
    }
}
