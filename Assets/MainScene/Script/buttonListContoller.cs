using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//init buttons
public class buttonListContoller : MonoBehaviour {
    public GameObject originBtn;//要被複製的
    public List<Image> btns;
    public GameObject superObj;
    public List<leveSimpleInfo> lsi;

    public Vector3 zeroPos;
    public Vector3 offset;
    public static buttonListContoller instance;
    public GameObject info;



    public Text gold;
    public Text skillChip;
    public Text exp;
    public Text temperStar;

	// Use this for initialization

	void Start () {
        instance = this;
        zeroPos = new Vector3(-28.6f, -10, 0);
        lsi = levelsInfo.levels;
        btns = new List<Image>();
        offset = new Vector3(0, -15f, 0);
        createBtns();
	}
    
   public void createBtns()
    {
        int size = lsi.Count;
        for(int i=0;i<size;++i)
        {
            GameObject btn = Instantiate(originBtn, superObj.transform);
            btns.Add(btn.GetComponent<Image>());
            RectTransform rect = btn.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(50f, 12f);
            rect.localScale = Vector3.one;
            rect.localPosition = zeroPos + offset * i;
            btn.name += i;
            btn.GetComponentInChildren<Text>().text = lsi[i].name;
            btn.GetComponent<buttonInfo>().setNum(i);
        }
    }

    public void setSelect(GameObject obj) {
        for (int i = 0; i < btns.Count; i++) {
            btns[i].color = new Color(1, 1, 1, 1);
        }
        obj.GetComponent<Image>().color = new Color(0.4353f,0.4353f , 0.4353f, 0.8f);
    }

    public void setText(string g, string sc, string expp, string temper) {
        gold.text = "金幣 x " + g;
        skillChip.text = "技能碎片 x " + sc;
        exp.text = "經驗值 " + expp;
        temperStar.text = "音律之星 x " + temper;
    }
}
