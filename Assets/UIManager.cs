using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour {
    
    public GameObject startCountObj;
    public GameObject canvas;

    public GameObject waveText;

    public GameObject waveClear;

    public static UIManager instance;
    public static int i;

    public TextMeshProUGUI comboText;
    public TextMeshProUGUI combo;
        
    public GameObject gesture_Point;
    public GameObject gesture_Right;
    
    public RectTransform promptArea;

    public int nowGesture;

    public Skill nowSkill;

    public RectTransform rhythmCircle;
    public GameObject rhythmArrived;

    public GameObject shine;

    public List<GameObject> nowPromptObj;
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
	void Update () {
        setComboText();
        if (rhythmCircle.localScale.x <= 0.1)
        {
            rhythmCircle.gameObject.SetActive(true);
        }
        else if(rhythmCircle.localScale.x >= 0.9)
        {
            rhythmCircle.gameObject.SetActive(false);
        }
        
	}
    public void setComboText() {
        if (BattleManager.instance.Combo != 0)
        {
            comboText.SetText(BattleManager.instance.Combo.ToString());
            combo.SetText("Combo!!");
        }
        else
        {
            comboText.SetText("");
            combo.SetText("");
        }
    }

    public void waveCleared() {
        Instantiate(waveClear, canvas.transform);
    }

    public void startCount() {
        GameObject obj = Instantiate(startCountObj,canvas.transform);
        RectTransform Rect = obj.GetComponent<RectTransform>();
        Rect.localPosition = new Vector3(0f, 10f, 0f);
        Rect.localScale =new Vector3(1.5f,1.5f,1.5f);
        obj.GetComponent<Animator>().speed = 1/MusicHandler.instance.BPM;
    }

    public void createWaveText() {
        GameObject obj = Instantiate(waveText, canvas.transform);
        RectTransform rect = obj.GetComponent<RectTransform>();
        obj.GetComponent<WaveTextAnimation>().setText(BattleManager.instance.nowWave + 1);
        rect.localScale = Vector3.one;
        rect.localPosition = new Vector3(0, 10, 0);
        rect.sizeDelta = new Vector2(42, 13);
        BattleManager.instance.enemyWalkIn();
    }

    public void gesturePrompt() {
        for (int i = 0; i < nowSkill.sectionAction.Count; i++)
        {
            nowGesture = nowSkill.sectionAction[i].gesture;
            
            if (nowGesture == 1)
            {
                float baseX = (-(nowSkill.sectionAction.Count - 1) / 2f)*25f;
                GameObject nowPrompt = Instantiate(gesture_Point, promptArea.transform);
                nowPromptObj.Add(nowPrompt);
                UIScript us = nowPrompt.GetComponent<UIScript>();
                us.setScale(Vector3.one);
                RectTransform re = us.GetComponent<RectTransform>();
                re.anchoredPosition = new Vector3(baseX + (25f * i), 10, 0);
                re.sizeDelta = (new Vector2(15f, 15f));
            }
            else if (nowGesture == 4)
            {
                float baseX = (-(nowSkill.sectionAction.Count - 1) / 2f) * 25f;
                GameObject nowPrompt = Instantiate(gesture_Right, promptArea.transform);
                nowPromptObj.Add(nowPrompt);
                UIScript us = nowPrompt.GetComponent<UIScript>();
                us.setScale(Vector3.one);
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

    public void createUIObject(GameObject obj) {
        Instantiate(obj, canvas.transform);
    }
}
