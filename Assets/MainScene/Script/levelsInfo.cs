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
        levels.Add(new leveSimpleInfo("森林裡的危機", "Level/TestLevel1", Player.instance.levelIndexs[0]));
        levels.Add(new leveSimpleInfo("返回村莊", "Level/Chapter1", Player.instance.levelIndexs[0]));
        if (Player.instance.levelIndexs[1] == 1)
        {
            levels.Add(new leveSimpleInfo("突破重圍", "Level/Chapter2", Player.instance.levelIndexs[0]));
        }
        size = levels.Count;        
    }
	// Update is called once per frame
	void Update () {
		
	}
}
