using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTween : MonoBehaviour {
    public Rigidbody2D rig;
    public effect sectionEffect;
    public SpriteBattling attacker;
    public SpriteBattling target;
    public string targetTag;
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

	}

    public virtual void startMoving() {
        
    }
    
}
