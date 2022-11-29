using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSaveData : MonoBehaviour {
    public SaveData savee;
    public Player player;


    //  public Weapon Sword1;
    void Start() {
       // PlayerPrefs.DeleteAll();
        savee = new SaveData();
            if (PlayerPrefs.HasKey("SaveData1"))
            {
                startGame();
            }
            else
            {            
                firstSave();
            }        
    }

    public void firstSave()
    {
        savee.level = 1;  // original setting
        //savee.level = 2;  // ¸õ¹L±Ð¾Ç
        savee.exp = 100;
        savee.nowChapter = 0;
        savee.weapon = new WeaponSaveData("Branch",1);
        weaponList wp = new weaponList();
        wp.str.Add(new WeaponSaveData("Branch",1));
        savee.weaponList = JsonUtility.ToJson(wp);

        jsonIntList st = new jsonIntList();
        st.str.Add(0);
        st.str.Add(0);
        st.str.Add(0);
        st.str.Add(0);
        st.str.Add(0);
        savee.levelIndex = JsonUtility.ToJson(st);

        skillList nowSkill = new skillList();
        nowSkill.str.Add(new SkillSaveData("BigCut", 1));
        nowSkill.str.Add(new SkillSaveData("none", 1));
        nowSkill.str.Add(new SkillSaveData("none", 1));
        savee.nowSkill = JsonUtility.ToJson(nowSkill);

        //Debug.Log(savee.weapon.weaponName);
        skillList strr = new skillList();
        strr.str.Add(new SkillSaveData("BigCut", 1));
        savee.skillList = JsonUtility.ToJson(strr);
        //Debug.Log(savee.skillList);
        PlayerPrefs.SetString("SaveData1", JsonUtility.ToJson(savee));
        PlayerPrefs.Save();
        firstStartGame();
    }

    public void firstStartGame()
    {
        Load();
        SceneManager.LoadScene("Start");
    }

    public static void Save() {
        SaveData save = new SaveData();
        save.exp = Player.instance.nowEXP;
        save.gold = Player.instance.gold;
        save.level = Player.instance.level;
        save.skillChip = Player.instance.skillChip;
        save.temperStar = Player.instance.temperStar;
        save.nowChapter = Player.instance.nowChapter;
        jsonIntList st = new jsonIntList();
        st.str = Player.instance.levelIndexs;
        save.levelIndex = JsonUtility.ToJson(st);


        skillList strr = new skillList();
        for (int i = 0; i < Player.instance.skills.Count; i++)
        {
            strr.str.Add(new SkillSaveData(Player.instance.skills[i].skillPath, Player.instance.skills[i].LV));
        }
        save.nowSkill = JsonUtility.ToJson(strr);



        skillList str2 = new skillList();
        for (int i = 0; i < Player.instance.skillList.Count; i++)
        {
            str2.str.Add(new SkillSaveData(Player.instance.skillList[i].skillPath, Player.instance.skillList[i].LV));
        }
        save.skillList = JsonUtility.ToJson(str2);
        

        



        save.weapon = new WeaponSaveData(Player.instance.weapon.path, Player.instance.weapon.LV);

        weaponList wp = new weaponList();
        for (int i = 0; i < Player.instance.weaponList.Count; i++) {
            wp.str.Add(new WeaponSaveData(Player.instance.weaponList[i].path, Player.instance.weapon.LV));
        }
        save.weaponList = JsonUtility.ToJson(wp);
        

        PlayerPrefs.SetString("SaveData1", JsonUtility.ToJson(save));
        PlayerPrefs.Save();
    }

    public List<WeaponSaveData> weaponToWeaponSaveData(List<Weapon> wp) {
        List<WeaponSaveData> wsd = new List<WeaponSaveData>();
        for (int i = 0; i < wp.Count; i++)
        {
            wsd.Add(new WeaponSaveData(wp[i].path, wp[i].LV));
        }
        return wsd;
    }

    public static void Load()
    {
        SaveData load = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString("SaveData1"));
        loadPlayerInfo(Player.instance,load);
        //Debug.Log(load.weapon.weaponName);
        //Debug.Log(load.nowSkill);
    }

    public void startGame() {
        Load();
        SceneManager.LoadScene("MainScene");
    }

    [System.Serializable]
    public class SaveData{
        public int level;
        public int exp;
        public WeaponSaveData weapon;
        public int nowChapter;
        public int gold;
        public int temperStar;
        public int skillChip;
        public string weaponList;
        public string skillList;
        public string nowSkill;
        public string levelIndex;
        public int storyState;
        
    }

    public static void loadPlayerInfo(Player player, SaveData data)
    {
        player.skills = new List<Skill>();
        player.skillList = new List<Skill>();
        player.weaponList = new List<Weapon>();
        player.level = data.level;
        player.HPBeforeEquiped = (data.level-1) * 200 + 800;
        player.ATKBeforeEquiped = (data.level-1) * 20 + 50;
        player.DEFBeforeEquiped = (data.level-1) * 20 + 60;
        player.CRIBeforeEquiped = 0;
        player.PENBeforeEquiped = 0;
        player.gold = data.gold;
        player.skillChip = data.skillChip;
        if (data.levelIndex != null)
            player.levelIndexs = JsonUtility.FromJson<jsonIntList>(data.levelIndex).str;
        else
        {
            player.levelIndexs = new List<int>();
            player.levelIndexs.Add(0);
            player.levelIndexs.Add(0);
            player.levelIndexs.Add(0);
            player.levelIndexs.Add(0);
            player.levelIndexs.Add(0);
        }

        player.temperStar = data.temperStar;
        player.weapon = Resources.Load("Weapon/" + data.weapon.weaponName + "/" + data.weapon.weaponName + " " + data.weapon.LV) as Weapon;
        player.weapon.LV = data.weapon.LV;
        player.nowChapter = data.nowChapter;
        player.storyState = data.storyState;
        //Debug.Log(data.skillList);
        skillList skillListt = JsonUtility.FromJson<skillList>(data.skillList);
        for (int i = 0; i < skillListt.str.Count; i++) {
            player.skillList.Add(Resources.Load("Skill/" + skillListt.str[i].skillPath + "/" + skillListt.str[i].skillPath + " " + skillListt.str[i].LV) as Skill);
        }

        skillList skills = JsonUtility.FromJson<skillList>(data.nowSkill);
        for (int i = 0; i < skills.str.Count; i++)
        {
            //Debug.Log("Skill/" + skills.str[i].skillPath + "/" + skills.str[i].skillPath + " " + skills.str[i].LV);
            Skill sk = Resources.Load("Skill/" + skills.str[i].skillPath + "/" + skills.str[i].skillPath + " " + skills.str[i].LV) as Skill;
            //Debug.Log(sk.skillName);
            player.skills.Add(sk);
        }

        weaponList wp = JsonUtility.FromJson<weaponList>(data.weaponList);
        for (int i = 0; i < wp.str.Count; i++)
        {
            Weapon wpp = Resources.Load("Weapon/" + wp.str[i].weaponName + "/" + wp.str[i].weaponName + " " + wp.str[i].LV) as Weapon;
            wpp.LV = wp.str[i].LV;
            player.weaponList.Add(wpp);
        }

        player.weapon.isEquiped = true;
        for (int i = 0; i < 3; ++i)
        {
            player.skills[i].isEquiped = true;
        }

        

        player.requiredEXP = new List<int>();
        requireList(player);
    }

    public static void requireList(Player player) {
        player.requiredEXP.Add(500);
        player.requiredEXP.Add(1000);
        player.requiredEXP.Add(1700);
        player.requiredEXP.Add(2700);
        player.requiredEXP.Add(3900);
        player.requiredEXP.Add(5300);
        player.requiredEXP.Add(6900);
        player.requiredEXP.Add(8700);
        player.requiredEXP.Add(10700);
        player.requiredEXP.Add(12900);
    }

}

[System.Serializable]
class jsonList {
    public List<string> str = new List<string>();
}

[System.Serializable]
class jsonIntList {
    public List<int> str = new List<int>(); 
}

[System.Serializable]
class weaponList {
    public List<WeaponSaveData> str = new List<WeaponSaveData>();
}

[System.Serializable]
class skillList{
    public List<SkillSaveData> str = new List<SkillSaveData>();
}

[System.Serializable]
class SkillSaveData
{
    public string skillPath;
    public int LV;
    public SkillSaveData(string path, int level) {
        skillPath = path;
        LV = level;
    }
}

[System.Serializable]

class jsonBoolList {
    public List<bool> str = new List<bool>();
}
