using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightControl : control{

    Light l;
    Color32 lightgreen = new Color32(202, 255, 195, 255);
	// Use this for initialization
	void Start () {
        l = GetComponent<Light>();
        l.color = lightgreen;
	}
	
	// Update is called once per frame
	void Update () {
        if (!lightOn)
            l.intensity = 8;
        else
            l.intensity = 0;
	}
}
