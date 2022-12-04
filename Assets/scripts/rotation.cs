using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour {

    Vector3 v1 = new Vector3(1, 1, -1);
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(v1 * Time.deltaTime * 15);
	}
}
