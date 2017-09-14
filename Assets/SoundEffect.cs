using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour {
    private AudioSource ad;
	// Use this for initialization
	void Start () {
        ad = GetComponent<AudioSource>();
        ad.Play();
	}
	
	// Update is called once per frame
	void Update () {
        if (!ad.isPlaying)
            Destroy(gameObject);
	}
}
