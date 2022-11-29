using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCameraController : MonoBehaviour {
    public float t;
    public GameObject playerBattling;
    public float lastFramePlayerX;
    public bool isStartFallow;

    public float nowBGPositionX;

    public float nowFrontBGPositionX;

    public GameObject farBG;
    public float farBGWidth;

    public static BattleCameraController instance;

    public Vector2 cameraRect;
    

    Camera cameraa;


   


    void Awake() {

        transform.position -= Vector3.right * transform.position.x;
        farBGWidth = 0;
        instance = this;
    }

    // Use this for initialization
    void Start () {
    }

    public void setViewField(GameObject bg) {
        bg.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        farBG = bg;
        SpriteRenderer bgSprite = farBG.GetComponent<SpriteRenderer>();
        farBGWidth = bgSprite.bounds.size.x;
        nowBGPositionX = 0;
        t = 0;
    }
	
	// Update is called once per frame
	void Update () {
	}

    void FixedUpdate() {
        if (gameObject.transform.position.x >= (nowFrontBGPositionX))
        {
            nowFrontBGPositionX = BackGroundController.instance.createFrontBG(nowBGPositionX);
        }

        if (gameObject.transform.position.x >= (nowBGPositionX))
        {
            nowBGPositionX = BackGroundController.instance.createMiddleBG(nowBGPositionX);
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
            if (Mathf.Abs(playerBattling.transform.position.x - (transform.position.x - 2f)) > 0.05f)
            {
                float lastX = transform.position.x;
               // transform.position = new Vector3(playerBattling.transform.position.x + 6f, transform.position.y, transform.position.z);
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, playerBattling.transform.position.x + 2f, 6 * Time.deltaTime), transform.position.y, transform.position.z);
                // smoothlyCameraFallow();
                float deltaX = transform.position.x - lastX;
                BackGroundController.instance.frontBGParent.transform.position -= new Vector3(deltaX * 0.3f, 0, 0);
                nowFrontBGPositionX -= deltaX * 0.3f;
                BackGroundController.instance.farBGParent.transform.position += new Vector3(deltaX * 0.5f, 0, 0);
                nowBGPositionX += deltaX * 0.5f;
               // transform.position = new Vector3(Mathf.Lerp(transform.position.x, playerBattling.transform.position.x + 5f, 5 * Time.deltaTime), transform.position.y, transform.position.z);
            }
        }
    }

    public void smoothlyCameraFallow() {        
        float currentX;
        currentX = Mathf.Lerp(transform.position.x, playerBattling.transform.position.x, 0.8f);
        transform.position = new Vector3(currentX, 0, -10);        
    }
    
}
