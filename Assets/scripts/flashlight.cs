using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashlight : MonoBehaviour {

    // Use this for initialization
    Light l;
    bool lightOn = false;
    void Start () {
        l = GetComponent<Light>();
        l.intensity = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (lightOn)
            {
                lightOn = false;
                l.intensity = 0;
            }
            else
            {
                lightOn = true;
                l.intensity = 5;
            }
        }
    }
}
