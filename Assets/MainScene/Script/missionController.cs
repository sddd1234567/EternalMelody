using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//負責load scene 跟 儲存路徑
public class missionController : MonoBehaviour
{


    public static string path;
	void Start () {
        path = "-1";
	}
	public static void setPath(string ph)
    {
        path = ph;
    }
	// Update is called once per frame
	void Update () {
		
	}
    public void pressEnter()
    {
        Debug.Log(path);
    }
}
