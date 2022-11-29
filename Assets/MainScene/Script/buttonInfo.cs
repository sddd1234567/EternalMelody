using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//每顆按紐做的事
public class buttonInfo : MonoBehaviour {
    private Button btn;
    public int num;
    public GameObject completed;
	// Use this for initialization
	void Start () {
        //num = 0;
        btn = GetComponent<Button>();
        setButton();


        
    }
	public void setNum(int n)
    {
        //Debug.Log("gege" + n);
        num = n;
        Level lv = Resources.Load(levelsInfo.levels[n].path) as Level;

        if (Player.instance.levelIndexs[lv.levelIndex] == 1)
            completed.SetActive(true);
    }
    public void btnClick()
    {
        //Debug.Log(num);
        //Debug.Log(transform.localPosition);
        string path = levelsInfo.levels[num].path;
        missionController.setPath(path);
        buttonListContoller.instance.setSelect(gameObject);
        buttonListContoller.instance.info.SetActive(true);
        Level lv = Resources.Load(path) as Level;
        buttonListContoller.instance.setText(lv.aw.gold.ToString(), lv.aw.skillChip.ToString(), lv.aw.exp.ToString(), lv.aw.temperStar.ToString());
        
    }
    private void setButton()//控制大小
    {
        transform.SetAsFirstSibling();
        btn.onClick.AddListener(btnClick);
        GetComponent<RectTransform>().sizeDelta = new Vector2(50f, 12f);
        //Debug.Log(transform.localPosition);
        //btn.GetComponentInChildren<Text>().text = levelsInfo.levels[num].name;
    }
	// Update is called once per frame
	void Update () {
		
	}
}
