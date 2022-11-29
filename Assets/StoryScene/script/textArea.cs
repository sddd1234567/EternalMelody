using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class textArea : MonoBehaviour {
    public Text storyText;
    public const int delay = 3;
    public int nowIdx;
    public int strSize;
    public int frameCount;//每frame+1
    public string pageStr;
    public int nowPage;
    public bool finish;
    public int totalPage;

    public Image fadeOut;

    public GameObject dialogObj;

    public string nextScene;
    //public string nameStr;
    public Text nameTxt;

    //public List<int> eventList;   // 0 = 對話   1 = 建立一般UI物件  2 = 刪除所有生成物件     3 = 建立UI物件 + 隱藏對話窗   4=ActiveObject   5 = changeBGM
    public int nowEvent;

    public storySetting setting;
    public List<dialogEvent> book;

    public GameObject canvas;

    public GameObject s;

    public List<GameObject> nowObjList;

    public bool dialogEnded;

    public GameObject black;

    public string levelPath;
//    public List<string> bookStr;
//    public List<string> bookname;
    // Use this for initialization
	void Start () {
        setting = s.GetComponent<storySetting>();
        //totalPage = 2;
        //nowPage = 0;
        frameCount = 0;
        clearText();
        nowObjList = new List<GameObject>();
        initBook(setting.dialogList);
        initNext(setting.nextScene);
        initPath(setting.levelPath);
        readPage(nowPage);
    }
	
	// Update is called once per frame
	void Update () {
        
        drawText();
        textClick();
	}

    void FixedUpdate() {
        updateFrame();
    }

    void updateFrame()
    {
        frameCount++;
    }

    void clearText()
    {
        storyText.text = "";
        pageStr = "";
        nowIdx =-1;
    }

    void drawText()
    {
        if (finish)
        {
            return;
        }

        if (frameCount >= delay)
        {
            frameCount = 0;
            nowIdx++;
            if(nowIdx>=strSize)
            {
                finish = true;
                return;
            }
            
            storyText.text += pageStr[nowIdx];
        }
    }

    void textClick()
    {
        if (Input.touchCount > 0 && !dialogEnded)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)//讓他只會讀到一次
            {

                if (finish)
                {
                    if (book[nowEvent].eventCode == 0)
                        book[nowEvent].speaker.transform.position = new Vector3(book[nowEvent].speaker.transform.position.x, book[nowEvent].speaker.transform.position.y, -3);


                    nowEvent++;
                    if (nowEvent >= book.Count)
                    {
                        StartCoroutine(dialogEnd());
                        dialogEnded = true;
                        return;
                    }
                    clearText();
                    if (book[nowEvent].eventCode == 0)
                    {
                        if (!string.IsNullOrEmpty(book[nowEvent].speaker.characterName))
                            black.SetActive(true);
                        book[nowEvent].speaker.transform.position = new Vector3(book[nowEvent].speaker.transform.position.x, book[nowEvent].speaker.transform.position.y, -5);
                        resumeDialog();
                        nowPage++;
                        if (nowPage >= totalPage)
                        {
                            StartCoroutine(dialogEnd());
                            dialogEnded = true;
                            return;
                        }
                        readPage(nowPage);
                        finish = !finish;
                    }

                    else if (book[nowEvent].eventCode == 1)
                    {
                        //black.SetActive(false);
                        resumeDialog();
                        createNormalObject(book[nowEvent].obj);
                        if (!string.IsNullOrEmpty(book[nowEvent].speaker.characterName))
                            black.SetActive(true);
                        book[nowEvent].speaker.transform.position = new Vector3(book[nowEvent].speaker.transform.position.x, book[nowEvent].speaker.transform.position.y, -5);
                        resumeDialog();
                        nowPage++;
                        if (nowPage >= totalPage)
                        {
                            StartCoroutine(dialogEnd());
                            dialogEnded = true;
                            return;
                        }
                        readPage(nowPage);
                        finish = !finish;
                    }

                    else if (book[nowEvent].eventCode == 2)
                    {
                        black.SetActive(false);
                        destroyAllObj();
                        resumeDialog();
                        nowPage++;
                        if (nowPage >= totalPage)
                        {
                            StartCoroutine(dialogEnd());
                            dialogEnded = true;
                            return;
                        }
                        readPage(nowPage);
                        finish = !finish;
                    }

                    else if (book[nowEvent].eventCode == 3)
                    {
                        black.SetActive(false);
                        createObj(book[nowEvent].obj);
                        disableDialog();
                    }

                    else if (book[nowEvent].eventCode == 4)
                    {
                        //black.SetActive(false);
                        resumeDialog();
                        activeObject(book[nowEvent].obj);
                        if (!string.IsNullOrEmpty(book[nowEvent].speaker.characterName))
                            black.SetActive(true);
                        book[nowEvent].speaker.transform.position = new Vector3(book[nowEvent].speaker.transform.position.x, book[nowEvent].speaker.transform.position.y, -5);
                        resumeDialog();
                        nowPage++;
                        if (nowPage >= totalPage)
                        {
                            StartCoroutine(dialogEnd());
                            dialogEnded = true;
                            return;
                        }
                        readPage(nowPage);
                        finish = !finish;
                    }
                    else if (book[nowEvent].eventCode == 5)
                    {
                        if (bgmController.instance != null)
                        {
                            StartCoroutine(bgmController.instance.fadeOut());
                            StartCoroutine(bgmController.instance.waitForChange(book[nowEvent].audio));
                        }
                        if (!string.IsNullOrEmpty(book[nowEvent].speaker.characterName))
                            black.SetActive(true);
                        book[nowEvent].speaker.transform.position = new Vector3(book[nowEvent].speaker.transform.position.x, book[nowEvent].speaker.transform.position.y, -5);
                        resumeDialog();
                        nowPage++;
                        if (nowPage >= totalPage)
                        {
                            StartCoroutine(dialogEnd());
                            dialogEnded = true;
                            return;
                        }
                        readPage(nowPage);
                        finish = !finish;
                    }
                }
                else
                {
                    storyText.text = pageStr;
                    finish = !finish;
                }


            }
        }

        else if (Input.GetMouseButtonDown(0) && !dialogEnded)
        {
            if (finish)
            {
                if (book[nowEvent].eventCode == 0)
                    book[nowEvent].speaker.transform.position = new Vector3(book[nowEvent].speaker.transform.position.x, book[nowEvent].speaker.transform.position.y, -3);


                nowEvent++;
                if (nowEvent >= book.Count)
                {
                    StartCoroutine(dialogEnd());
                    dialogEnded = true;
                    return;
                }
                clearText();
                if (book[nowEvent].eventCode == 0)
                {
                    if (!string.IsNullOrEmpty(book[nowEvent].speaker.characterName))
                        black.SetActive(true);
                    book[nowEvent].speaker.transform.position = new Vector3(book[nowEvent].speaker.transform.position.x, book[nowEvent].speaker.transform.position.y, -5);
                    resumeDialog();
                    nowPage++;
                    if (nowPage >= totalPage)
                    {
                        StartCoroutine(dialogEnd());
                        dialogEnded = true;
                        return;
                    }
                    readPage(nowPage);
                    finish = !finish;
                }

                else if (book[nowEvent].eventCode == 1)
                {
                    //black.SetActive(false);
                    resumeDialog();
                    createNormalObject(book[nowEvent].obj);
                    if (!string.IsNullOrEmpty(book[nowEvent].speaker.characterName))
                        black.SetActive(true);
                    book[nowEvent].speaker.transform.position = new Vector3(book[nowEvent].speaker.transform.position.x, book[nowEvent].speaker.transform.position.y, -5);
                    resumeDialog();
                    nowPage++;
                    if (nowPage >= totalPage)
                    {
                        StartCoroutine(dialogEnd());
                        dialogEnded = true;
                        return;
                    }
                    readPage(nowPage);
                    finish = !finish;
                }

                else if (book[nowEvent].eventCode == 2)
                {
                    black.SetActive(false);
                    destroyAllObj();
                    resumeDialog();
                    nowPage++;
                    if (nowPage >= totalPage)
                    {
                        StartCoroutine(dialogEnd());
                        dialogEnded = true;
                        return;
                    }
                    readPage(nowPage);
                    finish = !finish;
                }

                else if (book[nowEvent].eventCode == 3)
                {
                    black.SetActive(false);
                    createObj(book[nowEvent].obj);
                    disableDialog();
                }

                else if (book[nowEvent].eventCode == 4)
                {
                    //black.SetActive(false);
                    resumeDialog();
                    activeObject(book[nowEvent].obj);
                    if (!string.IsNullOrEmpty(book[nowEvent].speaker.characterName))
                        black.SetActive(true);
                    book[nowEvent].speaker.transform.position = new Vector3(book[nowEvent].speaker.transform.position.x, book[nowEvent].speaker.transform.position.y, -5);
                    resumeDialog();
                    nowPage++;
                    if (nowPage >= totalPage)
                    {
                        StartCoroutine(dialogEnd());
                        dialogEnded = true;
                        return;
                    }
                    readPage(nowPage);
                    finish = !finish;
                }
                else if (book[nowEvent].eventCode == 5)
                {
                    if (bgmController.instance != null)
                    {
                        StartCoroutine(bgmController.instance.fadeOut());
                        StartCoroutine(bgmController.instance.waitForChange(book[nowEvent].audio));
                    }
                    if (!string.IsNullOrEmpty(book[nowEvent].speaker.characterName))
                        black.SetActive(true);
                    book[nowEvent].speaker.transform.position = new Vector3(book[nowEvent].speaker.transform.position.x, book[nowEvent].speaker.transform.position.y, -5);
                    resumeDialog();
                    nowPage++;
                    if (nowPage >= totalPage)
                    {
                        StartCoroutine(dialogEnd());
                        dialogEnded = true;
                        return;
                    }
                    readPage(nowPage);
                    finish = !finish;
                }
            }
            else
            {
                storyText.text = pageStr;
                finish = !finish;
            }
        }
    }

    public void onSkipClicked() {
        dialogEnded = true;
        StartCoroutine(dialogEnd());
    }

    public void activeObject(GameObject obj)
    {
        obj.SetActive(true);
    }

    IEnumerator dialogEnd() {
        if (bgmController.instance != null)
            StartCoroutine(bgmController.instance.fadeOut());
        yield return null;
        while(fadeOut.color.a <= 0.95)
        {
            fadeOut.color += new Color(0, 0, 0, 1f * Time.deltaTime);
            yield return null;
        }
        missionController.path = levelPath;
        LoadScene.targetScene = nextScene;
        SceneManager.LoadScene("loadingScene");
    }

    void readPage(int page)
    {
        pageStr = book[page].bookStr;
        nameTxt.text = book[page].speaker.characterName;
        strSize = pageStr.Length;
    }

    void initBook(List<dialogEvent> b)
    {
        nowPage = 0;
        book = b;
        totalPage = b.Count;
    }
    void initNext(string next)
    {
        nextScene = next;
    }

    void initPath(string path)
    {
        levelPath = path;
    }

    void createObj(GameObject o) {
        GameObject obj = Instantiate(o, canvas.transform);
        nowObjList.Add(obj);
        obj.GetComponent<RectTransform>().localScale = o.GetComponent<RectTransform>().localScale;
        obj.GetComponent<RectTransform>().localPosition = o.GetComponent<RectTransform>().localPosition;
    }

    void createNormalObject(GameObject o) {
        GameObject obj = Instantiate(o);
        nowObjList.Add(obj);
        obj.transform.position = o.transform.position;
        obj.transform.localScale = o.transform.localScale;
    }

    void destroyAllObj() {
        for (int i = 0; i < nowObjList.Count; i++)
        {
            Destroy(nowObjList[i]);
        }
    }

    void disableDialog()
    {
        dialogObj.SetActive(false);
    }

    void resumeDialog() {
        dialogObj.SetActive(true);
    }    
}
