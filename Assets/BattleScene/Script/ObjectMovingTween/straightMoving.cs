using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class straightMoving : ObjectTween {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public override void startMoving()
    {
        rig.velocity = (target.gameObject.transform.position - attacker.gameObject.transform.position)/ MusicHandler.instance.BPM;        
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
            if (col.tag == "Enemy")
            {
                col.GetComponent<SpriteBattling>().nowHP = 0;
                Destroy(gameObject);
            }
    }
}
