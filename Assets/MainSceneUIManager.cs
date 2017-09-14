using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSceneUIManager : MonoBehaviour {
    // Use this for initialization
    public Text level;
    public Text gold;
    public Text temperStar;

	void Start () {
        if (Player.instance != null)
        {
            loadPlayerInfo(Player.instance);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void loadPlayerInfo(Player player) {
        level.text = "Lv：" + player.level;
        gold.text = "金幣：" + player.gold;
        temperStar.text = "音律之星：" + player.temperStar;
    }


    public void onLevelSelectedButtonClicked() {
        SceneManager.LoadSceneAsync(4, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(4));
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
    }
}
