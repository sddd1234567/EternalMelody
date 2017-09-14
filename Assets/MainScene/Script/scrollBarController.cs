using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scrollBarController : MonoBehaviour {
    const float shift = 380f;//放大value的位移

    public Scrollbar bar;
	// Use this for initialization
	void Start () {
        bar = GetComponent<Scrollbar>();
	}
	public void scrolling(RectTransform list)
    {
        list.localPosition = new Vector3(list.localPosition.x, bar.value * shift, list.localPosition.z);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
