using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCameraController : MonoBehaviour {
    public float t;
    public GameObject playerBattling;
    public float lastFramePlayerX;
    public bool isStartFallow;

    public float nowBGPositionX;

    public static BattleCameraController instance;


    void Awake() {
        instance = this;
    }

    // Use this for initialization
    void Start () {
        nowBGPositionX = 0;
        t = 0;        
    }
	
	// Update is called once per frame
	void Update () {
        


        if (gameObject.transform.position.x >= (nowBGPositionX))
        {
            nowBGPositionX = BackGroundController.instance.createBG(nowBGPositionX);
        }

        if (playerBattling != null)
        {
            moveControl();
            lastFramePlayerX = playerBattling.transform.position.x;
        }
	}

    

    public void moveControl() {
        if (lastFramePlayerX != playerBattling.transform.position.x)
        {
            t = 0;
        }

        if (isStartFallow)
        {
            if (Mathf.Abs(playerBattling.transform.position.x - transform.position.x) > 0.05f)
            {
                transform.position = new Vector3(playerBattling.transform.position.x + 6f, transform.position.y, transform.position.z);
              //  transform.position = new Vector3(Mathf.Lerp(transform.position.x, playerBattling.transform.position.x + 5f, 5 * Time.deltaTime), transform.position.y, transform.position.z);
            }
        }
    }

    public void smoothlyCameraFallow() {        
        float currentX;
        currentX = Mathf.Lerp(transform.position.x, playerBattling.transform.position.x, t);
        t += 0.85f * Time.deltaTime;
        transform.position = new Vector3(currentX, 0, -10);
    }
}
