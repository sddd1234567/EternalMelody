using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateManager : MonoBehaviour {
    public static AnimateManager instance;
    public Image shining;
    // Use this for initialization

    void Awake() {
        instance = this;
    }

	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void doAnimate(string animName , SpriteBattling target) {
        target.doAnimate(animName);
    }
    
}
