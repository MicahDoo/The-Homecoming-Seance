using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_SE : MonoBehaviour {

    [SerializeField] AudioSource player;
    [SerializeField] private AudioClip SE;
    bool played = false;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    private void OnMouseDown()
    {
        player.clip = SE;
        player.Play();
        if(played)
        player.enabled = false;

        played = true;
    }
}
