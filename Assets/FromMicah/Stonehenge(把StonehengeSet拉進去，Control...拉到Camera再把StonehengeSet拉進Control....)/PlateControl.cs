using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateControl : MonoBehaviour {

    Vector3 startingOrientation;
    private int Layermask = 1 << 8;
    RaycastHit rayCastHit;
    Ray ray;
    float turningAngle;
    private bool lerp = false;
    Vector3 lerpDestination;
    private Vector3 startPosition;
    private float timeElapsed = 0f;
  

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnMouseDown()
    {
        Debug.Log("MouseDownOnPlate");
        if (ControlCameraToStonehenge.StonehengeFocused)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out rayCastHit, 100f, Layermask))
            {
                Debug.Log("Hit" + rayCastHit.collider.name);
                startingOrientation = rayCastHit.point - transform.position;
            }
        }
        lerp = false;
    }

    public void OnMouseDrag()
    {
        Debug.Log("MouseDragOnPlate");
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out rayCastHit, 100f, Layermask))
        {
            Debug.Log("Hit" + rayCastHit.collider.name);
            turningAngle = Vector3.SignedAngle(startingOrientation, rayCastHit.point - transform.position, transform.up);
            transform.Rotate(0f, turningAngle, 0f);
            startingOrientation = rayCastHit.point - transform.position;
            Debug.Log("Angle = " + transform.eulerAngles.y);
        }
    }

    public void OnMouseUp()
    {
        if(transform.eulerAngles.y > 0f)
        {
            if (transform.eulerAngles.y < 45f)
            {
                LerpTowards(0f);
            }
            else if(transform.eulerAngles.y < 135f)
            {
                LerpTowards(90f);
            }
            else if(transform.eulerAngles.y < 225f)
            {
                LerpTowards(180f);
            }
            else if(transform.eulerAngles.y < 315f)
            {
                LerpTowards(270f);
            }
            else
            {
                LerpTowards(360f);
            }
        }
    }

    void FixedUpdate()
    {
        if (lerp)
        {
            transform.eulerAngles = Vector3.Lerp(startingOrientation, lerpDestination, timeElapsed*2);
            timeElapsed += Time.deltaTime;
            if(timeElapsed > 0.5f)
            {
                transform.eulerAngles = lerpDestination;
                lerp = false;
            }
        }
    }

    private void LerpTowards(float orientation)
    {
        lerp = true;
        lerpDestination = new Vector3(transform.eulerAngles.x, orientation, transform.eulerAngles.z);
        startingOrientation = transform.eulerAngles;
        timeElapsed = 0;
    }
}
