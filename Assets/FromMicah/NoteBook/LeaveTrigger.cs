using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveTrigger : MonoBehaviour {

	// Use this for initialization
	void OnMouseDown()
    {
        Debug.Log("LeaveTrigger");
        transform.parent.gameObject.SetActive(false);
    }

    void OnMouseHover()
    {

    }
}
