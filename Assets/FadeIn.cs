using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeIn : MonoBehaviour {
    Image img;
    public bool isOut;
    public string nextScene;
	// Use this for initialization
	void Start () {
        img = GetComponent<Image>();
        StartCoroutine(fadeIn());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator fadeIn()
    {
        yield return null;
        while (img.color.a > 0.05)
        {
            yield return null;
            img.color = new Color(0, 0, 0, (img.color.a - 0.8f * Time.deltaTime));
        }
        img.color = new Color(0, 0, 0, 0);
        if(isOut)
            StartCoroutine(fadeOut());
    }

    public void fade() {
        StartCoroutine(fadeOut());
    }

    public IEnumerator fadeOut() {
        yield return new WaitForSeconds(2f);
        StopCoroutine(fadeIn());
        while (img.color.a < 0.95)
        {
            yield return null;
            img.color = new Color(0, 0, 0, (img.color.a + 0.8f * Time.deltaTime));
        }
        img.color = new Color(0, 0, 0, 1);
        LoadScene.targetScene = nextScene;
        SceneManager.LoadScene("loadingScene");
    }
}
