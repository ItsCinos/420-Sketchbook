using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public float moveSpeed = 2;
    //public float mouseSensitivityX = 10;
    //public float mouseSensitivityY = 10;

    private CharacterController pawn;
    //private Camera cam;

    //float cameraPitch = 0;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pawn = GetComponent<CharacterController>();
        //cam = GetComponentInChildren<Camera>();
    }

    
    void Update()
    {
        MovePlayer();
        //TurnPlayer();
    }

    //void TurnPlayer()
    //{
    //    float h = Input.GetAxis("Mouse X");
    //    float v = Input.GetAxis("Mouse Y");

    //    transform.Rotate(0, h * mouseSensitivityX, 0); //turn player left and right

    //    //cam.transform.Rotate(v * mouseSensitivityY, 0, 0);

    //    cameraPitch += v * mouseSensitivityY;

    //    if (cameraPitch < -80) cameraPitch = -80;
    //    if (cameraPitch >  80) cameraPitch =  80;

    //    cameraPitch = Mathf.Clamp(cameraPitch, -80, 80);

    //    cam.transform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);


    //}

    void MovePlayer()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        float u = Input.GetAxis("Move Up");
        //float d = Input.GetAxis("Move Down");

        //transform.position += new Vector3(moveSpeed * Time.deltaTime * h, 0, 0);
        //transform.position += new Vector3(0, 0, moveSpeed * Time.deltaTime * v);

        //transform.position += transform.right * moveSpeed * Time.deltaTime * h;
        //transform.position += transform.forward * moveSpeed * Time.deltaTime * v;

        //transform.position += (transform.right * h + transform.forward * v) * moveSpeed * Time.deltaTime;

        Vector3 speed = (transform.right * h + transform.forward * v) * moveSpeed;
        pawn.Move(speed);

        Vector3 up = (transform.up * u) * moveSpeed;
        pawn.Move(up);

        //Vector3 down = (transform.up * -d) * moveSpeed;
        //pawn.Move(down);
    }
}
