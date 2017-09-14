using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainSceneCameraControl : MonoBehaviour {
    public Camera mainCamera;
    
    public CanvasScaler UICanvasScaler;

    public static GameObject battlingUICanvas;
    // Use this for initialization

    void Awake() {
        screenSpaceCanvasScale();
        cameraScale();
        battlingUICanvas = gameObject;
    }
    
    

    public void cameraScale() {
        float targetAspect = 16.0f / 9.0f;
        
        float nowAspect = (float)Screen.width / (float)Screen.height;
        
        float scaleHeight = nowAspect / targetAspect;

        if (scaleHeight < 1.0f)
        {
            Rect rect = mainCamera.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            mainCamera.rect = rect;
        }
        else
        {
            float scalewidth = 1.0f / scaleHeight;

            Rect rect = mainCamera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            mainCamera.rect = rect;
        }
    }

    public void screenSpaceCanvasScale() {
        float screenWidthScale = Screen.width / UICanvasScaler.referenceResolution.x;
        float screenHeightScale = Screen.height / UICanvasScaler.referenceResolution.y;
        UICanvasScaler.matchWidthOrHeight = screenWidthScale > screenHeightScale ? 1 : 0;
    }
}
 
