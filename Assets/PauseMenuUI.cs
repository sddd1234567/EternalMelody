using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour {

    public void resumeGame() {
        BattleManager.instance.resume(gameObject);
    }

    public void backToLobby() {
        Time.timeScale = 1;
        UIManager.instance.backToLobby();
    }
}
