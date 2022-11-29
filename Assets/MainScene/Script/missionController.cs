using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//負責load scene 跟 儲存路徑
public class missionController : MonoBehaviour
{
    [SerializeField]
    public static string path;
    public GameObject loadLevel;    
    public GameObject enterSnd;
	void Start () {
        path = "-1";
	}
	public static void setPath(string ph)
    {
        path = ph;
    }
	// Update is called once per frame
	void Update () {
		
	}
    public void pressEnter()
    {
        Instantiate(enterSnd);
        //Debug.Log(path);
        if (path != "-1")
        {
            Instantiate(loadLevel);
            LoadScene.targetScene = "BattleScene";
            SceneManager.LoadScene("loadingScene");
        }
    }

    
}
