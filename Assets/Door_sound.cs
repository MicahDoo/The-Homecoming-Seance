using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_sound : MonoBehaviour {

    [SerializeField] AudioSource boardcast;
    [SerializeField] AudioClip SoundEffect;
    bool played = false;
    // Use this for initialization
    void Start()
    {
    }
    void Update()
    {
        if (InventoryControl.doorOpen)
        {
            boardcast.clip = SoundEffect;
            boardcast.Play();
            InventoryControl.doorOpen = false;
        }
    }
}
