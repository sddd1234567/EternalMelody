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
		
    }

    public void canHit()
    {
        for (int i = 0; i < enemies.Count; ++i)
        {
            EnemyBattling e = enemies[i] as EnemyBattling;
            float step = e.autoAttackStep;
            float range = step / 10f;
            float rndstep = 100/Random.Range(step - range, step + range);
            float rndNum = Random.Range(0, 100);
            if (rndNum <= rndstep)//發動普攻
            {
                if (e.state != 2 && e.state != 5)//not idle
                {
                    return;
                }
                else
                {
                    if (canSkill(e))
                        continue;

                    e.animator.SetBool("isATK", true);
                    e.changeState(1);
                }
            }
        }        
    }

    public void nowStepChange() {
        for (int i = 0; i < enemies.Count; ++i)
        {
            EnemyBattling e = enemies[i] as EnemyBattling;
            if (e.state == 1)
            {
                e.nowAttackStep++;
            }
            else if (e.state == 4)
            {
                e.nowSkillSteps++;
            }
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
                    e.nowAttackStep = 0;
                    e.changeState(2);
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
                BattleHandler.instance.castSkill("Skill1", e.nowSkill.sectionAction[e.nowSkillSteps], e, enemies, players, "Player");

                if (e.activeSkillSteps == e.nowSkillSteps)
                {
                    e.nowAttackStep = 0;
                    e.changeState(0);
                }
            }
        }
    }

    public bool canSkill(EnemyBattling e)
    {
        float step = e.skillStep;
        float range = step / 10f;
        float rndstep = 100/Random.Range(step - range, step + range);
        float rndNum = Random.Range(0, 6);
        if(rndNum>=rndstep)
        {
            e.changeState(4);
            e.nowSkill = e.skills[0];
            e.animator.SetBool("Skill1", true);
            return true;
        }
        return false;
    }

    public void loadSprites(List<SpriteBattling> enes,List<SpriteBattling> plys)
    {
        enemies = enes;
        players = plys;
    }

}
