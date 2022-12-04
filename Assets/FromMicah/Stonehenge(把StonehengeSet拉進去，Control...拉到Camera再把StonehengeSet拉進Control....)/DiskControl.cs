using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DiskControl : MonoBehaviour
{
    Vector3 startingOrientation;
    private int Layermask = 1 << 8;
    RaycastHit rayCastHit;
    Ray ray;
    float turningAngle;

    private void Awake()
    {
        
    }

    public void OnMouseDown()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out rayCastHit, 100f, Layermask))
        {
            //Debug.Log("Hit" + rayCastHit.collider.name);
            startingOrientation = rayCastHit.point - transform.position;
        }
    }

    public void OnMouseDrag()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out rayCastHit, 100f, Layermask))
        {
            //Debug.Log("Hit" + rayCastHit.collider.name);
            turningAngle = Vector3.SignedAngle(startingOrientation, rayCastHit.point - transform.position, transform.up);
            transform.Rotate(0f, turningAngle, 0f);
            startingOrientation = rayCastHit.point - transform.position;
        }
    }

}