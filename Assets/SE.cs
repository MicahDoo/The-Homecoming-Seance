using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE : MonoBehaviour {

    [SerializeField] AudioSource boardcast;
    [SerializeField] AudioClip SoundEffect;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (!boardcast.isPlaying&& InventoryControl.moving)
        {
            boardcast.clip = SoundEffect;
            boardcast.Play();
        }
        if (!InventoryControl.moving)
            boardcast.Stop();
    }
}
