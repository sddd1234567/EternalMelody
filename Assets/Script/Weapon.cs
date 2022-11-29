using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon : ScriptableObject{
    
    public int LV;
    public int HP;
    public int ATK;
    public int DEF;
    public int PEN;
    public int CRI;

    public int levelUpCost;
    public int buyCost;

    public int weaponSpecies;//屬於哪種類型 劍 法 弓 配角(?)
    public string weaponName;
    public string path;
    public bool isEquiped;

    //public string iconPath;
    public string description;
    /*public static bool operator ==(Weapon w1,Weapon w2)
    {

        return w1.weaponName == w2.weaponName && w1.LV == w2.LV;
    }
    public static bool operator !=(Weapon w1, Weapon w2)
    {

        return w1.weaponName != w2.weaponName || w1.LV != w2.LV;
    }*/
    public bool equ(Weapon w2)
    {
        return weaponName == w2.weaponName && LV == w2.LV;
    }

    public Weapon()
    {
        LV = 0;
        ATK = 0;
        HP = 0;
        CRI = 0;
        PEN = 0;
        DEF = 0;
        levelUpCost = 0;
        buyCost = 0;
        weaponName = "nil";
    }
    public Weapon(int lv,int upcost,int buy,string name,string p,string des)
    {
        LV = lv;
        levelUpCost = upcost;
        buyCost = buy;
        weaponName = name;
        path = p;
        description = des;
    }
}

[System.Serializable]
public class WeaponSaveData {
    public string weaponName;
    public int LV;

    public WeaponSaveData(string name, int lvv){
        weaponName = name;
        LV = lvv;
    }
}
