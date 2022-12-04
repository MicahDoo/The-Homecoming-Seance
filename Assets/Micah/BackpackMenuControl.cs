using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackpackMenuControl : MonoBehaviour {

    [SerializeField] public Button[] items = new Button[10];
    private bool opened = false;
	// Use this for initialization
	void Start () {
        /*for (int i = 0; i < 10; i++)
        {
            items[i] = this.GetComponentsInChildren<Button>()[i];
        }*/
    }
	
	// Update is called once per frame
	public void Slots() {
        if (opened)
        {
            collapseMenu();
        }
        else
        {
            openMenu();
        }
	}

    public void openMenu()
    {
        opened = true;
        Debug.Log("OpenMenu");
        Debug.Log("ListLength: " + Inventory.getItemListCount());
        for (int i = 0; i < Inventory.getItemListCount(); i++)
        {
            items[i].GetComponentInChildren<Text>().text = LanguageControl.getDisplayName(Inventory.getItemList()[i].itemName);
            items[i].name = Inventory.getItemList()[i].itemName;
            items[i].gameObject.SetActive(true);
        }
    }

    public void collapseMenu()
    {
        Debug.Log("CollpaseMenu");
        opened = false;
        for (int i = 0; i < 10; i++)
        {
            if(items[i] != null)
            {
                items[i].gameObject.SetActive(false);
            }
        }
    }
}
