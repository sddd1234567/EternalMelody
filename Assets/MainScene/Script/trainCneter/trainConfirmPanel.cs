using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class trainConfirmPanel : MonoBehaviour {

    public Skill befSkl,aftSkl;

    public Button yesBtn, noBtn;


    public Text descriptiontxt;
    public Text costTxt;
    public Image costImg;
    public int whoCalled;//是 1:更換視窗, 2:升級視窗

    public int befIndex;//上個skill的位置(用於換技能就是原本選取締幾個來交換，升級就紀錄該技能在list的位置)
    public GameObject confirmSnd,compositionSnd,changeSnd;
    // Use this for initialization
    void Start () {
        init();
	}
	
    public void init()
    {
        setAllTxt();
        yesBtn.onClick.AddListener(clickYesBtn);
        noBtn.onClick.AddListener(clickNoBtn);
    }

	public void setSkill(Skill bef,Skill aft)
    {
        befSkl = bef;
        aftSkl = aft;
    }
    public void setCostPart()
    {
        if (whoCalled == 1)//change
        {
            return;
        }
        costImg.overrideSprite = Resources.Load<UnityEngine.Sprite>("UI/skillPiece");
        
        //upgrade
        costTxt.text = "cost: " + befSkl.levelUpCost;
        
    }
    public void setDescriptionTxt()
    {
        descriptiontxt.text = aftSkl.totalDes;
    }
    public void setAllTxt()
    {
        setCostPart();
        setDescriptionTxt();
    }

    public void clickYesBtn()
    {
        switch(whoCalled)
        {
            case 1://chage
                Instantiate(changeSnd);
                Player.instance.changeSkill(befIndex, aftSkl);
                Destroy(gameObject);
                break;
            case 2://upgrade
                if (Player.instance.checkMoneyEnough(0, 0, befSkl.levelUpCost))
                {
                    Instantiate(compositionSnd);
                    Player.instance.upgradeSkill(befIndex,aftSkl);
                    Destroy(gameObject);
                }
                else
                {
                    Instantiate(confirmSnd);
                    costTxt.text = "餘額不足!";
                    costTxt.color = Color.red;
                }
                break;
            default:

                break;
        }
    }
    public void clickNoBtn()
    {
        Instantiate(confirmSnd);
        Destroy(gameObject);
    }

}
