using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSceneButtonController : MonoBehaviour {

    public Button trainCenterBtn;
    public Button smithBtn;
    public Button hotelBtn;
    public Button shopBtn;

    public GameObject teleportSnd;

	// Use this for initialization
	void Start () {
        //trainCenterBtn.onClick.AddListener(clickTrainCenterBtn);
        //smithBtn.onClick.AddListener(clickSmithBtn);
        //hotelBtn.onClick.AddListener(clickHotelBtn);
        //shopBtn.onClick.AddListener(clickShopBtn);
	}
	
    public void clickTrainCenterBtn()
    {
        Instantiate(teleportSnd);
        SceneManager.LoadSceneAsync("TrainCenterScene", LoadSceneMode.Additive);
        StartCoroutine(giveSkillValue());
    }
    IEnumerator giveSkillValue()
    {
        while (SkillPanel.instance == null)
        {
            yield return null;
        }
        SkillPanel.instance.setValue(2, 0);//upgrade
    }
    public void clickSmithBtn()
    {
        Instantiate(teleportSnd);
        SceneManager.LoadSceneAsync("SmithScene", LoadSceneMode.Additive);
        StartCoroutine(giveWeaponValue());

    }
    IEnumerator giveWeaponValue()
    {
        while (WeaponPanel.instance == null)
        {
            yield return null;
        }
        WeaponPanel.instance.outsideGiveValue(2);//upgrade
    }
    public void clickShopBtn()
    {
        Instantiate(teleportSnd);
        SceneManager.LoadSceneAsync("ShopScene", LoadSceneMode.Additive);
    }
    public void clickHotelBtn()
    {
        Instantiate (teleportSnd);
        SceneManager.LoadSceneAsync("HotelScene", LoadSceneMode.Additive);
    }
}
