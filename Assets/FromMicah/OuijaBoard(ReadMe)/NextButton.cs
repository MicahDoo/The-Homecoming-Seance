using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextButton : MonoBehaviour {

    [SerializeField] RingMoveControl ringMoveControl;
    public void Load()
    {
        ringMoveControl.startNewPage();
    }
}
