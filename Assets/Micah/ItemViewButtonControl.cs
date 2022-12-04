using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemViewButtonControl : MonoBehaviour {

    [SerializeField] InventoryControl inventoryInitializer;

    void Awake ()
    {
    }

	// Use this for initialization
	void Start () {
        GetComponentInChildren<Text>().text = LanguageControl.getButtonText("OK");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Load()
    {
        Debug.Log("DestroyingObjectInView");
        inventoryInitializer.getOffItemView();
    }
}
