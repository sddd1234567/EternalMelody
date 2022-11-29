using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TabToStart : MonoBehaviour {
    public AsyncOperation asy;
    public Animator fadeOut;
    public bool isTouch;
	// Use this for initialization
	void Start () {
        fadeOut = GetComponent<Animator>();
    }

    public void tap() {
        if (!isTouch)
        {
            fadeOut.SetBool("FadeOut", true);
            StartCoroutine(loadScene());
            isTouch = true;
        }
        
    }

    public IEnumerator loadScene() {
        yield return null;
        asy = SceneManager.LoadSceneAsync("TapToStart_MainScene");
        asy.allowSceneActivation = false;
    }

    public void fadeComplete() {
        asy.allowSceneActivation = true;
    }
}
