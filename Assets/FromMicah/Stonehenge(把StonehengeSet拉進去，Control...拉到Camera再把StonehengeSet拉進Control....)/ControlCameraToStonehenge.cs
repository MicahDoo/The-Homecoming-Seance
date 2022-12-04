using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControlCameraToStonehenge : MonoBehaviour {
    [SerializeField] GameObject StonehengeSet;
    private bool lerp = false;
    private Vector3 startPosition;
    private Vector3 startRotation;
    private float timeElapsed = 0f;
    private static bool stonehengeFocused = false;
    private Transform parent;
    private Vector3 startLocalPosition;
    [SerializeField] Button leaveButton;

    public static bool StonehengeFocused
    {
        get
        {
            return stonehengeFocused;
        }
    }


	// Use this for initialization
	void Start () {
        parent = transform.parent.transform;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayCastHit;
            if (Physics.Raycast(ray, out rayCastHit))
            {
                if (rayCastHit.collider.name == "StonehengeSet")
                {
                    if (!EventSystem.current.IsPointerOverGameObject() && !GetComponentInParent<InventoryControl>().isInFreezeMode())
                    {
                        transform.SetParent(StonehengeSet.transform);
                        parent.gameObject.SetActive(false);
                        startPosition = transform.position;
                        startRotation = transform.eulerAngles;
                        InventoryControl.crossHairs.SetActive(false);
                        //startLocalPosition = transform.localPosition;
                        lerp = true;
                        StonehengeSet.GetComponent<BoxCollider>().enabled = false; //get rid of the outer box and start interacting with the iner colliders
                    }
                }
            }
        }
	}

    void FixedUpdate()
    {
        if (!stonehengeFocused && lerp)
        {
            transform.position = Vector3.Lerp(startPosition, new Vector3(StonehengeSet.transform.position.x, StonehengeSet.transform.position.y, StonehengeSet.transform.position.z) + new Vector3(StonehengeSet.transform.up.x, StonehengeSet.transform.up.y, StonehengeSet.transform.up.z - 0.5f).normalized * 7, timeElapsed);
            transform.eulerAngles = Vector3.Lerp(startRotation, new Vector3(StonehengeSet.transform.eulerAngles.x + 80, -StonehengeSet.transform.eulerAngles.y, StonehengeSet.transform.eulerAngles.z), timeElapsed);
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= 1f)
            {
                //transform.SetParent(StonehengeSet.transform);
                lerp = false;
                timeElapsed = 0f;
                stonehengeFocused = true;
                Debug.Log("CursorShown");
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                leaveButton.gameObject.SetActive(true);
}
        }
    }

    public void getBack()
    {
        parent.gameObject.SetActive(true);
        transform.SetParent(parent);
        transform.position = startPosition;
        transform.eulerAngles = parent.eulerAngles;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false ;
        InventoryControl.crossHairs.SetActive(true);
        StonehengeSet.GetComponent<BoxCollider>().enabled = true;
        stonehengeFocused = false;
    }
}
