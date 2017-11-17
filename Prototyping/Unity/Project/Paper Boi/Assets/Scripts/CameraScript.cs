using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public float cameraXMin;
    public float cameraXMax;
    public bool stopCam;

	void FixedUpdate ()
    {
        if (Camera.main.transform.position.x <= cameraXMin)
        {
            if(GameObject.FindWithTag("Player").transform.position.x > cameraXMin)
            {
                stopCam = false;
            }
            else if (GameObject.FindWithTag("Player").transform.position.x < cameraXMin)
            {
                stopCam = true;
            }
        }

         if (Camera.main.transform.position.x >= cameraXMax)
        {
            if (GameObject.FindWithTag("Player").transform.position.x < cameraXMax)
            {
                stopCam = false;
            }
            else if (GameObject.FindWithTag("Player").transform.position.x > cameraXMax)
            {
                stopCam = true;
            }
        }

        if (stopCam == false)
        {
            Camera.main.transform.position = new Vector3(GameObject.FindWithTag("Player").transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
        }
	}
}
