using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CanvasControl : MonoBehaviour {

    [SerializeField] GameObject panel;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("PanelDisabled");
            if(panel.activeSelf){
                panel.SetActive(false);
            }
            else
            {
                panel.SetActive(true);
            }
        }
	}
}
