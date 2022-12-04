using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour {

    [SerializeField] GameObject Door;
	// Use this for initialization
	void Start () {
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.name=="Player")
        {
            //Vector3 vDoor = new Vector3(-Door.transform.localScale.x + 2 * Door.transform.localScale.z, 0, Door.transform.localScale.x - Door.transform.localScale.z);
            //Door.transform.localPosition -= vDoor / 2;
            Door.transform.parent.Rotate(Vector3.up * 90);
            gameObject.SetActive(false);
        }
    }
}
