using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bag : MonoBehaviour {

    public GameObject bagsPanel;
    List<GameObject> bags;
	// Use this for initialization
	void Start () {
        bags = new List<GameObject>();
		foreach(Transform bag in bagsPanel.transform)
        {
            bags.Add(bag.gameObject);
        }
	}

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            foreach (GameObject bag in bags)
            {
                if (bag.name.Contains(transform.name) || bag.name == "LeaveTrigger")
                {
                    bag.gameObject.SetActive(true);
                }
                else
                {
                    bag.gameObject.SetActive(false);
                }
            }
            bagsPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            TimersOrganizer.reminderCountdown = false;
            InventoryControl.crossHairs.gameObject.SetActive(false);
            InventoryControl.controlsShown = true;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
