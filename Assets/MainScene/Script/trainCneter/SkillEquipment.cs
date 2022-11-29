using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkillEquipment : MonoBehaviour {
    public int whoCalled;//是 0:購買視窗，還是 1:更換視窗, 2:升級視窗
    public int befIndex;//如果是更換，才會有值
    public int num;//自己是第幾個skill
    public Skill skl;

    public Text confrimBtnTxt;
    public Button confirmBtn;
    public Text descriptionTxt;
    public Image iconImg;

    public GameObject canvas;
    public GameObject confirmPanel;
    public Vector2 midddleCanvas;
    // Use this for initialization
    public GameObject confirmSnd;

    void Start () {
        init();
	}
    public void init()
    {
        //whoCalled = 1;
        setDescriptionTxt();
        iconImg.overrideSprite = Resources.Load<UnityEngine.Sprite>("Skill/"+skl.skillPath + "/icon");
        setBtn();
        confirmBtn.onClick.AddListener(clickConfirmBtn);
        midddleCanvas = new Vector2(200, 150);
    }

    public void setDescriptionTxt()
    {
        descriptionTxt.text = skl.skillName+"LV."+skl.LV;
    }

    public void setBtn()
    {

         if (whoCalled == 1)//change
        {
            if (Player.instance.checkIsEquiped(skl))
            {
                confrimBtnTxt.text = "已裝備";
                confirmBtn.enabled = false;
                confirmBtn.image.overrideSprite = Resources.Load<UnityEngine.Sprite>("UI/confrimed");
            }
            else
            {
                confrimBtnTxt.text = "更換";
                confirmBtn.enabled = true;
                confirmBtn.image.overrideSprite = Resources.Load<UnityEngine.Sprite>("UI/confriming");
            }
        }
        else//upgrade
        {
            
            if (skl.levelUpCost == -1)
            {
                confrimBtnTxt.text = "MAX";
                confirmBtn.enabled = false;
                confirmBtn.image.overrideSprite = Resources.Load<UnityEngine.Sprite>("UI/confrimed");
            }
            else
            {
                confrimBtnTxt.text = "升級";
                confirmBtn.enabled = true;
                confirmBtn.image.overrideSprite = Resources.Load<UnityEngine.Sprite>("UI/confriming");
            }
        }
    }

    public void clickConfirmBtn()
    {
        Instantiate(confirmSnd);
        Skill befSkl;
        Skill aftSkl;
        int index;
        if(whoCalled==1)//change
        {
            befSkl = skl;
            aftSkl = skl;
            index = befIndex;
        }
        else//upgrade
        {
            index = num;

            befSkl = skl;
            skillAllList sklList = Resources.Load<skillAllList>("Skill/SkillAllList");
            aftSkl =sklList.skillList[num].list[skl.LV];
        }
        GameObject confirmP = Instantiate(confirmPanel, canvas.transform);
        RectTransform rec = confirmP.GetComponent<RectTransform>();
        rec.localPosition = Vector3.zero;
        rec.localScale = Vector3.one * 0.8f;
        confirmP.GetComponent<trainConfirmPanel>().befIndex = index;
        confirmP.GetComponent<trainConfirmPanel>().setSkill(befSkl,aftSkl);
        confirmP.GetComponent<trainConfirmPanel>().whoCalled = whoCalled;
    }
    public void refresh(Skill s)
    {
        skl = s;
        setBtn();
        setDescriptionTxt();
    }
}
