using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour {
    private AudioSource ad;
    public bool isDelay;
    public float delayTime;
	// Use this for initialization
	void Start () {
        
        ad = GetComponent<AudioSource>();
        if (isDelay)
            StartCoroutine(delayPlay(delayTime));
        else
            ad.Play();
	}
	
	// Update is called once per frame
	void Update () {
        if (!ad.isPlaying && !isDelay)
            Destroy(gameObject);
	}

    IEnumerator delayPlay(float time) {
        yield return new WaitForSeconds(time);
        ad.Play();
        isDelay = false;
    }
}
