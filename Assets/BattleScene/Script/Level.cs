using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Level : ScriptableObject {
    public int levelIndex;
    public string levelName;
    public List<levelEnemy> enemyWave1;
    public List<levelEnemy> enemyWave2;
    public List<levelEnemy> enemyWave3;
    public string musicName;
    public GameObject farBackGround;
    public GameObject middleBackGround;
    public List<GameObject> frontBackGround;
    public award aw;

    public bool hasStoryBefore;
    public bool hasStoryAfter;

    public string nextScene;
}

[System.Serializable]
public class levelEnemy {
    public string enemyName;
}

[System.Serializable]
public struct award {
    public int gold;
    public int skillChip;
    public int temperStar;
    public int exp;
}
