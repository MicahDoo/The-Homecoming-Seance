using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class boxOpen : MonoBehaviour {
    Transform obj;
    bool touched = false;
    [SerializeField]public GameObject pwd_panel;
	// Use this for initialization
	void Start () {
        obj = GetComponent<Transform>();
        pwd_panel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        pwd_panel.SetActive(true);
        TimersOrganizer.reminderCountdown = false;
        TimersOrganizer.timeSinceLastActivity = 0f;
        TimersOrganizer.rightClickImage.gameObject.SetActive(false);
        TimersOrganizer.timerForRightClickOn = false;
        InventoryControl.crossHairs.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        /*if (!touched)
        {
            obj.parent.parent.transform.Rotate(0, 90f, 0);
            touched = true;
            
        }*/

    }
}
