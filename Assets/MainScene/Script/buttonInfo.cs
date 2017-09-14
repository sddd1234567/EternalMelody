using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonInfo : MonoBehaviour {
    private Button btn;
    public int num;
	// Use this for initialization
	void Start () {
        //num = 0;
        btn = GetComponent<Button>();
        setButton();
    }
	public void setNum(int n)
    {
        Debug.Log("gege" + n);
        num = n;
    }
    public void btnClick()
    {
        Debug.Log(num);
        string path = levelsInfo.levels[num].path;
        missionController.setPath(path);
    }
    private void setButton()
    {
        btn.onClick.AddListener(btnClick);
        GetComponent<RectTransform>().sizeDelta = new Vector2(30f, 12f);
        //btn.GetComponentInChildren<Text>().text = levelsInfo.levels[num].name;
    }
	// Update is called once per frame
	void Update () {
		
	}
}
