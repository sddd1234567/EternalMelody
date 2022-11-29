using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponEquipment : MonoBehaviour {

    public int whoCalled;//是 0:購買視窗，還是 1:更換視窗, 2:升級視窗

    public Text confrimBtnTxt;
    public Button confirmBtn;
    public Text descriptionTxt;
    public Image iconImg;

    public GameObject canvas;
    public GameObject confirmPanel;
    //public string iconPath;
    //public string descriptionStr;
    public Weapon wp;

    public Vector2 midddleCanvas;
    public GameObject confirmSnd;
	// Use this for initialization
	void Start () {
        init();
	}
	public void init()
    {
        //whoCalled = 1;
        setDescriptionTxt();
        iconImg.overrideSprite = Resources.Load<UnityEngine.Sprite>("Weapon/"+wp.path+"/icon");
        setBtn();
        confirmBtn.onClick.AddListener(clickConfirmBtn);
        midddleCanvas = new Vector2(200, 150);
    }

    public void setBtn()
    {
        
        if(whoCalled==0)
        {
            confrimBtnTxt.text = "購買";
            confirmBtn.enabled = true;
            confirmBtn.image.overrideSprite = Resources.Load<UnityEngine.Sprite>("UI/confriming");
        }
        else if(whoCalled==1)//change
        {
            if(Player.instance.weapon==wp)
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
           // Debug.Log("cost"+wp.levelUpCost);
            if(wp.levelUpCost==-1)
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
        Weapon bef;
        Weapon aft;
        if(whoCalled==0)//buy
        {
            bef = Player.instance.weapon;
            aft = wp;
        }
        else if(whoCalled==1)//change
        {
            bef = Player.instance.weapon;
            aft = wp;
        }
        else //upgrade
        {
            bef = wp;
            WeaponAllList wpallList = Resources.Load<WeaponAllList>("Weapon/WeaponAllList");
            
            aft = wpallList.findNextGradeWeapon(wp);
        }
        GameObject confirmP = Instantiate(confirmPanel, canvas.transform);
        RectTransform rec = confirmP.GetComponent<RectTransform>();
        rec.localPosition = Vector3.zero;
        rec.localScale = Vector3.one * 0.8f;
        confirmP.GetComponent<WeaponConfirmPanel>().setWeapon(bef, aft);
        confirmP.GetComponent<WeaponConfirmPanel>().whoCalled = whoCalled;
    }
    public void setDescriptionTxt()
    {
        descriptionTxt.text = wp.weaponName+":"+wp.description;
    }
    public void refresh(Weapon w)
    {
        wp = w;
        setBtn();
        setDescriptionTxt();
    }

}
