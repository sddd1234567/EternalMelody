using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class WeaponAllList : ScriptableObject {
    [SerializeField]
    public List<wpList> wplist;
    public int wpNum;//武器總數量
                     // Use this for initialization


    public Weapon findNextGradeWeapon(Weapon w)
    {
        Weapon nil = ScriptableObject.CreateInstance<Weapon>();
        nil.name = "nil";
        if(w.levelUpCost==-1)
        {
            return nil;
        }
        for (int i=0;i<wpNum;++i)
        {
            if(wplist[i].list[0].weaponName==w.weaponName)
            {
                    return wplist[i].list[w.LV];
            }
            
        }
        return nil;        
    }
}

[System.Serializable]
public struct wpList {
    public List<Weapon> list;
}
