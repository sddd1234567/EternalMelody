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

    public int gold;
    public int skillChip;
    public int exp;
    

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
        doState();
        stateJudge();
    }

    void FixedUpdate()
    {
        
        smoothlyMove();
    }





    public override void loadInfo(Sprite sp)//進入戰鬥前，先LOAD
    {
        base.loadInfo(sp);
        Enemy e = sp as Enemy;
        skills = e.skills;
        attackSteps = e.attackStep;
        loadAnim();
    }

    public override float attack()
    {
        if (attackSE != null)
            playSE(attackSE);
        return base.attack();
    }

    public void loadAnimator() {
        animator = GetComponent<Animator>();
    }

    public override void hitted(float attack, float PEN = 0, float value = 1)
    {
        base.hitted(attack, PEN, value);
        knockBack();
    }

    public void knockBack() {
        if (state != 1 && state != 5)
        {
            StopCoroutine(knockBackComplete());
            changeState(5);
            animator.SetBool("KnockBack", true);
            targetPos = transform.position + Vector3.right * 1.2f;
            //transform.position = targetPos;
            isSmoothMoving = true;
            changeState(5);
        }
    }

    public void loadAnim() {

    }

    public void changeState(int st)
    {
       // if (state == 1)
          //  Debug.Log("atkcomplete");
        state = st;
    }

    public override void atkComplete()
    {
        base.atkComplete();
        animator.SetBool("isATK", false);
    }

    public void stopAnim(string animName) {
        animator.SetBool(animName, false);
    }


    public void smoothlyMove() {
        if (targetPos == transform.position || !isSmoothMoving || state == 1)
        {
            isSmoothMoving = false;
            return;
        }
            
         transform.position = Vector3.Lerp(transform.position, targetPos, 5f * Time.deltaTime);
       // transform.position = new Vector3(transform.position.x + 5f * Time.deltaTime, transform.position.y, transform.position.z);
        if (targetPos.x - transform.position.x <= 0.5f)
        {
            isSmoothMoving = false;
            transform.position = targetPos;
            StartCoroutine(knockBackComplete());
        }
    }

    public IEnumerator knockBackComplete() {
         yield return new WaitForSeconds(0.5f);
        if(state == 5)
            changeState(2);
    }

    public void stateJudge()
    {
        if (!isAttachPlayer && state == 2)
        {
            changeState(0);
        }
        else if (isAttachPlayer && state != 1 && state != 5)
        {
            changeState(2);
        }
        if (state == 5)
        {
            animator.SetBool("KnockBack", true);
        }
        else
        {
            animator.SetBool("KnockBack", false);
        }
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

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            isAttachPlayer = true;
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            isAttachPlayer = false;
        }
    }

    /*  public void OnTriggerEnter2D(Collider2D col)
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
      }*/

    public void stateChange() {
        if (state == 1)
        {
            nowAttackStep++;
        }
        else if (state == 4)
        {
            nowSkillSteps++;
        }
    }

    public void canHit() {

        float step = autoAttackStep;
        float range = step / 10f;
        float rndstep = 100 / UnityEngine.Random.Range(step - range, step + range);
        float rndNum = UnityEngine.Random.Range(0, 100);

        if (rndNum <= rndstep)//發動普攻
        {
            if (state != 2 && state != 5)//not idle
            {
                return;
            }
            else
            {
                if (canSkill())
                    return;

                animator.SetBool("isATK", true);
                changeState(1);
            }
        }
        
    }

    public bool canSkill() {
        float step = skillStep;
        float range = step / 10f;
        float rndstep = 100 / UnityEngine.Random.Range(step - range, step + range);
        float rndNum = UnityEngine.Random.Range(0, 6);
        if (rndNum >= rndstep)
        {
            changeState(4);
            nowSkill = skills[0];
            animator.SetBool("Skill1", true);
            return true;
        }
        return false;
    }

    public void attackFinish() {
        nowAttackStep = 0;
        changeState(2);
    }

    public void skillFinish() {
        nowAttackStep = 0;
        //  e.changeState(0);
    }

    public override void dead() {
        Destroy(hpBar.gameObject);
        Destroy(gameObject);
    }
}
