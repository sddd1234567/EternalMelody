using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour {

    void destroy() {
        BattleManager.instance.gameStart();
        Destroy(gameObject);
    }

}
