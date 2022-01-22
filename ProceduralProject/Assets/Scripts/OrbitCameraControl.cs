using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCameraControl : MonoBehaviour
{

    float pitch = 0;
    float targetPitch = 0;

    float yaw = 0;
    float targetYaw = 0;


    float dollyDis = 10;
    float targetDollyDis = 10;

    public float mouseSensitivityX = 1;
    public float mouseSensitivityY = 1;

    public float mouseScrollMultiplier = 5;

    private Camera cam;

    void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    
    void Update()
    {
        //if (Input.GetButton("Fire2"))
        //{
            //changing pitch
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            targetYaw += mouseX * mouseSensitivityX;
            targetPitch += mouseY * mouseSensitivityY;
        //}

        float scroll = Input.GetAxisRaw("Mouse ScrollWheel");
        targetDollyDis += scroll * mouseScrollMultiplier;
        targetDollyDis = Mathf.Clamp(targetDollyDis, 2.5f, 75);

        dollyDis = AnimMath.Slide(dollyDis, targetDollyDis, .05f); //ease

        cam.transform.localPosition = new Vector3(0, 0, -dollyDis);

        targetPitch = Mathf.Clamp(targetPitch, -89, 89);

        pitch = AnimMath.Slide(pitch, targetPitch, .01f);//ease
        yaw = AnimMath.Slide(yaw, targetYaw, .01f);//ease

        transform.rotation = Quaternion.Euler(pitch, yaw, 0);
    }
}
