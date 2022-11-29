using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicUIEvent : MonoBehaviour {

    public void exitScene(string sceneName) {
        SceneManager.UnloadSceneAsync(sceneName);
        //SceneManager.UnloadSceneAsync(SceneManager.sceneCountInBuildSettings);
    }
}
