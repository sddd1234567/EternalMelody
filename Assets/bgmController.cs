using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgmController : MonoBehaviour {
    public AudioSource bgm;
    // Use this for initialization
    public static bgmController instance;

    void Awake() {
        instance = this;
        bgm = GetComponent<AudioSource>();
    }

	void Start () {
        bgm.volume = 0;
        StartCoroutine(fadeIn());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator fadeIn() {
        yield return null;
        while (bgm.volume < 0.56)
        {
            yield return null;
            bgm.volume += Time.deltaTime;
        }
    }

    public IEnumerator fadeOut()
    {
        yield return null;
        while (bgm.volume > 0)
        {
            yield return null;
            bgm.volume -= Time.deltaTime;
        }
    }

    public IEnumerator waitForChange(AudioClip audio) {
        yield return null;
        while (bgm.volume > 0)
        {
            yield return null;
            //Debug.Log("wait");
        }
        bgm.Stop();
        bgm.PlayOneShot(audio);
        StartCoroutine(fadeIn());
    }
}
