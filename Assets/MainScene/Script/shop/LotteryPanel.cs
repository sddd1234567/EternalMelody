using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LotteryPanel : MonoBehaviour {

    public static LotteryPanel instance;

    public Button noBtn;
    public Button yesBtn;
    public Button confirmBtn;
    public Text descriptionTxt;
    public Text costTxt;
    public Image tempoStarImg;
    public Text getTxt;
    public Text nameTxt;
    public Image iconImg;

    public int whoCalled;//0:weapon, 1:skill
    public bool isConfirmState;//0:before lottery,1:result
    public const int WeaponLotteryCost=1;
    public const int skillLotteryCost = 1;

    public bool isValued;
    public GameObject lotterySnd, confirmSnd;
	// Use this for initialization
	void Start () {
        instance = this;
        StartCoroutine(init());
    }
    public void valued(int who)
    {
        whoCalled = who;
        isValued = true;
    }
	IEnumerator init()
    {
        while(isValued==false)
        {
            yield return null;
        }
        isConfirmState = false;
        setAllUIActive();
        yesBtn.onClick.AddListener(clickYesBtn);
        noBtn.onClick.AddListener(clickNoBtn);
        confirmBtn.onClick.AddListener(clickConfirmBtn);
        setCostTxt();
        setDescriptionTxt();
    }

    
    public void setBeforePanelIsActive(bool state)
    {
        descriptionTxt.enabled = state;
        tempoStarImg.enabled = state;
        costTxt.enabled = state;
        noBtn.gameObject.SetActive(state);
        yesBtn.gameObject.SetActive(state);
    }
    public void setConfirmPanelIsActive(bool state)
    {
        iconImg.enabled = state;
        getTxt.enabled = state;
        nameTxt.enabled = state;
        confirmBtn.gameObject.SetActive(state);
    }
    public void weaponLottery()
    {
        WeaponAllList wpAllList = Resources.Load<WeaponAllList>("Weapon/WeaponAllList");
        /*int getWpnum = UnityEngine.Random.Range(0, wpAllList.wpNum - 1);
        Weapon getWp = wpAllList.wplist[getWpnum].list[0];*/
        Weapon getWp = wpAllList.wplist[2].list[0];
        Player.instance.weaponLottery(getWp);
        iconImg.overrideSprite = Resources.Load<UnityEngine.Sprite>("Weapon/" + getWp.path + "/icon");
        nameTxt.text = getWp.weaponName;
    }
    public void skillLottery()
    {
        skillAllList sklList = Resources.Load<skillAllList>("Skill/SkillAllList");
        /*int getSklNum = Random.Range(0, sklList.sklNum - 1);
        Skill getskl = sklList.skillList[getSklNum].list[0];*/
        Skill getskl = sklList.skillList[1].list[0];
        Player.instance.skillLottery(getskl);
        iconImg.overrideSprite = Resources.Load<UnityEngine.Sprite>("Skill/" + getskl.skillPath + "/icon");
        nameTxt.text = getskl.skillName;
    }

    public void setAllUIActive()
    {
        setBeforePanelIsActive(!isConfirmState);
        setConfirmPanelIsActive(isConfirmState);
    }
    public void setCostNotEnough()
    {
        costTxt.text = "餘額不足!";
        costTxt.color = Color.red;
    }

    public void clickYesBtn()
    {

        if(whoCalled==0)//weapon
        {
            if(Player.instance.checkMoneyEnough(0,WeaponLotteryCost,0))
            {
                Instantiate(lotterySnd);
                isConfirmState = true;
                weaponLottery();
                setAllUIActive();

            }
            else
            {
                Instantiate(confirmSnd);
                setCostNotEnough();
            }

        }
        else//skill
        {
            if(Player.instance.checkMoneyEnough(0,skillLotteryCost,0))
            {
                Instantiate(lotterySnd);
                isConfirmState = true;
                skillLottery();
                setAllUIActive();
            }
            else
            {
                Instantiate(confirmSnd);
                setCostNotEnough();
            }
        }
    }

    public void clickNoBtn()
    {
        Instantiate(confirmSnd);
        Destroy(gameObject);
    }

    public void clickConfirmBtn()
    {
        Instantiate(confirmSnd);
        Destroy(gameObject);
    }

    public void setDescriptionTxt()
    {
        descriptionTxt.text = whoCalled == 0 ? "確定要抽武器嗎?" : "確定要抽技能嗎?";
    }
    public void setCostTxt()
    {
        costTxt.text = "cost: ";
        costTxt.text += whoCalled == 0 ? WeaponLotteryCost.ToString() : skillLotteryCost.ToString();
    }

}
