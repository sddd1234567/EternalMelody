using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HotelPanel : MonoBehaviour {
    public static HotelPanel instance;

    public Button cancelBtn;
    public Button changeWeaponBtn;
    public Button changeSkillBtn;
    public Text hpTxt,hpPlusTxt;
    public Text atkTxt, atkPlusTxt;
    public Text defTxt, defPlusTxt;
    public Text criTxt, criPlusTxt;
    public Text penTxt, penPlusTxt;
    public Text lvTxt;
    public Slider EXPBar;
    public Image weaponImg;
    public List<Image> skillImg;
    public List<Image> skillBackImg;
    public Image playerImg;
    public Player plr;

    public List<Button> sklBtns;

    public GameObject confirmSnd;

    public int nowSelcetSkl;
    // Use this for initialization
    void Start () {
        instance = this;
        nowSelcetSkl = -1;
        StartCoroutine( init());
	}
    IEnumerator  init()
    {
        while(Player.instance==null)
        {
            yield return null;
        }
        plr = Player.instance;
        setBefTxt();
        setExpBar();
        setPlusTxt();
        setImg();
        cancelBtn.onClick.AddListener(clickCancelBtn);
        sklBtns[0].onClick.AddListener(clickSkl1);
        sklBtns[1].onClick.AddListener(clickSkl2);
        sklBtns[2].onClick.AddListener(clickSkl3);
        changeWeaponBtn.onClick.AddListener(clickChangeWeaponBtn);
        changeSkillBtn.onClick.AddListener(clickChangeSkillBtn);
    }
	public void setBefTxt()
    {
        lvTxt.text = "LV: " + plr.level;
        hpTxt.text = "HP: "+plr.HP;
        atkTxt.text = "ATK: " + plr.ATK;
        defTxt.text = "DEF: " + plr.DEF;
        criTxt.text = "CRI: " + plr.CRI+"%";
        penTxt.text = "PEN: " + plr.PEN+"%";
    }
    public void setPlusTxt()
    {
        hpPlusTxt.text = "(+" + plr.weapon.HP + ")";
        atkPlusTxt.text = "(+" + plr.weapon.ATK + ")";
        defPlusTxt.text = "(+" + plr.weapon.DEF + ")";
        criPlusTxt.text = "(+" + plr.weapon.CRI + ")";
        penPlusTxt.text = "(+" + plr.weapon.PEN + ")";
    }

    public void setExpBar()
    {
        EXPBar.value = (float)plr.nowEXP / plr.requiredEXP[plr.level];
    }
    public void clickCancelBtn()
    {
        SceneManager.UnloadSceneAsync("HotelScene");
    }
	public void setImg()
    {
        //playerImg.overrideSprite
        playerImg.overrideSprite = Resources.Load<UnityEngine.Sprite>("Weapon/" + plr.weapon.path + "/PlayerIcon/plrIcon");

        weaponImg.overrideSprite = Resources.Load<UnityEngine.Sprite>("Weapon/" + plr.weapon.path + "/icon");
        skillImg[0].overrideSprite = Resources.Load<UnityEngine.Sprite>("Skill/" + plr.skills[0].skillPath + "/icon");
        skillImg[1].overrideSprite = Resources.Load<UnityEngine.Sprite>("Skill/" + plr.skills[1].skillPath + "/icon");
        skillImg[2].overrideSprite = Resources.Load<UnityEngine.Sprite>("Skill/" + plr.skills[2].skillPath + "/icon");

    }

    public void clickSkl1()
    {
        Instantiate(confirmSnd);
        if (nowSelcetSkl!=-1)
        skillBackImg[nowSelcetSkl].color = new Vector4(1, 1, 1, 1);
        nowSelcetSkl = 0;
        skillBackImg[nowSelcetSkl].color = new Vector4(0.9f, 0.9f, 0.9f ,1);
    }
    public void clickSkl2()
    {
        Instantiate(confirmSnd);
        if (nowSelcetSkl != -1)
            skillBackImg[nowSelcetSkl].color = new Vector4(1, 1, 1, 1);
        nowSelcetSkl = 1;
        skillBackImg[nowSelcetSkl].color = new Vector4( 0.7f, 0.7f, 0.7f,1);
    }
    public void clickSkl3()
    {
        Instantiate(confirmSnd);
        if (nowSelcetSkl != -1)
            skillBackImg[nowSelcetSkl].color = new Vector4(1, 1, 1, 1);
        nowSelcetSkl = 2;
        skillBackImg[nowSelcetSkl].color = new Vector4(0.7f, 0.7f, 0.7f, 1);
    }

    public void clickChangeWeaponBtn()
    {
        Instantiate(confirmSnd);
        SceneManager.LoadSceneAsync("SmithScene", LoadSceneMode.Additive);
        StartCoroutine(giveWeaponValue());

    }
    IEnumerator giveWeaponValue()
    {
        while(WeaponPanel.instance==null)
        {
            yield return null;
        }
        WeaponPanel.instance.outsideGiveValue(1);
    }

    public void clickChangeSkillBtn()
    {
        Instantiate(confirmSnd);
        if (nowSelcetSkl==-1)
        {
            return;
        }
        SceneManager.LoadSceneAsync("TrainCenterScene", LoadSceneMode.Additive);
        StartCoroutine(giveSkillValue());

    }
    IEnumerator giveSkillValue()
    {
        while (SkillPanel.instance == null)
        {
            yield return null;
        }
        SkillPanel.instance.setValue(1,nowSelcetSkl);
    }

    public void refreshWeapon()
    {
        playerImg.overrideSprite = Resources.Load<UnityEngine.Sprite>("Weapon/" + plr.weapon.path + "/PlayerIcon/plrIcon");
        weaponImg.overrideSprite = Resources.Load<UnityEngine.Sprite>("Weapon/" + plr.weapon.path + "/icon");
    }

    public void refrshSkill()
    {
        skillImg[0].overrideSprite = Resources.Load<UnityEngine.Sprite>("Skill/" + plr.skills[0].skillPath + "/icon");
        skillImg[1].overrideSprite = Resources.Load<UnityEngine.Sprite>("Skill/" + plr.skills[1].skillPath + "/icon");
        skillImg[2].overrideSprite = Resources.Load<UnityEngine.Sprite>("Skill/" + plr.skills[2].skillPath + "/icon");

    }
}
