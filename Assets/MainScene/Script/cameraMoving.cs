using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMoving : MonoBehaviour {
    public float targetX;
    public float t;
    public bool isMoving;
    public int nowUI;
    public float[] UIPosition;
    public float lastDeltaX;
    public bool isSmoothing;
    public Camera mainCamera;

    public static cameraMoving instance;

    void Awake() {
        instance = this;
    }

    void Start()
    {
        nowUI = 0;
    }

    // Update is called once per frame
    void Update()
    {
        moveCameraByPointer();
        if (mainCamera.transform.position.x != UIPosition[nowUI] && !isMoving)
        {
            smoothlyCamera(nowUI);
            isSmoothing = true;
        }
        else
            isSmoothing = false;

    }

    public void moveCameraByPointer()
    {
        if (isMoving && Input.touchCount > 0)
        {
            mainCamera.transform.Translate(-Input.touches[0].deltaPosition.x * 0.08f, 0, 0);
            Debug.Log(-Input.touches[0].deltaPosition.x);
            if (mainCamera.transform.position.x < UIPosition[0])
                mainCamera.transform.position = new Vector3(0, 0, -10);
            else if (mainCamera.transform.position.x > UIPosition[3])
                mainCamera.transform.position = new Vector3(UIPosition[3], 0, -10);
            lastDeltaX = -Input.touches[0].deltaPosition.x;
        }
    }

    public void smoothlyCamera(int targetUI)
    {
        if (Mathf.Abs(mainCamera.transform.position.x - UIPosition[nowUI]) < 1)
        {
            mainCamera.transform.position = new Vector3(UIPosition[nowUI], 0, -10);
        }

        float currentX;
        currentX = Mathf.Lerp(mainCamera.transform.position.x, UIPosition[targetUI], t);
        t += 0.85f * Time.deltaTime;
        mainCamera.transform.position = new Vector3(currentX, 0, -10);
    }

    public void onPointerEnter()
    {
        if (!isSmoothing)
        {
            isMoving = true;
            t = 0;
        }
    }

    public void onPointerExit()
    {
        isMoving = false;
        float cameraPositionX;
        cameraPositionX = mainCamera.transform.position.x;
        if (Mathf.Abs(cameraPositionX - UIPosition[nowUI]) > 1)
        {
            if (cameraPositionX - UIPosition[nowUI] > 0)
                nowUI++;
            else
                nowUI--;
        }
    }
}
