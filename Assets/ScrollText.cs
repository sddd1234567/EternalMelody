using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollText : MonoBehaviour {
    public RectTransform rec;
    public FadeIn fade;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, 100 * Time.deltaTime, 0);
        if (transform.localPosition.y >= 770)
        {
            StartCoroutine(fade.fadeOut());
        }
	}
}
