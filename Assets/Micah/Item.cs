using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {

    //Contructor: takes the type of the item and decides what to do with it

    public Item(string name)
    {
        itemName = name;
        switch (name)
        {
            case "TopMagnet":
                Debug.Log("SettingNameTopMagnet");
                itemType = ItemType.TopMagnet;
                break;
            case "BottomMagnet":
                Debug.Log("SettingNameBottomMagnet");
                itemType = ItemType.BottomMagnet;
                break;
            case "Camcorder":
                Debug.Log("SettingNameCamcorder");
                itemType = ItemType.Camcorder;
                break;
            case "Tripod":
                Debug.Log("SettingNameTripod");
                itemType = ItemType.Tripod;
                break;
        }
    }
    public enum ItemType
    {
        TopMagnet,
        BottomMagnet,
        Camcorder,
        Tripod,
    }

    public ItemType itemType;
    //public int amount;
    public string itemName;
    public string itemDescription;
}
