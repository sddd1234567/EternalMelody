using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSceneUIManager : MonoBehaviour {
    // Use this for initialization
    public Text gold;
    public Text skillChip;
    public Text temperStar;
    public static MainSceneUIManager instance;
    public GameObject teleportSnd;
    void Awake() {
        instance = this;
    }

	void Start () {
        //chapterCheck();
        
        if (Player.instance != null)
        {
            loadPlayerInfo(Player.instance);
        }
	}
	
	// Update is called once per frame
	void Update () {
        gold.text = Player.instance.gold.ToString();
        skillChip.text = Player.instance.skillChip.ToString();
        temperStar.text = Player.instance.temperStar.ToString();
	}

    public void chapterCheck() {
        int i = 0;
        for (i = 0; i < Player.instance.levelIndexs.Count; i++)
        {
            if (Player.instance.levelIndexs[i] == 0)
                break;
        }
        if (i == 0)
        {
            LoadScene.targetScene = "Intro1";
            SceneManager.LoadScene("loadingScene");
        }
        else if (i == 1)
        {
            LoadScene.targetScene = "Chapter1-1";
            SceneManager.LoadScene("loadingScene");
        }
        else if (i == 2)
        {
            if (mainSceneTtutorial.instance == null)
            {
                LoadScene.targetScene = "MainSceneTutorial";
                SceneManager.LoadScene("loadingScene");
            }

        }
    }

    public void loadPlayerInfo(Player player) {
        gold.text = player.gold.ToString();
        skillChip.text = player.skillChip.ToString();
        temperStar.text = player.temperStar.ToString();
    }


    public void onLevelSelectedButtonClicked() {
        Instantiate(teleportSnd);
        SceneManager.LoadSceneAsync("LevelSelect", LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("LevelSelect"));
        //Debug.Log(SceneManager.GetActiveScene().buildIndex);
    }
}
