using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MovementControl : MonoBehaviour
{
    private Camera mainCamera;
    private static Vector3 planarInput;
    private static float movingSpeed = 10f;
    private static float rotatingSpeed = 10f;
    private static float freezeSpeed = 1f;
    private static float viewportHorizontalInput = 0f;
    private static float viewportVerticalInput = 0f;
    private static float xRotation = 0f; //stating point of first person
    private static float maxXAngle = 60f;  //why public
    private static float maxYAngle = 65f;  //why public
    private static bool controlsShown = false;
    void Awake()
    {
        mainCamera = GetComponentInChildren<Camera>();
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        checkMove();

        checkRotationInNavigationMode();
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
        }
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
}
