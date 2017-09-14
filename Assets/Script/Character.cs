using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character :Sprite {

    public int nowEXP;//現在這等已經有多少經驗值
    public List<int> requiredEXP;//該等需要的經驗值
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public virtual void LevelUp()
    {
        level++;
        return;
    }
}
