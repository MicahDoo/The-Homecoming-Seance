using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryControl : MonoBehaviour
{

    [SerializeField] GameObject tripodPrefabTemp;
    [SerializeField] GameObject camcorderPrefabTemp;
    //**
    [SerializeField] GameObject cameraVideoPanelTemp;
    [SerializeField] GameObject crossHairsTemp;
    static public GameObject cameraVideoPanel;
    public static GameObject crossHairs;
    public Canvas foggyCanvas;
    static GameObject tripodPrefab;
    static GameObject camcorderPrefab;
    [SerializeField] Text gamePrompt_private;
    public Text gamePrompt
    {
        get
        {
            return gamePrompt_private;
        }
    }
    private Camera mainCamera_private;
    public Camera mainCamera
    {
        get
        {
            return mainCamera_private;
        }
    }

    public GameObject inspectorPanel;
    static public GameObject inspectorPanelStatic;
    private Collectible itemInView_private;
    public Collectible itemInView
    {
        get
        {
            return itemInView_private;
        }
        set
        {
            itemInView_private = value;
        }
    }
    public Inventory inventory;

    private Text itemDescriptionInPanel_private;
    public Text itemDescriptionInPanel
    {
        get
        {
            return itemDescriptionInPanel_private;
        }
    }
    private Text itemNameInPanel_private;
    public Text itemNameInPanel
    {
        get
        {
            return itemNameInPanel_private;
        }
    }
    public BackpackMenuControl backpackDropdownTemp;
    static BackpackMenuControl backpackDropdown;
    public panelOpen settingsButtonTemp;
    static panelOpen settingsButton;
    public GameObject _UICorners;
    static GameObject UICorners;

    Animator animator;
    public static bool moving = false;
    public static bool doorOpen = false;
    private static Vector3 planarInput;
    private static float movingSpeed = 12f;
    private static float rotatingSpeed = 12f;
    private static float freezeSpeed = 0.1f;
    private static float viewportHorizontalInput = 0f;
    private static float viewportVerticalInput = 0f;
    private static float xRotation = 0f; //stating point of first person
    private static float mouseSensitivity = 100f;
    private static float maxXAngle = 60f;  //why public
    private static float maxYAngle = 90f;  //why public
    public static bool controlsShown = false;
    public static bool underTable = false;
    void Awake()
    {
        //Initializations statics
        //tripodPrefab = tripodPrefabTemp;
        //camcorderPrefab = camcorderPrefabTemp;
        backpackDropdown = backpackDropdownTemp;
        //cameraVideoPanel = cameraVideoPanelTemp;
        crossHairs = crossHairsTemp;
        settingsButton = settingsButtonTemp;
        UICorners = _UICorners;
        inspectorPanelStatic = inspectorPanel;

        //
        TimersOrganizer.reminderCountdown = true;
        
        mainCamera_private = GetComponentInChildren<Camera>();
        itemDescriptionInPanel_private = inspectorPanel.GetComponentInChildren<Text>();
        itemNameInPanel_private = inspectorPanel.GetComponentsInChildren<Text>()[1];
        inventory = new Inventory();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

        LanguageControl.gamePrompt = gamePrompt;
        LanguageControl.itemDescriptionInPanel = itemDescriptionInPanel;
        LanguageControl.setUpHashTables();
        ObjectsOrganizer.setUpHashtables();
        {
            /*collectiblesInHealthRoom = collectiblesInHealthRoom_parent.GetComponentsInChildren<Collectible>();
            Debug.Log("ThereAre" + collectiblesInHealthRoom.Length + "CollectiblesInHealthRoom");
            collectiblesInOuijaBoardRoom = collectiblesInOuijaBoardRoom_parent.GetComponentsInChildren<Collectible>();
            Debug.Log("ThereAre" + collectiblesInOuijaBoardRoom.Length + "CollectiblesInOuijaBoardRoom");
            for (int i = 0; i < collectiblesInHealthRoom.Length; ++i)
            {
                collectiblesInHealthRoom[i].SetInventoryInitializer(this);
            }
            for (int i = 0; i < collectiblesInOuijaBoardRoom.Length; ++i)
            {
                collectiblesInOuijaBoardRoom[i].SetInventoryInitializer(this);
            }*/
        }
        Collectible.SetInventoryInitializer(this);
    }

    // Use this for initialization
    void Start()
    {
        //backpackDropdown.options.Clear();
        inspectorPanel.SetActive(false);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //!!!!!Adjust mouse input settings for this part of code
        //Edit -> ProjectSettings -> Input -> Axes Horizontal&Vertical (Gravity = 100, Sensitivity = 100) / MouseX&Y (Gravity = Sensitivity = 1, Dead = 0)
        if (!isInFreezeMode()) //If is not in FreezeMode, let player move and turn freely
        {
            checkMove();

            checkRotationInNavigationMode();
        }
        else //If is in FreezeMode, don't let player move but let them turn a little
        {
            checkRotationInFreezeMode();
        }
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("MouseButtonDown(1)");
            if (controlsShown)
            {
                getOffControlPanel();
            }
            else if (!inspectorPanel.activeSelf)
            {
                showControlPanel();
            }
        }
    }

    private void checkMove()
    {
        //Get the directions the player is going in, but ignore the y direction
        planarInput = Input.GetAxis("Horizontal") * new Vector3(transform.right.x, 0, transform.right.z).normalized + Input.GetAxis("Vertical") * new Vector3(transform.forward.x, 0, transform.forward.z).normalized;

        //GetComponent<Rigidbody>().velocity +=  transform.right * horizontalInput;
        if (planarInput != Vector3.zero)
        {
            if (Input.GetAxis("Fire3") > 0)
            {
                transform.localPosition += 2 * movingSpeed * Time.deltaTime * planarInput;
            }
            else
            {
                transform.localPosition += movingSpeed * Time.deltaTime * planarInput;
            }
            moving = true;
        }
        else
        {
            moving = false;
        }
        animator.SetBool("move", moving);
    }

    private void checkRotationInNavigationMode()
    {
        viewportHorizontalInput = Input.GetAxis("Mouse X");
        if (viewportHorizontalInput != 0)
        {
            transform.Rotate(Vector3.up * viewportHorizontalInput * rotatingSpeed * Time.deltaTime, Space.World);
        }

        viewportVerticalInput = Input.GetAxis("Mouse Y");
        if (viewportVerticalInput != 0)
        {
            //This is another correct way
            xRotation -= viewportVerticalInput * rotatingSpeed * Time.deltaTime;
            xRotation = Mathf.Clamp(xRotation, -maxYAngle, maxYAngle);
            mainCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
    }

    private void checkRotationInFreezeMode()
    {
        viewportHorizontalInput = Input.GetAxis("Mouse X");
        if (viewportHorizontalInput != 0)
        {
            transform.Rotate(Vector3.up * viewportHorizontalInput * freezeSpeed * Time.deltaTime, Space.World);
        }

        viewportVerticalInput = Input.GetAxis("Mouse Y");
        if (viewportVerticalInput != 0)
        {
            //This is another correct way
            xRotation -= viewportVerticalInput * freezeSpeed * Time.deltaTime;
            xRotation = Mathf.Clamp(xRotation, -maxYAngle, maxYAngle);
            mainCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
    }

    static public void getOffControlPanel()
    {
        TimersOrganizer.timeSinceLastActivity = 0f;
        TimersOrganizer.rightClickImage.gameObject.SetActive(false);
        TimersOrganizer.timerForRightClickOn = false;
        Debug.Log("BackToNormalView");
        backpackDropdown.collapseMenu();
        settingsButton.panel.gameObject.SetActive(false);
        setControlPanelActive(false);
    }

    public void showControlPanel()
    {
        TimersOrganizer.timeSinceLastActivity = 0f;
        crossHairs.gameObject.SetActive(false);
        Debug.Log("GoToControlPanel");
        setControlPanelActive(true);
    }

    public static void setControlPanelActive(bool state)
    {
        Cursor.lockState = state ? CursorLockMode.None : Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = state;
        TimersOrganizer.reminderCountdown = !state;
        crossHairs.gameObject.SetActive(!state);
        UICorners.gameObject.SetActive(state);
        controlsShown = state;
    }

    public void setControlPanelActiveNonStatic(bool state)
    {
        Cursor.lockState = state ? CursorLockMode.None : Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = state;
        TimersOrganizer.reminderCountdown = !state;
        crossHairs.gameObject.SetActive(!state);
        UICorners.gameObject.SetActive(state);
        controlsShown = state;
    }

    public void showItemView(Collectible itemInView)
    {
        TimersOrganizer.reminderCountdown = false;
        TimersOrganizer.timeSinceLastActivity = 0f;
        TimersOrganizer.rightClickImage.gameObject.SetActive(false);
        TimersOrganizer.timerForRightClickOn = false;
        crossHairs.gameObject.SetActive(false);
        foggyCanvas.gameObject.SetActive(true);
        inspectorPanel.SetActive(true);
        itemDescriptionInPanel.text = itemInView.getItem.itemDescription;
        itemNameInPanel.text = LanguageControl.getDisplayName(itemInView.getItem.itemName);
        this.itemInView = itemInView;
        gamePrompt.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void getOffItemView()
    {
        TimersOrganizer.reminderCountdown = true;
        crossHairs.gameObject.SetActive(true);
        foggyCanvas.gameObject.SetActive(false);
        itemInView.FadeOut();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        inspectorPanel.SetActive(false);
    }

    //In FreezeMode you can't navigate by moving the mouse (you're even checking the UI or view items)
    public bool isInFreezeMode()
    {
        return inspectorPanel.activeSelf || controlsShown || underTable;
    }

    static public void GameOver()
    {

    }

    static public bool applyItemOn(string used, GameObject usedOn) //returns true if something is done
    {
        try
        {
            if (((ItemApplicationMethod)ObjectsOrganizer.applicationHash[used])(usedOn))
                return true;
            return false;
        }
        catch (NullReferenceException e)
        {
            Debug.Log("Nothing to do here...");
            return false;
        }
    }

    //These are all the methods for the applications of any items
    public delegate bool ItemApplicationMethod(GameObject usedOn); //returns true if something is done


    public static ItemApplicationMethod InstallTripod = (GameObject usedOn) =>
    {
        Debug.Log("TriedToUseTripodOn" + usedOn);
        if (usedOn.transform.name == "Platform")
        {
            Debug.Log("PuttingTripodOnPlatform");
            Inventory.removeFromItemList("Tripod");
            tripodPrefab = Instantiate(tripodPrefab, new Vector3(usedOn.transform.position.x, usedOn.transform.position.y + tripodPrefab.transform.localScale.y, usedOn.transform.position.z), usedOn.transform.rotation);
            tripodPrefab.transform.name = "TripodInstalled";
            LanguageControl.setPromptText("TripodByTable");
            return true;
        }
        return false;
    };

    public static ItemApplicationMethod InstallCamcorder = (GameObject usedOn) =>
    {
        Debug.Log("TriedToUseCamOn" + usedOn);
        if (usedOn.transform.name == "TripodInstalled")
        {
            Inventory.removeFromItemList("Camcorder");
            Debug.Log("SettingCamOnTripod");
            //Vector3 thePosition = tripodPrefab.transform.TransformPoint(0, 0, 0);
            //Instantiate(camcorderPrefab, new Vector3(thePosition.x, thePosition.y + camcorderPrefab.transform.localScale.y/2, thePosition.z), tripodPrefab.transform.rotation);
            camcorderPrefab = Instantiate(camcorderPrefab, new Vector3(tripodPrefab.transform.position.x, tripodPrefab.transform.position.y + tripodPrefab.transform.localScale.y / 2 + camcorderPrefab.transform.localScale.y, tripodPrefab.transform.position.z), tripodPrefab.transform.rotation);
            LanguageControl.setPromptText("CamcorderOnTripod");
            return true;
        }
        return false;
    };

    public static ItemApplicationMethod OpenHealthRoom = (GameObject usedOn) =>
    {
        Debug.Log("TriedToUseFirstKeyOn" + usedOn);
        if (usedOn.transform.name == "Handle")
        {
            Debug.Log("HealroomOpened!");
            doorOpen = true;
            Inventory.removeFromItemList("FirstKey");
            //Vector3 vDoor = new Vector3(-usedOn.transform.parent.localScale.x + 2 * usedOn.transform.parent.localScale.z, 0, usedOn.transform.parent.localScale.x - usedOn.transform.parent.localScale.z);
            usedOn.transform.parent.parent.Rotate(Vector3.up * -90);
            //usedOn.transform.parent.transform.parent.localPosition += vDoor / 2;
            LanguageControl.setPromptText("HealthroomOpened");
            return true;
        }
        return false;
    };

    public static ItemApplicationMethod OpenLidToHandle = (GameObject usedOn) =>
    {
        Debug.Log("TriedToUseExitKeyOn" + usedOn);
        if (usedOn.transform.name == "LockBox")
        {
            Debug.Log("LidOpened!");
            usedOn.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            LanguageControl.setPromptText("LidOpened");
            return true;
        }
        return false;
    };
}
