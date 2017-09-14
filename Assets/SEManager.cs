using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour {
    // Use this for initialization
    public static SEManager instance;
    public GameObject playerAttackSE;
    public GameObject skillStartSE;

    void Awake() {
        instance = this;
    }

    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public IEnumerator playSE(GameObject se) {
        Instantiate(se);
        yield return se;
    }

    public void loadPlayerSE(PlayerBattling player) {
        playerAttackSE = player.attackSE;
    }

    public void playerAttack() {
        StartCoroutine(playSE(playerAttackSE));
    }

    public void startSkill() {
        StartCoroutine(playSE(skillStartSE));
    }
}
