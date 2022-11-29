using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SkillPanel : MonoBehaviour {
    public int whoCalled;//是 0:購買視窗，還是 1:更換視窗, 2:升級視窗
    public int befIndex;//如果是更換，才會有值

    public GameObject bg;
    public const int skillPerPage = 3;//每頁幾個weapon
    public int nowPage;
    public int totalPage;
    public static SkillPanel instance;

    public Button upBtn;
    public Button downBtn;
    public Button cancelBtn;

    public Vector2 leftopPos;
    public Vector2 offset;
    public Vector2 outside;

    public float xScale, yScale;

    public GameObject canvas;
    public GameObject skillOption;
    public List<GameObject> skillObjList;
    public List<Skill> skillList;
    public bool isValued;

    public GameObject confirmSnd;

    // Use this for initialization
    void Start () {
        instance = this;
        StartCoroutine(init());
	}
    public void setValue(int who,int idx)
    {
        whoCalled = who;
        befIndex = idx;
        isValued = true;
    }
    IEnumerator init()
    {
        //whoCalled = 2;

        while (Player.instance == null|| !isValued)
        {
            yield return null;
        }
        setSklList();
        totalPage = skillList.Count % skillPerPage == 0 ? skillList.Count / skillPerPage : skillList.Count / skillPerPage + 1;
        nowPage = 0;
        upBtn.onClick.AddListener(clickUpBtn);
        downBtn.onClick.AddListener(clickDownBtn);
        cancelBtn.onClick.AddListener(clickCancelBtn);
        /////////////
        yScale = 0.7f;
        xScale = 0.7f;
        leftopPos = new Vector2(265, 223);
        offset = new Vector2(0, 15);
        ///////////////////
        outside = new Vector2(-1000, 0);

        createObjList();
        setNowPage();

        

    }

    public void setSklList()
    {
        skillList = Player.instance.skillList;
    }

    public void createObjList()
    {
        for (int i = 0; i < skillList.Count; ++i)
        {
            skillObjList.Add(Instantiate(skillOption, bg.transform));
            //skillObjList[i].transform.localScale+=new Vector3(xScale,yScale,0);
            SkillEquipment skle = skillObjList[i].GetComponent<SkillEquipment>();
            skle.whoCalled = whoCalled;
            skle.befIndex = befIndex;
            skle.canvas = canvas;
            skle.skl = skillList[i];
            skle.num = i;
        }
    }

    public void refreshAllObj()
    {

        for (int i = 0; i < skillObjList.Count; ++i)
        {
            skillObjList[i].GetComponent<SkillEquipment>().refresh(skillList[i]);

        }

    }

    public void setNowPage()
    {
        for (int i = 0; i < skillObjList.Count; ++i)
        {
            skillObjList[i].transform.position = outside;
        }
        for (int i = 0; i < skillPerPage; ++i)
        {
            if (nowPage * skillPerPage + i < skillObjList.Count)
                skillObjList[nowPage * skillPerPage + i].GetComponent<RectTransform>().localPosition = new Vector3(-8,12.9f,0) + Vector3.down * offset.y * i;

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
        HotelPanel.instance.refrshSkill();
        SceneManager.UnloadSceneAsync("TrainCenterScene");
    }
}
