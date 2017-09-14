using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCount : UIScript {    

    public void resumeBattle()
    {
        BattleManager.instance.resumeButton();
    }
}
