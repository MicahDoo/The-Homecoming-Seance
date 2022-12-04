using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class motion_transition : MonoBehaviour{

    Animator animator;
    bool moving=false;
    Vector3 temp_pos;
    
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        temp_pos=transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position != temp_pos)
        {
            temp_pos = transform.position;
            moving = true;
        }
        else
            moving = false;
    }
}
