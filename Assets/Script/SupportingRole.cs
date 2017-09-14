using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportingRole : Character {
    public Skill skill;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public override void LevelUp()
    {
        base.LevelUp();
        skillUp();
        ATKUp();
    }
    private void skillUp()// not yet
    {

    }
    private void ATKUp()//not yet
    {

    }
}
