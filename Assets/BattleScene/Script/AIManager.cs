using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour {
    private const float scale = 10;//間隔的range
    public List<SpriteBattling> enemies;
    public List<SpriteBattling> players;

    public static AIManager instance;
	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        isAttackFinish();
    }

    public void canHit()
    {
            for (int i = 0; i < enemies.Count; ++i)
            {
                EnemyBattling e = enemies[i] as EnemyBattling;

                e.canHit();
            }
    }

    public void nowStepChange() {
        for (int i = 0; i < enemies.Count; ++i)
        {
            EnemyBattling e = enemies[i] as EnemyBattling;
            e.stateChange();
        }
    }

    public void isAttackFinish() {
        for (int i = 0; i < enemies.Count; ++i)
        {
            EnemyBattling e = enemies[i] as EnemyBattling;
            if (e.state == 1)
            {
                if (e.nowAttackStep == e.attackSteps)
                {
                    BattleHandler.instance.spriteAttack(enemies[i], players[0], "Player");
                    e.attackFinish();
                }
            }
        }
    }

    public void isSkillFinish() {
        for (int i = 0; i < enemies.Count; ++i)
        {
            EnemyBattling e = enemies[i] as EnemyBattling;
            if (e.state == 4)
            {
                BattleHandler.instance.castSkill("Skill1", e.nowSkill.sectionAction[e.nowSkillSteps], e, enemies, players, "Player", false);

                if (e.activeSkillSteps == e.nowSkillSteps)
                {
                    e.skillFinish();
                }
            }
        }
    }

    public void loadSprites(List<SpriteBattling> enes,List<SpriteBattling> plys)
    {
        enemies = enes;
        players = plys;
    }

    public void enemyKnockBack(EnemyBattling enemy) {
        if (enemy.state != 1 && enemy.state != 5)
        {
            StopCoroutine(enemy.knockBackComplete());
            enemy.changeState(5);
            enemy.animator.SetBool("KnockBack", true);
            enemy.targetPos = enemy.transform.position + Vector3.right * 1.2f;
            //transform.position = targetPos;
            enemy.isSmoothMoving = true;
            enemy.changeState(5);
        }
    }


}
