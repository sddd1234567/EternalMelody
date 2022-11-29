using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {
    public PlayerBattling playerBattling;

    public List<GameObject> attachEnemy;

    public int nowState;    // 0 = walk  1 = readyToAttack  2 = attacking    3 = cant control

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
      /*  if (nowState != 2)
        {
            if (lastState != nowState)
            {
                Debug.Log("changeState");
                doState();
                lastState = nowState;
            }
              
        }*/
	}

   /* public IEnumerator rushToEnemy(float x) {
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
    }*/

    public void doState() {
        if (nowState == 0)
        {
            playerBattling.walk(5.5f);
            animator.SetBool("isWalk", true);
        }

        else if (nowState == 1)
        {
            playerBattling.walk(0f);
            animator.SetBool("isWalk", false);
        }
    }


    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            stateChange(1);
            attachEnemy.Add(col.gameObject);
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            attachEnemy.Remove(col.gameObject);
        }
    }

    public void stateChange(int state)
    {
        nowState = state;
    }

    public void attackMove() {
        // GameObject obj = new GameObject("ATKParaent");
        //    transform.SetParent(obj.transform);
        if (attachEnemy.Count == 0)
        {
            //transform.position += Vector3.right * 1.5f;
            StartCoroutine(smoothlyMoving(transform.position.x + 1.2f));
        }

        else
            StartCoroutine(smoothlyMoving(transform.position.x + 0.1f));
        //transform.position += Vector3.right * 0.1f;
        // StartCoroutine(move(transform.position.x + 1));
    }

    IEnumerator smoothlyMoving(float targetX) {
        for (; transform.position.x < targetX; transform.position += Vector3.right * 15f * Time.deltaTime)
        {
            yield return null;
        }
        if (transform.position.x > targetX)
        {
            transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
        }
    }

}
