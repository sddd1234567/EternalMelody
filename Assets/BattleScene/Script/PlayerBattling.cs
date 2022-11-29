using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattling : SpriteBattling {
    // Use this for initialization
    public float blockRatio;
    public Buff comboBuff;
   // public GameObject attackSE;

    public int nowPlayerAttackState;

    public bool isBlock;

    public GameObject blockSE;

    public bool blockSuccess;
    public GameObject hittedSE;

    void Awake()
    {
        nowPlayerAttackState = 0;
        animator = GetComponent<Animator>();
        damageText = Resources.Load("UI/DamageText") as GameObject;
        damageBalance = 0.9f;
    }

    void Start() {        
        buffs = new List<Buff>();
        buffTime = new List<int>();
        skillTime = new int[3];
        skillTime[0] = 0;
        skillTime[1] = 0;
        skillTime[2] = 0;

        attackEffect = new effect();
        attackEffect.species = 3;
        attackEffect.value = 1;
        attackEffect.attackerEffectObj = Resources.Load("Skill/testImmediately") as GameObject;

        buffs.Add(comboBuff = new Buff(0, 1f, 1000));

        //  Anim = GetComponent<Animator>();

        //  animOverride = GetComponent<Animator>().runtimeAnimatorController as AnimatorOverrideController;
        //  animOverride["Attack"] = Resources.Load("Skill1") as AnimationClip;
    }

    // Update is called once per frame
    void Update() {
        
    }


    public override void loadInfo(Sprite sp)
    {
        base.loadInfo(sp);
        Player ply = sp as Player;
        CRI = ply.CRI;
        PEN = ply.PEN;
        skills = ply.skills;
        loadAnim(ply);
    }

    public override float attack()
    {
        //playSE(attackSE);
        return base.attack();
    }    

    public void loadAnim(Player ply) {
        //Debug.Log("override");
        AnimatorOverrideController animOverride = new AnimatorOverrideController();
        animOverride.runtimeAnimatorController = animator.runtimeAnimatorController;
        animOverride["Attack"] = Resources.Load("Weapon/" + ply.weapon.path + "/Attack") as AnimationClip;
        animOverride["Attack2"] = Resources.Load("Weapon/" + ply.weapon.path + "/Attack2") as AnimationClip;
        animOverride["Attack3"] = Resources.Load("Weapon/" + ply.weapon.path + "/Attack3") as AnimationClip;
        animOverride["Attack4"] = Resources.Load("Weapon/" + ply.weapon.path + "/Attack4") as AnimationClip;
        animOverride["Walk"] = Resources.Load("Weapon/" + ply.weapon.path + "/Walk") as AnimationClip;
        animOverride["Idle"] = Resources.Load("Weapon/" + ply.weapon.path + "/Idle") as AnimationClip;
        for (int i = 0; i < skills.Count; i++)
        {
            animOverride["Skill" + (i+1) + "_1"] = Resources.Load("Skill/" + skills[i].skillPath + "/AnimationClip/Skill1") as AnimationClip;
            animOverride["Skill" + (i+1) + "_2"] = Resources.Load("Skill/" + skills[i].skillPath + "/AnimationClip/Skill2") as AnimationClip;
        }
        animator.runtimeAnimatorController = animOverride;
    }
    

    public override void hitted(float attack, float PEN = 0, float value = 1)
    {
        base.hitted(attack, PEN, value);
        playSE(hittedSE);
    }

    public override void atkComplete()
    {
        base.atkComplete();
        nowPlayerAttackState = 0;
        animator.SetBool("isATK1", false);
        animator.SetBool("isATK2", false);
        animator.SetBool("isATK3", false);
        animator.SetBool("isATK4", false);
    }

    public void move(float value) {
        transform.position += new Vector3(value, 0, 0);
    }

    public override Vector3 getPosition()
    {
        return (transform.position + Vector3.right * 1f);
    }

    public void stopAnim(string animateName) {
        animator.SetBool(animateName, false);
    }

    public void stopSkillAnim() {
        animator.SetBool("Skill1_1", false);
        animator.SetBool("Skill1_2", false);
        animator.SetBool("Skill2_1", false);
        animator.SetBool("Skill2_2", false);
    }

    public override void dead()
    {
        base.dead();
        gameObject.SetActive(false);
    }
}
