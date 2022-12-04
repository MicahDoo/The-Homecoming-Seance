using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {

    static private List<Item> itemList;
    static private List<Item> noteList;

    public Inventory()
    {
        itemList = new List<Item>();
        //AddItem(new Item("TopMagnet"){ itemType = Item.ItemType.TopMagnet, amount = 1, itemName = "TopMagnet" });
        //Debug.Log(itemList.Count);
    }
	
    public void AddItem(Item item)
    {
        itemList.Add(item);
        Debug.Log(item.itemName + "Added");
        PrintItems();
    }

    public void PrintItems()
    {
        Debug.Log("Item List:");
        int i = 0;
        foreach (Item item in itemList)
        {
            i++;
            Debug.Log(i.ToString() + ": " + item.itemName);
        }
    }

    static public void removeFromItemList(string itemToRemove)
    {
        for(int i = 0; i < itemList.Count; ++i)
        {
            if(itemList[i].itemName == itemToRemove)
            {
                itemList.RemoveAt(i);
            }
        }
    }

    static public int getItemListCount()
    {
        return itemList.Count;
    }

    static public List<Item> getItemList()
    {
        return itemList;
    }
}
