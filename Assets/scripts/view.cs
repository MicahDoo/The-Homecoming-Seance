using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class view : DATA {

    // Use this for initialization
    public float sensitivity = 8f;
    public float maxXAngle = 60f;
    public float maxYAngle = 70f;
    Transform t;
    private Vector2 currentRotation;
    void Start () {
        t = GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetMouseButtonDown(1))
        {
            if (mouseOn)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                mouseOn = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                mouseOn = true;
            }
        }
        if(!mouseOn)
        {
            currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
            currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity;
            transform.Rotate(Input.GetAxis("Mouse X") * sensitivity * Vector3.up);
            //currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
            //currentRotation.x = Mathf.Clamp(currentRotation.x, -maxXAngle, maxXAngle);
            currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
            Camera.main.transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
        }
    }
}
