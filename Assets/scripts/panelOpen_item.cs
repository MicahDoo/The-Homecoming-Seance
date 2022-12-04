using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panelOpen_item : control {
    
	// Update is called once per frame
	void Update () {
        if (unlock_F)
        {
            panel.SetActive(true);
        }
        else
            panel.SetActive(false);
	}
    public GameObject panel;
}
