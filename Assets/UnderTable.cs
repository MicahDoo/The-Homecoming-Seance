using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnderTable : MonoBehaviour {

    Vector3 before_pos;
    Vector3 Under_pos;
    bool entered = false;
    Transform parent;

	// Use this for initialization
	void Start () {
        Under_pos = transform.position + Vector3.down * 7;
        parent = Camera.main.transform.parent.transform;
	}

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("active");
            if (!entered)
            {
                before_pos = Camera.main.transform.localPosition;
                Camera.main.transform.position = Under_pos;
                Camera.main.transform.SetParent(transform);

                entered = true;
            }
            else
            {
                Camera.main.transform.SetParent(parent);
                Camera.main.transform.localPosition = before_pos;
                entered = false;
            }
        }
    }
    // Update is called once per frame
    void Update () {
    }

}
