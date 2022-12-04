using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Collectible : MonoBehaviour {

    static protected InventoryControl inventoryInitializer;
    protected Item item; // this variable dictates its behaviour when added
    public Item getItem
    {
        get
        {
            return item;
        }
    }
    protected bool isClicked;
    protected bool isApproaching;
    private bool fading = false;
    private float fadeTimeRemaining = 0.5f;
    //private Material material;
    //private CanvasGroup canvasGroup;
    static private Camera mainCamera;
    protected float timeElapsed;
    protected Vector3 startPosition;
    protected Vector3 startRotation;
    protected Vector3 endRotation;

    //Scheme One: When I click the item on the screen
    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && !inventoryInitializer.isInFreezeMode())
        {
            switch ((string)ObjectsOrganizer.itemCategory[item.itemName])
            {
                case "Collectible":
                    MoveToContainer();
                    break;
                case "Viewable": //watch video on camera or find stuff in bags
                    ViewObject();
                    break;
                case "Note":
                    ViewNoteAndAdd();
                    break;
                default:
                    Debug.Log("Can'tMatchCategory");
                    break;
            }
        }
    }

    //Initialize the item associated with this object
    void Awake()
    {
        //Initialization
        item = new Item(this.name);
        isClicked = false;
        isApproaching = false;
        //canvasGroup = GetComponent<CanvasGroup>();
        //material = GetComponent<Material>();
    }

    // Use this for initialization
    void Start () {
        //Other initializations
        item.itemDescription = LanguageControl.getDescription(this.name);
        Debug.Log(item.itemDescription);
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
                transform.position = Vector3.Lerp(startPosition, new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z) + new Vector3(mainCamera.transform.forward.x, mainCamera.transform.forward.y, mainCamera.transform.forward.z).normalized * 3, timeElapsed);
                transform.eulerAngles = Vector3.Lerp(startRotation, new Vector3(mainCamera.transform.eulerAngles.x, mainCamera.transform.eulerAngles.y, mainCamera.transform.eulerAngles.z), timeElapsed);
                timeElapsed += Time.deltaTime;
                if (timeElapsed >= 1f)
                {
                    transform.SetParent(mainCamera.transform);
                    isApproaching = false;
                }
            }
        }

        /*if (fading)
        {
            //material.color = new Vector4(material.color.r, material.color.g, material.color.b, fadeTimeRemaining / 1f);
            //canvasGroup.alpha = fadeTimeRemaining/0.5f;
            fadeTimeRemaining -= Time.deltaTime;
            if(fadeTimeRemaining <= 0)
            {
                Destroy(gameObject);
            }
        }*/
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
        startPosition = transform.position;
        startRotation = transform.eulerAngles;
        timeElapsed = 0f;
    }

    void ViewObject()
    {
        TimersOrganizer.timeSinceLastActivity = 0f;
        //((GameObject)ObjectsOrganizer.objectViewPanel[item.itemName]).SetActive(true);
        //Start animation
        isClicked = true;
        isApproaching = true;
        inventoryInitializer.showItemView(this);
        startPosition = transform.position;
        startRotation = transform.eulerAngles;
        timeElapsed = 0f;
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

    //Scheme One: reference to each individual object

    static public void SetInventoryInitializer(InventoryControl inventoryInitializer)
    {
        Collectible.inventoryInitializer = inventoryInitializer;
        mainCamera = inventoryInitializer.mainCamera;

    }

    public void FadeOut()
    {
        //needs modificatoin
        Destroy(gameObject);
        //fading = true;
    }
}
