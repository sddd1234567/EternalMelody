using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour {
    public GameObject middleBackGround;

    public List<GameObject> frontBackGround;

    public RectTransform canvas;

    public float bgWidth;
    public float bgOffset;

    public static BackGroundController instance;

    public List<GameObject> BGs;
    public List<GameObject> frontBG;

    public GameObject frontBGParent;
    public GameObject farBGParent;
    // Use this for initialization

    void Awake()
    {
        bgOffset = 0.8f;
        frontBGParent = new GameObject("FrontBGPrent");
        farBGParent = new GameObject("FarBGParent");
        instance = this;
        BGs = new List<GameObject>();
    }

    void FixedUpdate() {
    }

    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        BGControl();
    }

    public void BGControl() {
        if (BGs.Count == 0)
            return;

        for (int i = 0; i < BGs.Count; i++) {
            if (BGs[i].transform.position.x <= BattleCameraController.instance.transform.position.x - (2 * (bgWidth - bgOffset)))
                removeBG(BGs[i]);
        }
    }

    public void removeBG(GameObject bg) {
        BGs.Remove(bg);
        Destroy(bg);
    }

    public float createMiddleBG(float x) {
        GameObject bg = Instantiate(middleBackGround,farBGParent.transform);
        BGs.Add(bg);
        bg.transform.position = new Vector3(x + (2 * (bgWidth-bgOffset)), bg.transform.position.y, bg.transform.position.z);
        return (bg.transform.position.x - (bgWidth - bgOffset));
    }

    public float createFrontBG(float x)
    {
        if (frontBackGround.Count == 0)
            return 0;
        GameObject bg = Instantiate(frontBackGround[Random.Range(0,frontBackGround.Count)], frontBGParent.transform);
        BGs.Add(bg);
        bg.transform.position = new Vector3(x + (2 * (bgWidth - bgOffset)) + Random.Range(-2,2), bg.transform.position.y, bg.transform.position.z);
        return (bg.transform.position.x - (bgWidth - bgOffset));
    }

    public void setFrontBackGround(List<GameObject> bg)
    {
        frontBackGround = bg;
    }


    public void setBackGround(GameObject bg) {
        middleBackGround = bg;
        bgWidth = bg.GetComponent<Renderer>().bounds.size.x;

        if (bg != null)
        {
            GameObject bg1 = Instantiate(bg, farBGParent.transform);
            BGs.Add(bg1);
            bg1.transform.position = new Vector3(BattleCameraController.instance.nowBGPositionX - (bgWidth - bgOffset), bg.transform.position.y, bg.transform.position.z);
            GameObject bg2 = Instantiate(bg, farBGParent.transform);
            BGs.Add(bg2);
            bg2.transform.position = new Vector3(BattleCameraController.instance.nowBGPositionX + (bgWidth - bgOffset), bg.transform.position.y, bg.transform.position.z);
            BattleCameraController.instance.nowBGPositionX = bg2.transform.position.x - (bgWidth - bgOffset);
            BattleCameraController.instance.nowFrontBGPositionX = bg2.transform.position.x - (bgWidth - bgOffset);
        }
        if (frontBackGround.Count != 0)
        {
            GameObject bg3 = Instantiate(frontBackGround[Random.Range(0, frontBackGround.Count)], frontBGParent.transform);
            BGs.Add(bg3);
            bg3.transform.position = new Vector3(BattleCameraController.instance.nowBGPositionX - (bgWidth - bgOffset), bg3.transform.position.y, bg3.transform.position.z);
            GameObject bg4 = Instantiate(frontBackGround[Random.Range(0, frontBackGround.Count)], frontBGParent.transform);
            BGs.Add(bg4);
            bg4.transform.position = new Vector3(BattleCameraController.instance.nowBGPositionX + (bgWidth - bgOffset), bg4.transform.position.y, bg4.transform.position.z);
        }

        
    }
}
