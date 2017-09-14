using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leveSimpleInfo 
{
    public string name;
    public string path;
	// Use this for initialization
	
    public leveSimpleInfo()
    {
        //nothing
    }
    public leveSimpleInfo(string n,string p)
    {
        load(n, p);
    }
	public void load(string n,string p)
    {
        name = n;
        path = p;
    }
	
}
