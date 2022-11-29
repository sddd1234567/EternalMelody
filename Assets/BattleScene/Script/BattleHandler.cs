using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour {
    public static BattleHandler instance;

    // Use this for initialization
    void Awake() {
        instance = this;
    }

    public void spriteAttack(SpriteBattling attacker,SpriteBattling target, string targetTag) {
        //if (targetTag == "Player")
            //Debug.Log("enemyAtk");
        createObjectAnimate(attacker.attackEffect, attacker, target, targetTag);
    }

    public void castSkill(string animName, sectionAction sectionAct, SpriteBattling attacker, List<SpriteBattling> attackerList, List<SpriteBattling> targetList, string targetTag, bool isBonus)
    {
        for (int i = 0; i < sectionAct.sectionEffect.Count; i++)
        {
            effect e = sectionAct.sectionEffect[i];
            if (isBonus)
            {
                e.value *= e.extraBonus;
                Debug.Log(e.value);
            }
                createObjectAnimate(e, attacker, targetList[0], targetTag);
        }
    }
    public Buff addBuff(int species,SpriteBattling target, float value, int buffDuration) {
        Buff buff = new Buff(species, value, buffDuration);
        target.buffs.Add(buff);
        target.addBuffEffect(species, value);
        target.buffTime.Add(buffDuration);
        return buff;
    }

    public void doDamage(SpriteBattling attacker, SpriteBattling target, float value=1) {
        float attack = attacker.attack();
        float criChance = Random.Range(0, 100);
        float balance = attacker.damageBalance;
        int CRIDmg = criChance < attacker.CRI? 2 : 1 ;// < 代表爆擊

        value *= Random.Range(balance, 1);
       // Debug.Log(attack);
        target.hitted(attack,attacker.PEN,value * CRIDmg);
    }

    public void buffDurationTiming(SpriteBattling target) {  //計時用，每個拍點呼叫一次，減少剩餘時間1
        buffDurationControl(target, -1);
    }

    public void buffDurationControl(SpriteBattling target, int change) {    //用來控制Buff的持續時間
        if (target.buffTime != null)
        {
            for (int i = 0; i < target.buffTime.Count; i++) {
                target.buffTime[i] += change;
                if (target.buffTime[i] <= 0) {
                    target.buffTime.Remove(target.buffTime[i]);
                    removeBuff(target, target.buffs[i]);
                }
            }
        }
    }

    public void coolDownTiming(SpriteBattling target) {
        coolDownControl(target, -1);
    }

    public void coolDownControl(SpriteBattling target,int change) {
        for (int i = 0; i < target.skillTime.Length; i++) {
            if (target.skillTime[i] > 0)
            {
                target.skillTime[i] += change;
            }
        }   
    }

    public void removeBuff(SpriteBattling target,Buff buff) {
        target.buffs.Remove(buff);
        target.removeBuffEffect(buff.species,buff.numerical);
    }

    

    public void createObjectAnimate(effect sectionEffect, SpriteBattling attacker, SpriteBattling target, string targetTag)
    {
        if (targetTag == "Enemy" && sectionEffect.species <= 2)
            targetTag = "Player";

        GameObject obj = Instantiate(sectionEffect.attackerEffectObj);
        ObjectTween tween = obj.GetComponent<ObjectTween>();        

        tween.targetTag = targetTag;
        tween.attacker = attacker;
        tween.target = target;
        tween.sectionEffect = sectionEffect;
        tween.startMoving();
    }

    public void health(SpriteBattling target, float value)
    {
        target.nowHP += value;
        if (target.nowHP > target.maxHP)
            target.nowHP = target.maxHP;
        target.createHealthCreate(((int)value).ToString());
    }

    public void castEffect(SpriteBattling attacker, SpriteBattling target, effect sectionEffect)
    {
        if (sectionEffect.species <= 1)
        {
            addBuff(sectionEffect.species, target, sectionEffect.value, sectionEffect.buffDuration);
        }

        else if (sectionEffect.species == 2)
        {
            float a = target.maxHP * sectionEffect.value;
            health(target, a);
        }

        else if (sectionEffect.species == 3)
        {
            if (target.tag == "Enemy")
            {
                AIManager.instance.enemyKnockBack(target as EnemyBattling);
            }
            doDamage(attacker, target, sectionEffect.value);
        }

    }
}
