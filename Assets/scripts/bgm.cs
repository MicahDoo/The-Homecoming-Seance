using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgm : MonoBehaviour {

    AudioSource Bgm;
    [SerializeField] private AudioClip menu_M;
	// Use this for initialization
	void Start () {
        Bgm = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        //if (menu_M != null)
        if (!Bgm.isPlaying)
        {
            Bgm.clip = menu_M;
            Bgm.Play();
        }
    }
}
