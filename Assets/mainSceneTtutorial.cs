using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainSceneTtutorial : MonoBehaviour {
    public GameObject tutorialWindow;
    // Use this for initialization
    public static mainSceneTtutorial instance;

    void Awake() {
        instance = this;
    }

	void Start () {
        StartCoroutine(createWindow());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator createWindow()
    {
        yield return null;

        tutorialWindow.SetActive(true);
    }
}
