using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour {
    public List<GameObject> tutorialWindows;
    public bool isWindowsOpen;
    public int nowWindow;
    public static TutorialController instance;
    public Text confirmText;

    void Awake() {
        instance = this;
    }

    // Use this for initialization
    void Start() {

        nowWindow = 0;
        BattleManager.instance.isGameStop = true;
    }

    void Update() {
        battleTutorial();
        
    }

    public void battleTutorial() {
        if (BattleManager.instance != null && MusicHandler.instance != null && UIManager.instance != null)
        {
            if (isWindowsOpen)
            {
                if (MusicHandler.instance.waitCount <= 6)
                    MusicHandler.instance.waitTiming(5);
                BattleManager.instance.isGameStop = true;
            }
            if (BattleManager.instance.isGameStop)
            {
                if (!isWindowsOpen)
                {
                    if (BattleManager.instance.nowWave == nowWindow)
                    {
                        if (tutorialWindows.Count > nowWindow)
                        {
                            nowWindow++;
                            //Debug.Log("tuuuuuuuuuu");
                            StartCoroutine(createWindow());
                            isWindowsOpen = true;
                        }
                    }
                    else
                    {
                        //BattleManager.instance.isGameStop = false;
                    }
                }
                else
                {
                    if (MusicHandler.instance.waitCount <= 6 && nowWindow > 0)
                        MusicHandler.instance.waitTiming(5);
                }
            }
        }
    }

    public void closeWindow() {
        isWindowsOpen = false;
    }

    public IEnumerator createWindow() {
        yield return new WaitForSeconds(1f);
       // Debug.Log("tutorial"+(nowWindow - 1));
        tutorialWindows[nowWindow - 1].SetActive(true);
    }

    public void loadingScene(string nextScene)
    {
        LoadScene.targetScene = nextScene;
        SceneManager.LoadScene("loadingScene");
    }
}
