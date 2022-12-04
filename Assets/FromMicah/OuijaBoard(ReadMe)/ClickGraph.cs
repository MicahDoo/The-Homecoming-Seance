using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickGraph : MonoBehaviour {

    [SerializeField]GameObject ouijaBoardPanel;
    void OnMouseDown()
    {
        ouijaBoardPanel.SetActive(true);
        InventoryControl.setControlPanelActive(true);
    }
}
