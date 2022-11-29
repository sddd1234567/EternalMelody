using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WeaponPanel : MonoBehaviour {
    public int whoCalled;//是 0:購買視窗，還是 1:更換視窗, 2:升級視窗
    public const int weaponPerPage = 3;//每頁幾個weapon
    public int nowPage;
    public int totalPage;
    public static WeaponPanel instance;

    public Button cancelBtn;
    public Button upBtn;
    public Button downBtn;

    public Vector2 leftopPos;
    public Vector2 offset;
    public Vector2 outside;
    public float xScale, yScale;

    public GameObject bg;


    public GameObject canvas;
    public GameObject weaponOption;//單行
    public List<GameObject> weaponObjList;
    public List<Weapon> weaponList;

    public bool isOutsideInit;

    public GameObject confirmSnd;

    public void outsideGiveValue(int who)
    {
        whoCalled = who;
        isOutsideInit = true;
    }

    // Use this for initialization
    void Start () {
        //isOutsideInit = false;
        instance = this;
        StartCoroutine(init());
	}
	public void setWpList()
    {
        if(whoCalled==0)//buy
        {
            
            weaponList = Resources.Load<WeaponPurchaseList>("Weapon/WeaponPurchaseList").weaponList;
        }
        else //change,upgrade
        {
            weaponList = Player.instance.weaponList;
        }
    }

    IEnumerator init()
    {
        //whoCalled = 2;

        while(Player.instance==null || !isOutsideInit)
        {
            yield return null;
        }
        setWpList();
        totalPage = weaponList.Count % weaponPerPage == 0 ? weaponList.Count / weaponPerPage : weaponList.Count / weaponPerPage + 1;
        nowPage = 0;
        upBtn.onClick.AddListener(clickUpBtn);
        downBtn.onClick.AddListener(clickDownBtn);
        cancelBtn.onClick.AddListener(clickCancelBtn);
        
        ///////////////
        xScale = 0.7f;
        yScale = 0.7f;
        leftopPos = new Vector2(265, 223);
        offset = new Vector2(0, 15);
        ////////////////
        outside = new Vector2(-1000, 0);

        createObjList();
        setNowPage();

        

    }

    public void createObjList()
    {
        for(int i=0;i<weaponList.Count;++i)
        {
            weaponObjList.Add(Instantiate(weaponOption,bg.transform));
            //weaponObjList[i].transform.localScale +=new Vector3(xScale,yScale,0);
            weaponObjList[i].transform.position = new Vector3(weaponObjList[i].transform.position.x, 12.9f, weaponObjList[i].transform.position.z);
            WeaponEquipment wpe = weaponObjList[i].GetComponent<WeaponEquipment>();
            wpe.whoCalled = whoCalled;
            wpe.canvas = canvas;
            wpe.wp = weaponList[i];
        }
    }
    public void refreshAllObj()
    {
        
         for(int i=0;i<weaponObjList.Count;++i)
         {
                weaponObjList[i].GetComponent<WeaponEquipment>().refresh(weaponList[i]);
           
         }
        
    }

    public void setNowPage()
    {
        for(int i=0;i<weaponObjList.Count;++i)
        {
            weaponObjList[i].transform.position = outside;
        }
        for(int i=0;i<weaponPerPage;++i)
        {
            if(nowPage * weaponPerPage + i <weaponObjList.Count)
            weaponObjList[nowPage * weaponPerPage + i].GetComponent<RectTransform>().localPosition = new Vector3(-8,12.9f,0) + Vector3.down*offset.y*i;
            
        }
    }

    public void clickUpBtn()
    {
        Instantiate(confirmSnd);
        nowPage = nowPage > 0 ? nowPage - 1 : 0;
        setNowPage();
    }
    public void clickDownBtn()
    {
        Instantiate(confirmSnd);
        nowPage = nowPage + 1 >= totalPage ? nowPage : nowPage + 1;
        setNowPage();
    }
    public void clickCancelBtn()
    {
        Instantiate(confirmSnd);
        if (whoCalled==1)
        HotelPanel.instance.refreshWeapon();
        SceneManager.UnloadSceneAsync("SmithScene");
    }
}
