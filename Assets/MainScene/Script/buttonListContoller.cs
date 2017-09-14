using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonListContoller : MonoBehaviour {
    public GameObject originBtn;//要被複製的
    public List<Button> btns;
    public GameObject superObj;
    public List<leveSimpleInfo> lsi;

    public Vector3 zeroPos;
    public Vector3 offset;
	// Use this for initialization

	void Start () {
        zeroPos = Vector3.zero + new Vector3(-20, -10, 0);
        lsi = levelsInfo.levels;
        btns = new List<Button>();
        offset = new Vector3(0, -15f, 0);
        createBtns();
	}
    
   public void createBtns()
    {
        int size = lsi.Count;
        for(int i=0;i<size;++i)
        {
            GameObject btn = Instantiate(originBtn, superObj.transform);
            RectTransform rect = btn.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(30f, 12f);
            rect.localScale = Vector3.one;
            //rect.localPosition = new Vector3(-20, -10 - 15*i, 0);
            rect.localPosition = zeroPos + offset * i;
            btn.name += i;
            btn.GetComponentInChildren<Text>().text = lsi[i].name;

            btn.GetComponent<buttonInfo>().setNum(i);

        }
    }	

}
