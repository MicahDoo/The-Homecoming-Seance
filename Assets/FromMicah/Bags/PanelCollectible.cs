using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelCollectible : Collectible, IPointerDownHandler{

    Rect itemViewRect;
    //RectTransform 
    /*public Item getItem
    {
        get
        {
            return item;
        }
    }*/

    public void OnPointerDown(PointerEventData e)
    {
        if (!InventoryControl.inspectorPanelStatic.activeSelf)
        {
            switch ((string)ObjectsOrganizer.itemCategory[transform.name])
            {
                case "Collectible":
                    MoveToContainer();
                    break;
                case "Viewable": //watch video on camera or find stuff in bags
                    ViewObject();
                    break;
                case "Note":
                    Debug.Log("IsNote");
                    ViewNoteAndAdd();
                    break;
                case "Toggle":
                    ToggleObject();
                    break;
                default:
                    Debug.Log("Can'tMatchCategory");
                    break;
            }
        }
        else
        {
            Debug.Log("InspectorIsOn");
        }
    }

    void Awake()
    {
        //Initialization
        item = new Item(name);
        itemViewRect = new Rect(-100, -100, 200, 200);
    }

    // Use this for initialization
    void Start () {
        if(LanguageControl.getDescription(this.name) != null)
        {
            item.itemDescription = LanguageControl.getDescription(this.name);
        }
        else
        {
            item.itemDescription = "";
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        //After being clicked, move object to the front
        if (isClicked)
        {
            if (isApproaching)
            {
                transform.localPosition = Vector3.Lerp(startPosition, new Vector3(0, 0, 0), timeElapsed);
                transform.eulerAngles = Vector3.Lerp(startRotation, endRotation, timeElapsed);
                Debug.Log("NewRotation = " + transform.eulerAngles);
                timeElapsed += Time.deltaTime;
                if (timeElapsed >= 1f)
                {
                    transform.SetAsLastSibling();
                    isApproaching = false;
                }
            }
        }
    }

    void MoveToContainer()
    {
        TimersOrganizer.timeSinceLastActivity = 0f;
        Debug.Log("Adding" + this.name);

        //*****Change Layer So As not To get fogged up
        gameObject.layer = 8;

        //Add item to inventory class
        inventoryInitializer.inventory.AddItem(item);

        //Start animation
        isClicked = true;
        isApproaching = true;
        inventoryInitializer.showItemView(this);
        startPosition = transform.localPosition;
        startRotation = transform.eulerAngles;
        endRotation = new Vector3(0, 0, 0);
        //if (startRotation.x >= 180) endRotation += new Vector3(360, 0, 0);
        //if (startRotation.y >= 180) endRotation += new Vector3(0, 360, 0);
        if (startRotation.z >= 180 || startRotation.z < 0)
        {
            Debug.Log("360InsteadOf0");
            endRotation += new Vector3(0, 0, 360);
        }
        timeElapsed = 0f;
    }

    void ViewObject()
    {
        TimersOrganizer.timeSinceLastActivity = 0f;
        ((GameObject)ObjectsOrganizer.objectViewPanel[item.itemName]).SetActive(true);
    }

    void ViewNoteAndAdd()
    {
        TimersOrganizer.timeSinceLastActivity = 0f;

        NoteBookParent.AddNote(item.itemName);

        isClicked = true;
        isApproaching = true;
        inventoryInitializer.showItemView(this);
        startPosition = transform.localPosition;
        startRotation = transform.eulerAngles;
        Debug.Log("StartRotation = " + startRotation);
        endRotation = new Vector3(0, 0, 0);
        //if (startRotation.x >= 180) endRotation += new Vector3(360, 0, 0);
        //if (startRotation.y >= 180) endRotation += new Vector3(0, 360, 0);
        if (startRotation.z >= 180 || startRotation.z < 0)
        {
            Debug.Log("360InsteadOf0");
            endRotation += new Vector3(0, 0, 360);
        }
        timeElapsed = 0f;
        Debug.Log("NoteAdded");
        //Destroy(gameObject);
    }

    void ToggleObject()
    {

    }
}
