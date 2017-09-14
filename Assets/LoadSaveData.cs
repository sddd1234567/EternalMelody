using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSaveData : MonoBehaviour {
    public SaveData save;
    public Player player;
    public Weapon Sword1;
  //  public Weapon Sword1;
    public SaveData load;
    void Start() {
        save = new SaveData();
        load = new SaveData();
       // Load();
     //   SceneManager.LoadSceneAsync(0);
        Sword1 = Resources.Load("Weapon/Sword01") as Weapon;
    }

    public void Save() {
        save.level = 1;
        save.exp = 100;
        save.nowChapter = 0;
        save.gold = 1000;
        save.temperStar = 1087;
        save.weapon = Sword1;
        PlayerPrefs.SetString("SaveData1", JsonUtility.ToJson(save));
        PlayerPrefs.Save();
    }

    public void Load()
    {
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("SaveData1"), load);
        loadPlayerInfo(player,load);
        Debug.Log(load.weapon.weaponName);
        SceneManager.LoadSceneAsync(2);
    }

    [System.Serializable]
    public class SaveData{
        public int level;
        public int exp;
        public Weapon weapon;
        public int nowChapter;
        public int gold;
        public int temperStar;
        public string weaponList;
    }

    public void loadPlayerInfo(Player player, SaveData data)
    {
        player.level = data.level;
        player.HPBeforeEquiped = data.level * 100;
        player.ATKBeforeEquiped = data.level * 10;
        player.DEFBeforeEquiped = data.level * 10;
        player.CRIBeforeEquiped = 0;
        player.PENBeforeEquiped = 0;
        player.gold = data.gold;
        player.temperStar = data.temperStar;
        player.equip(data.weapon);
    }
}
