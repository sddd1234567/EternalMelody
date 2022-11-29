using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : UIScript{
    public RectTransform tr;
    public SpriteBattling hittedObj;

    public Font chineseFont;
    void Start()
    {
        tr = GetComponent<RectTransform>();
    }

    void Update()
    {
        //tr.position += 2f * Vector3.up * Time.deltaTime;
    }

    public void removeNowText() {
        if(hittedObj.damageTextCount >= 3)
            hittedObj.damageTextCount=0;
    }

    public void setChineseFont() {

    }

    public void setDamageText(string value) {
        Text text = GetComponent<Text>();
        text.text = value;
    }
}
