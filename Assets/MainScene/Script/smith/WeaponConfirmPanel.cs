using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WeaponConfirmPanel : MonoBehaviour {

    public Weapon beforeWp, afterWp;
    public WeaponChange wpChange;
    public Button yesBtn,noBtn;
    
    public Text befLvTxt,aftLvTxt;
    public Text befAtkTxt,aftAtkTxt;
    public Text befHpTxt,aftHpTxt;
    public Text befDefTxt,aftDefTxt;
    public Text befTxt1, aftTxt1;
    public Text befTxt2, aftTxt2;
    public Text costTxt;
    public Image costImg;
    public int whoCalled;//是 0:購買視窗，還是 1:更換視窗, 2:升級視窗

    public GameObject purchaseSnd, confirmSnd, compositionSnd,changeSnd;

    public void setWeapon(Weapon bef,Weapon aft)
    {
        beforeWp = bef;
        afterWp = aft;
    }

     public void init()
    {
        wpChange = new WeaponChange(beforeWp, afterWp);
        setAllTxt();
        yesBtn.onClick.AddListener(clickYesBtn);
        noBtn.onClick.AddListener(clickNoBtn);
    }
    public void setAllTxt()
    {
        setBefTxt();
        setAftTxt();
        setLeftTxt();
    }
    public void setBefTxt()
    {
        befLvTxt.text = "LV: " + wpChange.lv;
        befHpTxt.text = "HP: " + wpChange.hp;
        befAtkTxt.text = "ATK: " + wpChange.atk;
        befDefTxt.text = "DEF: " + wpChange.def;
    }
    public void setAftTxt()
    {
        setOneAftTxt(wpChange.lvPlus, aftLvTxt);
        setOneAftTxt(wpChange.hpPlus, aftHpTxt);
        setOneAftTxt(wpChange.atkPlus, aftAtkTxt);
        setOneAftTxt(wpChange.defPlus, aftDefTxt);
        setCostPart();
    }
    public void setOneAftTxt(int num,Text t)
    {
        if (num == 0)
            return;
        else if(num>0)
        {
            t.color = Color.red;
            t.text = "↑" + num;
        }
        else
        {
            t.color = Color.blue;
            t.text = "↓" + num;
        }
    }
    public void setLeftTxt()//設定cri pen txt
    {
        if(wpChange.criPlus!=0 || wpChange.cri!=0)
        {
            befTxt1.text = "CRI: " + wpChange.cri;
            setOneAftTxt(wpChange.criPlus, aftTxt1);
            if(wpChange.penPlus != 0 || wpChange.pen != 0)
            {
                befTxt2.text = "PEN: " + wpChange.pen;
                setOneAftTxt(wpChange.penPlus, aftTxt2);
            }
        }
        else if(wpChange.penPlus != 0 || wpChange.pen != 0)
        {
            befTxt1.text = "PEN: " + wpChange.pen;
            setOneAftTxt(wpChange.penPlus, aftTxt1);
        }
    }
    public void setCostPart()
    {
        if(whoCalled==1)//change
        {
            return;
        }
        costImg.overrideSprite = Resources.Load<UnityEngine.Sprite>("UI/coin");
        if(whoCalled==0)//購買
        {
            costTxt.text = "cost: " + afterWp.buyCost;
        }
        else//升級
        {
            costTxt.text = "cost: " + beforeWp.levelUpCost;
        }
    }


    void Start () {
        init();
	}
	
	

    void clickYesBtn()
    {
        if(whoCalled==0)//購買
        {
            if(Player.instance.checkMoneyEnough(afterWp.buyCost,0,0))
            {
                Instantiate(purchaseSnd);
                Player.instance.purchaseWeapon(afterWp);
                Destroy(gameObject);
            }
            else
            {
                Instantiate(confirmSnd);
                costTxt.text = "餘額不足!";
                costTxt.color = Color.red;
            }
        }
        else if(whoCalled==1)//更換
        {
            Instantiate(changeSnd);
            Player.instance.changeWeapon(afterWp);
            Destroy(gameObject);
        }
        else//upgrade
        {
            if (Player.instance.checkMoneyEnough(afterWp.levelUpCost, 0, 0))
            {
                Instantiate(compositionSnd);
                Player.instance.upgradeWeapon(beforeWp, afterWp);
                Destroy(gameObject);
            }
            else
            {
                Instantiate(confirmSnd);
                costTxt.text = "餘額不足!";
                costTxt.color = Color.red;
            }
            
        }

        
    }
    void clickNoBtn()
    {
        Instantiate(confirmSnd);
        Destroy(gameObject);
    }
}

public class WeaponChange
{
    public Weapon befWp, aftWp;
    public  WeaponChange(Weapon bef,Weapon aft)
    {
        befWp = bef;
        aftWp = aft;
        init();
    }
    public int lv,lvPlus;
    public int atk, atkPlus;
    public int hp, hpPlus;
    public int def, defPlus;
    public int cri, criPlus;
    public int pen, penPlus;
    
    public void init()
    {
        lv = aftWp.LV;
        atk = aftWp.ATK;
        hp = aftWp.HP;
        def = aftWp.DEF;
        cri = aftWp.CRI;
        pen = aftWp.PEN;

        lvPlus = lv - befWp.LV;
        hpPlus = hp - befWp.HP;
        atkPlus = atk - befWp.ATK;
        defPlus = def - befWp.DEF;
        criPlus = cri - befWp.CRI;
        penPlus = pen - befWp.PEN;
        
    }

}

