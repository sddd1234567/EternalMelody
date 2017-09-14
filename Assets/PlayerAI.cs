using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : MonoBehaviour {
    public PlayerBattling playerBattling;

    public List<GameObject> attachEnemy;

    public int nowState;    // 0 = walk  1 = readyToAttack  2 = attacking    3 = cant control  4 = rushing

    public bool canAttack;

    public int lastState = -1;

    public Animator animator;

    void Awake() {
        lastState = -1;
        animator = GetComponent<Animator>();
        playerBattling = GetComponent<PlayerBattling>();
    }

	// Use this for initialization
	void Start () {
        attachEnemy = new List<GameObject>();
        nowState = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (nowState != 2)
        {
            attackStateJudge();
            if (lastState != nowState)
            {
                Debug.Log("changeState");
                doState();
                lastState = nowState;
            }
              
        }
	}

    public IEnumerator rushToEnemy(float x) {
        stateChange(4);
        playerBattling.walk(30f);
        while (attachEnemy.Count == 0)
        {
            if (x - transform.position.x <= 0.3)
                break;
            yield return null;
        }
        stateChange(1);
        playerBattling.walk(0f);
    }

    public void doState() {
        if (nowState == 0)
        {
            playerBattling.walk(5f);
            animator.SetBool("isWalk", true);
        }

        else if (nowState == 1)
        {
            playerBattling.walk(0f);
            animator.SetBool("isWalk", false);
        }
    }

    public void attackStateJudge() {
        if (attachEnemy.Count == 0 && nowState != 4)
        {
            stateChange(0);
        }
        else if (attachEnemy.Count > 0)
        {
            stateChange(1);
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            attachEnemy.Add(col.gameObject);
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            attachEnemy.Remove(col.gameObject);
        }
    }

    public void stateChange(int state)
    {
        nowState = state;
    }


}
