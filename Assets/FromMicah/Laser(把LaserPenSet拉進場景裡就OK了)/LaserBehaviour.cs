using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour {

    private static bool isInUse = false;
    private bool isShooting = false;
    private Vector3 laserOrigin;
    private RaycastHit hit;
    private RaycastHit secondHit;
    [SerializeField] private Light spotlight;
    [SerializeField] private Light secondSpotlight;

	// Use this for initialization
	void Start () {
        laserOrigin = new Vector3(transform.position.x, transform.position.y + 0.9f, transform.position.z);
        spotlight = GetComponentInChildren<Light>();
        secondSpotlight = GetComponentsInChildren<Light>()[1];
        spotlight.enabled = false;
        secondSpotlight.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (isShooting)
        {
            if (Input.GetMouseButton(0))
            {
                if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
                {
                    transform.Rotate((Vector3.up * Input.GetAxis("Mouse X") + Vector3.right * Input.GetAxis("Mouse Y")) * 10f * Time.deltaTime, Space.World);
                    makeRaycast();
                }
            }
        }
	}

    void OnMouseDown()
    {
        Debug.Log("MouseDown");
        setInUse();
        startShooting();
    }

    void startShooting()
    {
        isShooting = true;
        spotlight.enabled = true;
        makeRaycast();
    }

    void makeRaycast()
    {
        if (Physics.Raycast(laserOrigin, transform.forward, out hit))
        {
            spotlight.transform.position = Vector3.Lerp(laserOrigin, hit.point, (hit.distance - 5) / hit.distance);
            Debug.Log("Hit" + hit.collider.name);
            if (hit.collider.name == "MirrorReflection")
            {
                Debug.Log("RayBounce");
                if (Physics.Raycast(hit.point, Vector3.Reflect(transform.forward, hit.normal), out secondHit))
                {
                    secondSpotlight.enabled = true;
                    Debug.Log("RayBounceSucceeded");
                    secondSpotlight.transform.position = Vector3.Lerp(hit.point, secondHit.point, (secondHit.distance - 5) / secondHit.distance);
                    secondSpotlight.transform.forward = Vector3.Reflect(transform.forward, hit.normal);
                }
                else
                {
                    secondSpotlight.enabled = false;
                }
            }
            else
            {
                secondSpotlight.enabled = false;
            }
        }
    }

    void setInUse()
    {
        isInUse = true;
    }

    /// <summary>
    /// We have to decide what to do with this one
    /// </summary>
    void setNotInUse()
    {
        isInUse = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(laserOrigin, hit.point);
        Gizmos.DrawLine(hit.point, secondHit.point);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(spotlight.transform.position, hit.point);
        Gizmos.DrawLine(secondSpotlight.transform.position, secondHit.point);
    }
}
