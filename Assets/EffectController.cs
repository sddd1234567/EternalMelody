using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectController : MonoBehaviour {
    // Use this for initialization
    public Image fadeMask;

    public GameObject allObj;

    void Awake() {
        allObj.SetActive(false);
    }

	void Start () {
        StartCoroutine(fadeIn());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator fadeIn() {
        fadeMask.color = new Color(0, 0, 0, 1);
        yield return null;
        while (fadeMask.color.a >= 0.05)
        {
            fadeMask.color -= new Color(0, 0, 0, 0.6f * Time.deltaTime);
            yield return null;
        }
        allObj.SetActive(true);
    }
}
