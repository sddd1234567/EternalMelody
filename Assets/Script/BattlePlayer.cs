using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class BattlePlayer : Player {

    private int nowHP;
    private Player player;
    private BattleManager battleManager;
	// Use this for initialization
	void Start () {
        HP = player.HP;
        ATK = player.ATK;
        CRI = player.CRI;
        DEF = player.DEF;
        PEN = player.PEN;
        skills = player.skills;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override int attack()
    {
        return base.attack();
    }

    public void castSkill(int skillNum) {
        for (int i = 0; i < skills[skillNum].skillEffect.Length; i++) {//判斷技能效果種類
            if (skills[skillNum].skillEffect[i].species == 0)   //ATKBuff
            {
                battleManager.ATKBuff(battleManager.Player, skills[skillNum].skillEffect[i].value);
            }

             else if (skills[skillNum].skillEffect[i].species == 5)  //Damage
            {
                battleManager.doDamage(battleManager.Enemy, skills[skillNum].skillEffect[i].value);
            }
        }
    }
}
*/