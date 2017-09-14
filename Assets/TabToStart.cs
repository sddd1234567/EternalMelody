using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TabToStart : MonoBehaviour {
    public AsyncOperation asy;
    public Animator fadeOut;
	// Use this for initialization
	void Start () {
        fadeOut = GetComponent<Animator>();
    }

    public void tap() {
        fadeOut.SetBool("FadeOut", true);
        StartCoroutine(loadScene());
    }

    public IEnumerator loadScene() {
        yield return null;
        asy = SceneManager.LoadSceneAsync(1);
        asy.allowSceneActivation = false;
    }

    public void fadeComplete() {
        asy.allowSceneActivation = true;
    }
}
