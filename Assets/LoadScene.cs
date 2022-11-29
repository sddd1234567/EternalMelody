using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {
    AsyncOperation async;
    public static string targetScene;
    public float time;
    public bool canGo;
    public static bool isLastLV;
    // Use this for initialization
    void Start () {
        LoadSaveData.Save();
        LoadSaveData.Load();
        if(targetScene == "BattleScene")
        {
            if (missionController.path != null)
            {
                if (missionController.path == "Level/Chapter2" && !isLastLV)
                {
                    isLastLV = true;
                    if (Player.instance.levelIndexs[2] == 0)
                        targetScene = "Chapter2-4";
                }
            }
                
            

        }
        StartCoroutine(loadScene());
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time >= 0.5f)
        {
            canGo = true;
        }
	}

    public IEnumerator loadScene()
    {
        yield return null;
        async = SceneManager.LoadSceneAsync(targetScene);
        async.allowSceneActivation = false;
        while (!canGo)
        {
            yield return null;
        }
        async.allowSceneActivation = true;
    }
}
