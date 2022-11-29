using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteBattling : MonoBehaviour {
    public float CRI;
    public float maxHP;
    public float nowHP;
    public float ATK;
    public float DEF;
    public float PEN;
    public float damageBalance;

    public GameObject attackSE;

    public List<Buff> buffs;
    public List<int> buffTime;
    public List<Skill> skills;
    public int[] skillTime;

    public Slider hpBar;
    public HealthBarUI hpBarController;

    public effect attackEffect;

    public Animator animator;

    public bool isWalk;

    public Vector3 nowDestination;

    public GameObject damageText;

    public int damageTextCount;

    // Use this for initialization
    void Start () {
        damageBalance = 0.7f;
	}
	
	// Update is called once per frame
	void Update () {
        hpBar.value = nowHP / maxHP;
	}

    public virtual float attack()
    {        
        return ATK;
    }

    public void startAnim(string animName) {
        animator.SetBool(animName, true);
    }

    public virtual void hitted(float attack, float PEN=0, float value = 1)
    {
        float dmg = Mathf.Round((attack - DEF * (100 - PEN)/100) * value);
        nowHP -= dmg > 0 ? dmg : 1;
        damgeTextCreate((dmg > 0 ? dmg : 1).ToString());
        hpBarController.updateValue(nowHP / maxHP);
    }

    public bool CheckisDead()
    {
        return nowHP <= 0;
    }

    public virtual void loadInfo(Sprite sp)//進入戰鬥前，先LOAD
    {
        maxHP = sp.HP;
        nowHP = sp.HP;
        ATK = sp.ATK;
        DEF = sp.DEF;
        CRI = sp.CRI;
    }

    public void addBuffEffect(int species, float value)//上buff
    {
        if (species == 0)   //ATKBuff or ATKDebuff
        {
            ATK = (int)(ATK * value);
        }
        else if (species == 1)
        {
            DEF = (int)(DEF * value);
        }
    }

    public void removeBuffEffect(int species,float value) {
        if (species == 0)   //ATKBuff or ATKDebuff
        {
            ATK = (int)(ATK / value);
        }
        else if (species == 1)
        {
            DEF = (int)(DEF / value);
        }
    }

    //need animation 
    public virtual void dead()
    {
        
    }

    public void doAnimate(string animName) {
        animator.SetBool(animName, true);
    }

    public void walk(float speed)
    {        
        gameObject.GetComponent<Rigidbody2D>().velocity = (new Vector2 (speed, 0f));
        isWalk = true;
        hpBarController.gameObject.SetActive(true);
    }

    public DamageText damgeTextCreate(string value) {
        damageTextCount++;
        GameObject obj = Instantiate(damageText,mainSceneCameraControl.battlingUICanvas.transform);
        DamageText dmgScript = obj.transform.GetComponent<DamageText>();
        if(tag == "Player")
            dmgScript.GetComponent<Text>().color = Color.red;
        dmgScript.hittedObj = this;
        dmgScript.setPosition(transform.position + (Vector3.up * 1f) + (Vector3.up * (damageTextCount-1) * 0.5f) + new Vector3(Random.Range(-0.005f, 0.005f), 0,0));
        dmgScript.setDamageText(value);
        return dmgScript;
    }

    public DamageText missTextCreate(string value)
    {
        damageTextCount++;
        GameObject obj = Instantiate(damageText, mainSceneCameraControl.battlingUICanvas.transform);
        DamageText dmgScript = obj.transform.GetComponent<DamageText>();
        dmgScript.GetComponent<Text>().color = Color.gray;
        dmgScript.hittedObj = this;
        dmgScript.setPosition(transform.position + (Vector3.up * 1f) + (Vector3.up * (damageTextCount - 1) * 0.5f) + new Vector3(Random.Range(-0.005f, 0.005f), 0, 0) + Vector3.right * 3f);
        dmgScript.setDamageText(value);
        return dmgScript;
    }

    public DamageText createHealthCreate(string value)
    {
        damageTextCount++;
        GameObject obj = Instantiate(damageText, mainSceneCameraControl.battlingUICanvas.transform);
        DamageText dmgScript = obj.transform.GetComponent<DamageText>();
        dmgScript.GetComponent<Text>().color = Color.green;
        dmgScript.hittedObj = this;
        dmgScript.setPosition(transform.position + (Vector3.up * 1f) + (Vector3.up * (damageTextCount - 1) * 0.5f) + new Vector3(Random.Range(-0.005f, 0.005f), 0, 0));
        dmgScript.setDamageText(value);
        return dmgScript;
    }

    public virtual void atkComplete()
    {
        
    }

    public virtual Vector3 getPosition() {
        return transform.position;
    }

    public void playSE(GameObject obj)
    {
        Instantiate(obj);
    }
}
