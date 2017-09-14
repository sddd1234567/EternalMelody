using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour {
    public GameObject backGround;    

    public RectTransform canvas;

    public float bgWidth;

    public static BackGroundController instance;

    public List<GameObject> BGs;
    // Use this for initialization

    void Awake()
    {
        instance = this;
        BGs = new List<GameObject>();
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
            if (BGs[i].transform.position.x <= BattleCameraController.instance.transform.position.x - (2 * bgWidth))
                removeBG(BGs[i]);
        }
    }

    public void removeBG(GameObject bg) {
        BGs.Remove(bg);
        Destroy(bg);
    }

    public float createBG(float x) {
        GameObject bg = Instantiate(backGround);
        BGs.Add(bg);
        bg.transform.position = new Vector3(x + (2 * bgWidth), bg.transform.position.y, bg.transform.position.z);
        return (bg.transform.position.x - bgWidth);
    }

    public void setBackGround(GameObject bg) {
        backGround = bg;
        bgWidth = bg.GetComponent<Renderer>().bounds.size.x;

        GameObject bg1 = Instantiate(bg);
        BGs.Add(bg1);
        bg1.transform.position = new Vector3(BattleCameraController.instance.nowBGPositionX - bgWidth, bg.transform.position.y, bg.transform.position.z);
        GameObject bg2 = Instantiate(bg);
        BGs.Add(bg2);
        bg2.transform.position = new Vector3(BattleCameraController.instance.nowBGPositionX + bgWidth, bg.transform.position.y, bg.transform.position.z);
        BattleCameraController.instance.nowBGPositionX = bg2.transform.position.x - bgWidth;
    }
}
