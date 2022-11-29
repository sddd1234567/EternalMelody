using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour {
    // Use this for initialization
    public static SEManager instance;
    public GameObject playerAttackSE;
    public GameObject skillStartSE;
    public GameObject winningSE;
    public GameObject defeatSE;

    void Awake() {
        instance = this;
    }

    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public IEnumerator playSE(GameObject se) {
        yield return null;
        Instantiate(se);
        
    }

    public void loadPlayerSE(PlayerBattling player) {
        playerAttackSE = player.attackSE;
    }

    public void playerAttack() {
       // StartCoroutine(playSE(playerAttackSE));
    }

    public void startSkill() {
        StartCoroutine(playSE(skillStartSE));
    }
}
