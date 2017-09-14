using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicUIEvent : MonoBehaviour {

    public void exitScene(int index) {
        SceneManager.UnloadSceneAsync(index);
        //SceneManager.UnloadSceneAsync(SceneManager.sceneCountInBuildSettings);
    }
}
