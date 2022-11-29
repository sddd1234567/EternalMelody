using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class skillAllList : ScriptableObject
{
    public  List<sklList> skillList;
    public int sklNum;



}

[System.Serializable]
public struct sklList
{
    public List<Skill> list;
}
