using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattling : SpriteBattling {

    public int autoAttackStep;// 普攻間隔

    public int skillStep;//技能間隔  單位:普攻(非間隔)

    public int state; //現在狀態  0=walking,1=attacking,2=idle,3=dead 4=skill 5=hitted

    public Skill nowSkill;

    public int activeSkillSteps;

    public int nowSkillSteps;

    public int attackSteps;

    public int nowAttackStep;

    public bool isAttachPlayer;

    public Vector3 targetPos;

    public bool isSmoothMoving;
    

    void Start () {
        state = 2;
        buffs = new List<Buff>();
        buffTime = new List<int>();
        skillTime = new int[3];
        skillTime[0] = 0;
        skillTime[1] = 0;
        skillTime[2] = 0;
        damageText = Resources.Load("UI/DamageText") as GameObject;
    }
	
	// Update is called once per frame
	void Update () {
        attackStateJudge();
        doState();
        smoothlyMove();
    }

    public void doState()
    {
        if (state == 0)
        {
            walk(-5f);
            animator.SetBool("isWalk", true);
        }

        else if (state == 2)
        {
            walk(0f);
            animator.SetBool("isWalk", false);
        }
        else
        {
            walk(0f);
        }
    }

    public void attackStateJudge()
    {
        if (isAttachPlayer == false && state != 5 && state != 1)
        {
            changeState(0);
        }
        else if (state != 1 && state != 5)
        {
            changeState(2);
        }
    }

    public override void loadInfo(Sprite sp)//進入戰鬥前，先LOAD
    {
        base.loadInfo(sp);
        Enemy e = sp as Enemy;
        skills = e.skills;
        attackSteps = e.attackStep;
        loadAnim();
    }

    public void loadAnimator() {
        animator = GetComponent<Animator>();
    }

    public override void hitted(float attack, float PEN = 0, float value = 1)
    {
        base.hitted(attack, PEN, value);
        if (state != 1)
        {
            targetPos = transform.position + Vector3.right * 3;
            isSmoothMoving = true;
            changeState(5);
        }
    }

    public void loadAnim() {

    }

    public void changeState(int st)
    {
        state = st;
    }

    public override void atkComplete()
    {
        base.atkComplete();
        animator.SetBool("isATK", false);
    }


    public void smoothlyMove() {
        if (targetPos == transform.position || !isSmoothMoving || state == 1)
            return;
        transform.position = Vector3.Lerp(transform.position, targetPos, 5f * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPos) <= 0.5f)
        {
            isSmoothMoving = false;
            changeState(2);
            transform.position = targetPos;
        }
            
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            isAttachPlayer = true;
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            isAttachPlayer = false;
        }
    }

}
