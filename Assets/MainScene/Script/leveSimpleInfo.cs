using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leveSimpleInfo 
{
    public string name;
    public string path;
    public int completed;
	// Use this for initialization
	
    public leveSimpleInfo()
    {
        //nothing
    }
    public leveSimpleInfo(string n,string p,int c)
    {
        load(n, p,c);
    }
	public void load(string n,string p, int c)
    {
        name = n;
        path = p;
        completed = c;
    }
	
}
