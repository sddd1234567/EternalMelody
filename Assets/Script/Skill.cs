using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : ScriptableObject
{
    public string skillName;    
    public List<sectionAction> sectionAction;
    public int coolDown;//冷卻
    public int weaponSpecies;
    public int combo;  //技能接完可以加多少combo
}


[System.Serializable]
public struct effect {
    public int species;      //ATKBuff=0 DEFBuff=1 Health=2 damage=3 NONE=10
    public float value;
    public int buffDuration;
    //public int target;      //me=0  opposite=1  全隊=2  allenemy=3  all=4

    public GameObject attackerEffectObj;
    public GameObject targetEffectObj;
    public bool isAOE;
}

[System.Serializable]
public struct sectionAction {
    public int gesture;//手勢     1:點擊   2:壓住    3:左滑   4:右滑   5:上滑   6:下滑  7.繼續壓住  8:放開時機
    public AnimationClip sectionAnim;
    public List<effect> sectionEffect;    
}


