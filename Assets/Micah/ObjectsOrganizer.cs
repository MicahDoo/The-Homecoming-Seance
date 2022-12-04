using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class ObjectsOrganizer {

    static public Hashtable applicationHash;
    static public Hashtable itemCategory;
    static public Hashtable objectViewPanel;

    static public void setUpHashtables()
    {
        //****Add Function to Each Object
        applicationHash = new Hashtable();
        applicationHash.Add("Tripod", InventoryControl.InstallTripod);
        applicationHash.Add("Camcorder", InventoryControl.InstallCamcorder);
        applicationHash.Add("FirstKey", InventoryControl.OpenHealthRoom);
        applicationHash.Add("ExitKey", InventoryControl.OpenLidToHandle);
        //Add more hash items here...

        //****Assign Each Collectible a Category: Collectible, Viewable, Notes
        itemCategory = new Hashtable();
        itemCategory.Add("Tripod", "Collectible");
        itemCategory.Add("Camcorder", "Collectible");
        itemCategory.Add("TopMagnet", "Collectible");
        itemCategory.Add("BottomMagnet", "Collectible");
        itemCategory.Add("FirstKey", "Collectible");
        itemCategory.Add("BabySketchLiu", "Note");
        itemCategory.Add("ModelSketchLiu", "Note");
        itemCategory.Add("BabySketchChang", "Note");
        itemCategory.Add("ModelSketchChang", "Note");
        itemCategory.Add("BabySketchWu", "Note");
        itemCategory.Add("ModelSketchWu", "Note");
        itemCategory.Add("CoupleShot", "Note");
        itemCategory.Add("LetterToHer", "Note");
        itemCategory.Add("Prelude", "Note");
        itemCategory.Add("YiJingHint", "Note");
        itemCategory.Add("YiJingChart", "Note");
        itemCategory.Add("WuXingChart", "Note");
        itemCategory.Add("ShadowGameHint", "Note");
        itemCategory.Add("PencilHint", "Note");
        itemCategory.Add("LaserHint", "Note");
        itemCategory.Add("FootprintsHint", "Note");
        itemCategory.Add("DarknessHint", "Note");
        itemCategory.Add("ScriptFromMole", "Note");
        itemCategory.Add("PostcardToMole", "Note");
        itemCategory.Add("LastWord1", "Viewable");
        itemCategory.Add("LastWord2", "Viewable");
        itemCategory.Add("ExitKey", "Collectible");
        //Add more hash items here...

        //****Assign a panel to each view object event
        objectViewPanel = new Hashtable();
        objectViewPanel.Add("CamcorderRolling", InventoryControl.cameraVideoPanel);
        //Add more hash items here...
    }
}
