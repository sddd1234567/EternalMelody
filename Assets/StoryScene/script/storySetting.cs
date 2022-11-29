using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class storySetting : MonoBehaviour {

    public string nextScene;
    public string levelPath;
    public List<dialogEvent> dialogList;


	// Use this for initialization
	//void Start () {
        //initStr();
	//}
	
    

  /*  void initStr()
    {
        nextScene = "MainScene";
        bookName.Add("a");
        bookName.Add("b");
        bookStr.Add("hahahaha");
        bookStr.Add("testtest");
    }*/
}
[System.Serializable]
public struct dialogText {
    public StoryCharacterInfo speaker;
    public string bookStr;
}

[System.Serializable]
public struct dialogEvent {
    public StoryCharacterInfo speaker;
    [TextArea]
    public string bookStr;
    public GameObject obj;
    public int eventCode;
    public AudioClip audio;
}
