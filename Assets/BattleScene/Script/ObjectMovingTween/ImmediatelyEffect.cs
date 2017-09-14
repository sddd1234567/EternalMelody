using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmediatelyEffect : ObjectTween {
    // Use this for initialization

    public bool isAtk;
    public List<GameObject> targets;
    public int attackCount;
	void Start () {
        targets = new List<GameObject>();
        rig = GetComponent<Rigidbody2D>();
        StartCoroutine(waitForDestroy());
        attackCount = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (attacker != null)
            transform.position = attacker.transform.position;
        else
            Debug.Log("233");
    }

    public override void startMoving() {
        if (attacker != null)
        {
            transform.position = attacker.transform.position;
            transform.localScale = attacker.transform.localScale;
            GetComponent<BoxCollider2D>().offset = attacker.GetComponent<BoxCollider2D>().offset + Vector2.right * 2f;
            GetComponent<BoxCollider2D>().size = attacker.GetComponent<BoxCollider2D>().size;
        }
            
        else
            Debug.Log("233");
    }

    public void OnTriggerEnter2D(Collider2D col) { 

        if (col.tag == targetTag)
        {
            isAtk = true;
            SpriteBattling tg = col.GetComponent<SpriteBattling>();
            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i].GetInstanceID() == col.gameObject.GetInstanceID())
                {
                    Debug.Log("double");
                    return;
                }
                    
                else
                    continue;
            }
            attackCount++;
            targets.Add(col.gameObject);
            BattleHandler.instance.castEffect(attacker, tg, sectionEffect);
                
        }      
    }

    public IEnumerator waitForDestroy() {
        yield return new WaitForSeconds(0.15f);
        if (!isAtk)
            Debug.Log("wtf?");
        Debug.Log(attackCount);
        Destroy(gameObject);
    }
}
