using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class shopPanel : MonoBehaviour {

    public Button cancelBtn;
    public Button skillLotteryBtn;
    public Button weaponLotteryBtn;
    public Button weaponPurchaseBtn;

    public GameObject lotteryPanel;
    public GameObject canvas;
    public Vector2 middleCanvas;
    public float xScale, yScale;
    public GameObject confirmSnd;
	// Use this for initialization
	void Start () {
        init();
	}
	
    public void init()
    {
        xScale = 0.7f;
        yScale = 0.7f;
        middleCanvas = new Vector2(400, 200);
        cancelBtn.onClick.AddListener(clickCancelBtn);
        weaponPurchaseBtn.onClick.AddListener(clickWeaponPurchaseBtn);
        weaponLotteryBtn.onClick.AddListener(clickWeaponLotteryBtn);
        skillLotteryBtn.onClick.AddListener(clickSkillLotteryBtn);
    }

    public void clickCancelBtn()
    {
        Instantiate(confirmSnd);
        SceneManager.UnloadSceneAsync("ShopScene");
    }

    public void clickWeaponPurchaseBtn()
    {
        Instantiate(confirmSnd);
        SceneManager.LoadSceneAsync("SmithScene", LoadSceneMode.Additive);
        StartCoroutine(giveWeaponPanelValue());
    }
    IEnumerator giveWeaponPanelValue()
    {
        while(WeaponPanel.instance==null)
        {
            yield return null;
        }
        WeaponPanel.instance.outsideGiveValue(0);
    }

    public void clickWeaponLotteryBtn()
    {
        Instantiate(confirmSnd);
        GameObject lotPanel = Instantiate(lotteryPanel, canvas.transform);
        RectTransform rec = lotPanel.GetComponent<RectTransform>();
        rec.localScale = Vector3.one * 0.8f;
        rec.localPosition = Vector3.one;
        StartCoroutine(giveWpLotPanelValue());
    }
    IEnumerator giveWpLotPanelValue()
    {
        while(LotteryPanel.instance==null)
        {
            yield return null;
        }
        LotteryPanel.instance.valued(0);
    }

    public void clickSkillLotteryBtn()
    {
        Instantiate(confirmSnd);
        GameObject lotPanel = Instantiate(lotteryPanel, canvas.transform);
        RectTransform rec = lotPanel.GetComponent<RectTransform>();
        rec.localScale = Vector3.one * 0.8f;
        rec.localPosition = Vector3.one;
        StartCoroutine(giveSklLotPanelValue());
    }
    IEnumerator giveSklLotPanelValue()
    {
        while (LotteryPanel.instance == null)
        {
            yield return null;
        }
        LotteryPanel.instance.valued(1);
    }
}
