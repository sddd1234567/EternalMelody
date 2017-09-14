using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : ScriptableObject{

    public int HP;
    public int ATK;
    public int DEF;
    public int PEN;
    public int CRI;

    public int weaponSpecies;//屬於哪種類型 劍 法 弓 配角(?)
    public string weaponName;

}
