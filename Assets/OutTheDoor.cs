using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutTheDoor : MonoBehaviour {

    [SerializeField] GameObject Door;
    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            transform.parent.parent.Rotate(Vector3.up * -90);
        }
    }
}
