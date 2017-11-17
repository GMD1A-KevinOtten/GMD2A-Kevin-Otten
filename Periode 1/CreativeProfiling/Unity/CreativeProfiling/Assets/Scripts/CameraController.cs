using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float posX = 0;
    public float posZ = 0;
    public float posY = 60;
    public float rotY;
    public float ScrollSpeed;
    public float CSpeed;
    public float mouseMoveSpeed;

    void Update()
    {
        Camera.main.transform.position = new Vector3(posX, posY, posZ);

        posX += Input.GetAxis("Horizontal") * CSpeed;
        posZ += Input.GetAxis("Vertical") * CSpeed;
        posY += Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed;

        if (Input.GetButton("Fire3"))
        {
            rotY += Input.GetAxis("Mouse X");
            Camera.main.transform.localEulerAngles = new Vector3(90, 0, rotY);
        }

        if (Input.mousePosition.x >= Screen.width)
        {
            posX += Time.deltaTime * mouseMoveSpeed;
        }
        else if (Input.mousePosition.x <= 0)
        {
            posX += Time.deltaTime * -mouseMoveSpeed;
        }

        if(Input.mousePosition.y <= 0)
        {
            posZ += Time.deltaTime * -mouseMoveSpeed;
        }
        else if(Input.mousePosition.y >= Screen.height)
        {
            posZ += Time.deltaTime * mouseMoveSpeed;
        }
  
    }
}
