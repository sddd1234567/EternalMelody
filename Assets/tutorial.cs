using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorial : MonoBehaviour {
    [SerializeField]
    public List<tutorialEvent> tutorialList;
    public List<GameObject> objs;
    public Text text;
    public int eventCount;
    public Canvas canvas;

    void Start() {
        objs = new List<GameObject>();
        text.text = tutorialList[0].tutorialText;   
    }

    public void next() {
        eventCount++;

        if (eventCount >= tutorialList.Count)
        {
            closeWindow();
        }
        else
        {
            if (tutorialList[eventCount].isDark)
                transform.SetAsLastSibling();
            if (tutorialList[eventCount].isLight)
                lightObj(tutorialList[eventCount].lightUI);
            if (tutorialList[eventCount].isDestroy)
                destroyObjs(objs);
            if (tutorialList[eventCount].isCreate)
                createObj(tutorialList[eventCount].obj);
                        
            text.text = tutorialList[eventCount].tutorialText;
        }
    }

    public void lightObj(GameObject obj)
    {
        obj.transform.SetAsLastSibling();
    }

    public void createObj(GameObject obj) {
        GameObject o;
        if (UIManager.instance != null)
        {
            o = UIManager.instance.createUIObj(tutorialList[eventCount].obj, tutorialList[eventCount].obj.transform.position);
        }
        else
        {
            o = Instantiate(obj, canvas.transform);
            o.transform.position = obj.transform.position;
            o.transform.localScale = obj.transform.localScale;
        }
            
        o.transform.localScale = tutorialList[eventCount].obj.transform.localScale;
        objs.Add(o);
    }


    public void destroyObjs(List<GameObject> o) {
        for (int i = 0; i < o.Count; i++) {
            Destroy(o[i]);
        }
    }

    public void closeWindow() {
        if(TutorialController.instance != null)
            TutorialController.instance.closeWindow();
        Destroy(gameObject);
    }
}

[System.Serializable]
public struct tutorialEvent {
    public bool isLight;
    public bool isCreate;
    public bool isDestroy;
    public bool isDark;
    public GameObject obj;
    [TextArea(3, 10)]
    public string tutorialText;
    public GameObject lightUI;
}
