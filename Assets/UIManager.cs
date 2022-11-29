using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    
    public GameObject startCountObj;
    public GameObject canvas;

    public GameObject waveText;

    public GameObject waveClear;

    public static UIManager instance;
    public static int i;

    //public TextMeshProUGUI comboText;
    //public TextMeshProUGUI combo;

    public Text comboText;
    //public Text combo;

    public GameObject gesture_Point;
    public GameObject gesture_Right;
    public GameObject gesture_Left;
    public GameObject gesture_Up;
    public GameObject gesture_Down;

    public RectTransform promptArea;

    public int nowGesture;

    public Skill nowSkill;

    public GameObject shine;

    public List<GameObject> nowPromptObj;

    public GameObject pauseUI;

    public Image shining;
    public int lastCombo;
    public Animator comboAnim;

    public Image[] coolDownUI;
    public Text[] coolDownText;

    public List<Image> skillIcons;
    // Use this for initialization

    void Awake() {
        instance = this;
        nowPromptObj = new List<GameObject>();
    }

	void Start () {
        i = 5;
        startCountObj = Resources.Load("UI/BattleCount") as GameObject;
        waveText = Resources.Load("UI/WaveText") as GameObject;
        waveClear = Resources.Load("UI/WaveClear") as GameObject;
    }

    // Update is called once per frame
    void Update() {
        setComboText();
    }

    public void loadIcon(List<Skill> sk) {
        for (int i = 0; i < sk.Count; i++)
        {
            if (!string.IsNullOrEmpty(sk[i].skillPath))
                skillIcons[i].sprite = (Resources.Load("Skill/" + sk[i].skillPath + "/Icon") as GameObject).GetComponent<SpriteRenderer>().sprite;
            else
                skillIcons[i].color = new Color(255, 255, 255, 0);
        }
    }



    public void setComboText() {
        if (BattleManager.instance.Combo == lastCombo)
        {
            return;
        }

        if (BattleManager.instance.Combo != 0)
        {
            //comboText.SetText(BattleManager.instance.Combo.ToString());
            //combo.SetText("Combo!!");
            comboText.text = BattleManager.instance.Combo.ToString();
            //combo.text = "Combo!!";
        }
        else
        {
            //comboText.SetText("");
            //combo.SetText("");
            comboText.text = "";
            //combo.text = "";
        }

        lastCombo = BattleManager.instance.Combo;
    }

    public void waveCleared() {
        Instantiate(waveClear, canvas.transform);
    }

    public void startCount() {
        GameObject obj = Instantiate(startCountObj,canvas.transform);
        RectTransform Rect = obj.GetComponent<RectTransform>();
        Rect.localPosition = new Vector3(0f, 37f, 0f);
        //Rect.localScale = new Vector3(1.5f,1.5f,1.5f);
        obj.GetComponent<Animator>().speed = 1/MusicHandler.instance.BPM;
    }

    public void createWaveText() {
        GameObject obj = Instantiate(waveText, canvas.transform);
        //RectTransform rect = obj.GetComponent<RectTransform>();
        obj.GetComponent<WaveTextAnimation>().setText(BattleManager.instance.nowWave + 1);
        //rect.localScale = Vector3.one;
        //rect.localPosition = new Vector3(0, 10, 0);
        //rect.sizeDelta = new Vector2(42, 13);
        BattleManager.instance.enemyWalkIn();
    }

    public void gesturePrompt() {
        for (int i = 0; i < nowSkill.sectionAction.Count; i++)
        {
            nowGesture = nowSkill.sectionAction[i].gesture;

            if (nowGesture == 1)
            {
                float baseX = (-(nowSkill.sectionAction.Count - 1) / 2f) * 25f;
                GameObject nowPrompt = Instantiate(gesture_Point, promptArea.transform);
                nowPromptObj.Add(nowPrompt);
                UIScript us = nowPrompt.GetComponent<UIScript>();
                us.setScale(Vector3.one * 1.5f);
                RectTransform re = us.GetComponent<RectTransform>();
                re.anchoredPosition = new Vector3(baseX + (25f * i), 10, 0);
                re.sizeDelta = (new Vector2(15f, 15f));
            }
            else if (nowGesture == 3)
            {
                float baseX = (-(nowSkill.sectionAction.Count - 1) / 2f) * 25f;
                GameObject nowPrompt = Instantiate(gesture_Left, promptArea.transform);
                nowPrompt.transform.SetAsLastSibling();
                nowPromptObj.Add(nowPrompt);
                UIScript us = nowPrompt.GetComponent<UIScript>();
                us.setScale(Vector3.one * 1.5f);
                RectTransform re = us.GetComponent<RectTransform>();
                re.anchoredPosition = new Vector3(baseX + (25f * i), 10, 0);
                re.sizeDelta = (new Vector2(15f, 15f));
            }

            else if (nowGesture == 4)
            {
                float baseX = (-(nowSkill.sectionAction.Count - 1) / 2f) * 25f;
                GameObject nowPrompt = Instantiate(gesture_Right, promptArea.transform);
                nowPrompt.transform.SetAsLastSibling();
                nowPromptObj.Add(nowPrompt);
                UIScript us = nowPrompt.GetComponent<UIScript>();
                us.setScale(Vector3.one * 1.5f);
                RectTransform re = us.GetComponent<RectTransform>();
                re.anchoredPosition = new Vector3(baseX + (25f * i), 10, 0);
                re.sizeDelta = (new Vector2(15f, 15f));
            }
            else if (nowGesture == 5)
            {
                float baseX = (-(nowSkill.sectionAction.Count - 1) / 2f) * 25f;
                GameObject nowPrompt = Instantiate(gesture_Up, promptArea.transform);
                nowPrompt.transform.SetAsLastSibling();
                nowPromptObj.Add(nowPrompt);
                UIScript us = nowPrompt.GetComponent<UIScript>();
                us.setScale(Vector3.one * 1.5f);
                RectTransform re = us.GetComponent<RectTransform>();
                re.anchoredPosition = new Vector3(baseX + (25f * i), 10, 0);
                re.sizeDelta = (new Vector2(15f, 15f));
            }
            else if (nowGesture == 6)
            {
                float baseX = (-(nowSkill.sectionAction.Count - 1) / 2f) * 25f;
                GameObject nowPrompt = Instantiate(gesture_Down, promptArea.transform);
                nowPrompt.transform.SetAsLastSibling();
                nowPromptObj.Add(nowPrompt);
                UIScript us = nowPrompt.GetComponent<UIScript>();
                us.setScale(Vector3.one * 1.5f);
                RectTransform re = us.GetComponent<RectTransform>();
                re.anchoredPosition = new Vector3(baseX + (25f * i), 10, 0);
                re.sizeDelta = (new Vector2(15f, 15f));
            }
        }
        
        
    }

    public void removePromptObj()
    {
        while(nowPromptObj.Count != 0) {
            Debug.Log("remove");
            GameObject obj = nowPromptObj[0];
            nowPromptObj.Remove(nowPromptObj[0]);
            obj.GetComponent<Animator>().SetBool("isFadeOut", true);
        }
    }

    public void removePromptOne() {
        GameObject obj = nowPromptObj[0];
        nowPromptObj.Remove(nowPromptObj[0]);
        Destroy(obj);
    }

    public void createHealthBar(SpriteBattling target)
    {
        GameObject healthBar = Instantiate(Resources.Load("HealthBar") as GameObject, mainSceneCameraControl.battlingUICanvas.gameObject.transform);
        healthBar.transform.SetAsFirstSibling();
        HealthBarUI hpBarController = healthBar.GetComponent<HealthBarUI>();
        hpBarController.setTarget(target);
        hpBarController.setScale(new Vector3(1.5f, 2f, 1.5f));
        hpBarController.gameObject.SetActive(false);
    }

    public GameObject createUIObj(GameObject obj, Vector3 pos)
    {
        GameObject g = Instantiate(obj, canvas.transform);
        g.transform.localPosition = pos;
        return g;
    }

    public void backToLobby() {
        StopCoroutine(AccountInfo.instance.expBarAnim(false, 0, 0, 0));
        if (Player.instance.levelIndexs[BattleManager.instance.selectedLevel.levelIndex] == 1)
        {
            LoadScene.targetScene = "mainScene";
        }
        else 
        {
            if (BattleManager.instance.selectedLevel.hasStoryBefore)
            {
                if (!AccountInfo.instance.isWin)
                {
                    LoadScene.targetScene = "BattleScene";
                }
                else
                {
                    LoadScene.targetScene = BattleManager.instance.selectedLevel.nextScene;
                    Player.instance.levelIndexs[BattleManager.instance.selectedLevel.levelIndex] = 1;
                }
            }
            else
            {
                if (!AccountInfo.instance.isWin)
                {
                    LoadScene.targetScene = "mainScene";
                }
                else
                {
                    LoadScene.targetScene = BattleManager.instance.selectedLevel.nextScene;
                    Player.instance.levelIndexs[BattleManager.instance.selectedLevel.levelIndex] = 1;
                }

            }

        }
        
        SceneManager.LoadScene("loadingScene");
    }

    public void setActiveUI(GameObject ui) {
        ui.SetActive(true);
    }

    public void coolDownControl(SpriteBattling player) {
        for (int i = 0; i < player.skillTime.Length; i++) {
            if (player.skillTime[i] != 0)
            {
                coolDownUI[i].gameObject.SetActive(true);
                coolDownUI[i].fillAmount = (float)player.skillTime[i] / (float)player.skills[i].coolDown;
                coolDownText[i].gameObject.SetActive(true);
                coolDownText[i].text = "" + player.skillTime[i];
            }
            else
            {
                coolDownUI[i].fillAmount = 0;
                coolDownUI[i].gameObject.SetActive(false);
                coolDownText[i].gameObject.SetActive(false);
            }
        }
    }
}
