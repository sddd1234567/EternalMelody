using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {
    public static Player instance;      //遊戲裡永遠只會有一個Player，遊戲開始實建立實體供其他Script讀取

    //public int CRI;//critical
    public int PEN;
    public List<Skill> skills;
    public Weapon weapon;
    public int gold;
    public int temperStar;
    public int skillChip;
    public List<Weapon> weaponList;
    public List<Skill> skillList;
    public int nowChapter;

    public List<int> levelIndexs;//劇情相關

    public int storyState;

    public const int weaponLotteryCost = 1, skillLotteryCost = 1;

    public int HPBeforeEquiped;
    public int ATKBeforeEquiped;
    public int DEFBeforeEquiped;
    public int CRIBeforeEquiped;
    public int PENBeforeEquiped;
    void Awake() {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;            
        }
    }


    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
   

    public override void LevelUp()
    {
        base.LevelUp();
        HPUp();
        ATKUp();
        DEFUp();
        CRIUp();
        PENUp();
    }
    
    private void HPUp()//not yet
    {

    }
    private void ATKUp()//not yet
    {

    }
    private void DEFUp()//not yet
    {

    }
    private void PENUp()//not yet
    {

    }
    private void CRIUp()//not yet
    {

    }

    public void equip(Weapon wp)
    {
        HP =wp.HP+ HPBeforeEquiped ;
        ATK =wp.ATK + ATKBeforeEquiped;
        DEF =wp.DEF +DEFBeforeEquiped;
        CRI = wp.CRI+ CRIBeforeEquiped;
        PEN = wp.PEN + PENBeforeEquiped;
    }


    public bool checkMoneyEnough(int cash,int tempoStar,int skillPiece)
    {
        return gold >= cash && temperStar >= tempoStar && skillChip >= skillPiece;
    }

    public void changeWeapon(Weapon wp)
    {
        //old unequip
        weapon.isEquiped = false;


        weapon = wp;
        weapon.isEquiped = true;
        LoadSaveData.Save() ;
        WeaponPanel.instance.refreshAllObj();
    }
    public void purchaseWeapon(Weapon wp)
    {
        gold -= wp.buyCost;
        weaponList.Add(wp);
        LoadSaveData.Save();
    }
    public void upgradeWeapon(Weapon bef,Weapon aft)
    {
        if(bef.isEquiped)
        {
            weapon = aft;
        }
        gold -= bef.levelUpCost;
        for(int i=0;i<weaponList.Count;++i)
        {
            if(bef==weaponList[i])//名字相同，等級相同，升級上去就可以
            {
                weaponList[i] = aft;
                break;
            }
        }
        
        LoadSaveData.Save();
        WeaponPanel.instance.refreshAllObj();
    }

    public void changeSkill(int n,Skill s)
    {
        if(skills[n]!=null)
        skills[n].isEquiped = false;

        s.isEquiped = true;
        skills[n] = s;
        LoadSaveData.Save();
        SkillPanel.instance.refreshAllObj();
    }
    public void updateNowSkill(Skill s)
    {
        for(int i=0;i<3;++i)
        {
            if(skills[i].skillName==s.skillName)
            {
                skills[i] = s;
                break;
            }
        }
    }
    public void upgradeSkill(int n,Skill s)
    {
        

        skillChip -= skillList[n].levelUpCost;
        if(skillList[n].isEquiped)
        {

        }

        skillList[n] = s;
        LoadSaveData.Save();
        SkillPanel.instance.refreshAllObj();
    }

    public bool checkIsEquiped(Skill s)
    {
        return s.skillName == skills[0].skillName || s.skillName == skills[1].skillName || s.skillName == skills[2].skillName;
    }
    public void weaponLottery(Weapon w)
    {
        temperStar -= weaponLotteryCost;
        weaponList.Add(w);
        LoadSaveData.Save();
    }
    public void skillLottery(Skill s)
    {
        temperStar -= skillLotteryCost;
        skillList.Add(s);
        LoadSaveData.Save();
    }
}
