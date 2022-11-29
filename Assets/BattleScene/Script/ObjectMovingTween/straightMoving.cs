using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class straightMoving : ObjectTween {
    public Rigidbody2D rig;
    public bool isAtk;
    public List<GameObject> targets;
    public int attackCount;
    public float startTime;
    public SpriteRenderer sprit;

    void Awake() {
        rig = GetComponent<Rigidbody2D>();
        rig.velocity = new Vector3(15, 0, 0);
        StartCoroutine(waitForStart());
    }

    void Start()
    {
        targets = new List<GameObject>();        
        StartCoroutine(waitForDestroy());
        attackCount = 0;        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void startMoving()
    {
        if (attacker != null)
        {
            transform.position = attacker.transform.position + Vector3.right * 2;
            transform.localScale = attacker.transform.localScale;            
        }
    }

    public IEnumerator waitForStart() {
        yield return null;
        float time = 0;
        while (time < startTime)
        {
            time += Time.deltaTime;
            yield return null;
        }
        startMoving();
        sprit.color = new Color(255, 255, 255, 255);
        
    }

    public void OnTriggerEnter2D(Collider2D col)
    {

        if (col.tag == targetTag)
        {
            isAtk = true;
            SpriteBattling tg = col.GetComponent<SpriteBattling>();
            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i].GetInstanceID() == col.gameObject.GetInstanceID())
                {
                   // Debug.Log("double");
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

    public IEnumerator waitForDestroy()
    {
        yield return new WaitForSeconds(1f);
        if (!isAtk)
            Debug.Log("wtf?");
        //Debug.Log(attackCount);
        Destroy(gameObject);
    }
}
