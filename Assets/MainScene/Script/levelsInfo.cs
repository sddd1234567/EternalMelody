using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelsInfo : MonoBehaviour
{
    public int size;
    public static List<leveSimpleInfo> levels;
    // Use this for initialization
    void Awake()
    {
        levels = new List<leveSimpleInfo>();
        addLevels();
    }

	void addLevels()
    {
        levels.Add(new leveSimpleInfo("level_1", "level/TestLevel1"));
        levels.Add(new leveSimpleInfo("level_2", "level/TestLevel2"));
        size = levels.Count;
    }
	// Update is called once per frame
	void Update () {
		
	}
}
