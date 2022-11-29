using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour {
    public static PlayerStateManager instance;
    public PlayerBattling player;
    public PlayerState playerState;
    // Use this for initialization

    //state 0=walking   1=idle   2=attacking   3=can't control

    void Awake() {
        instance = this;
    }

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (player != null)
        {
            playerStateJudge();
            doState();
        }
	}

    public void playerStateJudge() {
        if (!playerState.canAttack)
        {
            playerState.stateChange(0);
        }
    }

    public void doState()
    {
        if (playerState.nowState == 0)
        {
            if(playerState.nowState != 2)
                player.walk(5.5f);
            playerState.animator.SetBool("isWalk", true);
        }

        else if (playerState.nowState == 1)
        {
            player.walk(0f);
            playerState.animator.SetBool("isWalk", false);
        }
    }
}
