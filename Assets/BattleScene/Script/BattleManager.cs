using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {
    #region skills objects definition
    public int activeSkill;
    public List<int>[] skill; //技能的畫法 1：點擊 2：壓2拍 3：向左劃 4：向右劃　５：向上劃　６：向下劃     取自技能的資料
    public List<int> nowSkill;  //按下技能按鍵後這個會=那個技能，之後方便做手勢判斷
    public int nowGesture;  //紀錄現在是技能的第幾下
    public float skillEnegyBar;
    #endregion

    #region targets objects definition
    public List<SpriteBattling> enemy;
    public List<SpriteBattling> player;
    public PlayerBattling playerBattling;
    public PlayerAI playerAI;
    public SpriteBattling nowTarget;
    #endregion

    public Vector3 playerPosition;
    public Vector3[] enemyPosition;

    public float[] timingInterval;

    public GameObject playerObject;
    private bool hadMoved;
    private bool isHit;     //這拍有沒有打中拍點
    public int Combo;
    public bool hadHit;
    public bool isGameStop;

    public static BattleManager instance;
    public Level selectedLevel;
    public GameObject[] Wave;

    public List<SpriteBattling>[] enemyWave;
    public int nowWave;
    public bool isPress;
    public int skillPressTime;
    public int nowPressTime;
    public Animator playerAnim;

    public bool isGameEnd;

    public bool isTimingArrived;

    public AIManager AIManager;

    public GameObject mask;

    public GameObject nowMask;

    public BattleCameraController cameraControl;
    
    
    // Use this for initialization

    void Awake() {
        enemyWave = new List<SpriteBattling>[3];
        enemyWave[0] = new List<SpriteBattling>();
        enemyWave[1] = new List<SpriteBattling>();
        enemyWave[2] = new List<SpriteBattling>();
        skill = new List<int>[3];
        skill[0] = new List<int>();
        skill[1] = new List<int>();
        skill[2] = new List<int>();
        Wave = new GameObject[3];
        instance = this;
        LoadWave();
        isGameStop = true;
        isGameEnd = true;        
        
    }

	void Start () {
        MusicHandler.instance.loadMusic(selectedLevel.musicName);
        Combo = 0;
        LoadLevelPrefab();        
        hadHit = false;
        AIManager.loadSprites(enemy, player);
        
	}
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(enemyInView());

           if (Input.touchCount == 0)//沒有按，沒有動
                 hadMoved = false;
        if (activeSkill != 0 && !isGameStop)//代表現在有技能正在施放中，需要畫手勢
        {
            skillActive();   //進行手勢判斷
        }

        
        if (!isGameEnd)
        {
            checkDead();
        }
            
    }

    public IEnumerator enemyInView() {
        yield return 0;

        bool isEnemyIn = false;
        for (int i = 0; i < enemy.Count; i++)
        {
            float x = enemy[i].transform.position.x;
            if (Mathf.Abs(x - BattleCameraController.instance.transform.position.x) <= BackGroundController.instance.bgWidth/2 - 1)
            {
                isEnemyIn = true;
                if (x < nowTarget.transform.position.x)
                    nowTarget = enemy[i];
            }
        }
        playerAI.canAttack = isEnemyIn;
    }


    public void gameStart() {
        MusicHandler.instance.startMusic();
        isGameStop = false;
        isGameEnd = false;
        playerWalkIn();
        //enemyWalkIn();
    }

    public void playerWalkIn() {
      //  playerAnim.SetBool("isWalk", true);
       // playerBattling.walk(5f);
        playerBattling.hpBarController.gameObject.SetActive(true);
    }

    public void enemyWalkIn() {
        Wave[nowWave].transform.position = new Vector3(BattleCameraController.instance.transform.position.x + 30, -0.15f, 0);
        Wave[nowWave].SetActive(true);
        for (int i = 0; i < enemy.Count; i++) {
            (enemy[i] as EnemyBattling).state = 0;
            enemy[i].startAnim("isWalk");
        }
    }

    public void LoadLevelPrefab() {       
        LoadPlayerBattling();
        LoadEnemyBattling(selectedLevel);
        LoadLevelBackground();
    }

    public void LoadLevelBackground() {
        Instantiate(selectedLevel.backGround);
        BackGroundController.instance.setBackGround(selectedLevel.backGround);
    }

    public void LoadPlayerBattling() {
        //playerBattling = new GameObject("PlayerBattling").AddComponent<PlayerBattling>();   //建立PlayerBattling實體物件
        playerObject = Instantiate(Resources.Load("PlayerBattling") as GameObject);
        cameraControl.playerBattling = playerObject;
        playerObject.transform.position = playerPosition;
        playerAnim = playerObject.GetComponent<Animator>(); 
        playerBattling = playerObject.GetComponent<PlayerBattling>();
        PlayerAIManager.instance.player = playerBattling;
        playerAI = playerObject.GetComponent<PlayerAI>();
        playerBattling.loadInfo(Player.instance);                   //讀取Player資料放入PlayerBattling
        UIManager.instance.createHealthBar(playerBattling);
        playerBattling.hpBarController.gameObject.SetActive(true);
        for (int i = 0; i < skill.Length; i++) {
            for (int a = 0; a < playerBattling.skills[i].sectionAction.Count; a++) {
                skill[i].Add(playerBattling.skills[i].sectionAction[a].gesture);
            }
        }
        player.Add(playerBattling);
        SEManager.instance.loadPlayerSE(playerBattling);
    }

    public void LoadEnemyBattling(Level levelInfo) {    //將Level裡面儲存的Enemy資料建立實體，當作Wave1,2,3的Child，之後控制Wave1,2,3的Active狀態來管理Wave
        
        for (int i = 0; i < levelInfo.enemyWave1.Count; i++)
        {
            GameObject enemyPrefab = Instantiate(Resources.Load("Enemy/" + levelInfo.enemyWave1[i] + "/BattleEnemyPrefab") as GameObject , Wave[0].transform);
            enemyPrefab.transform.position = new Vector3(3f * i, -0.15f, 0);
            EnemyBattling e = enemyPrefab.GetComponent<EnemyBattling>();
            e.loadInfo((Resources.Load("Enemy/" + levelInfo.enemyWave1[i] + "/Info") as GameObject).GetComponent<Enemy>());
            UIManager.instance.createHealthBar(e);
            e.state = 2;
            e.loadAnimator();
            e.animator.speed = 1 / MusicHandler.instance.BPM;
            enemyWave[0].Add(enemyPrefab.GetComponent<EnemyBattling>());
        }

        int waveNum = 0;
        for (int i = 0; i < Wave.Length; i++)
        {
            Wave[i].SetActive(false);
        }
        enemy = enemyWave[waveNum];        
        nowWave = waveNum;
        changeNowTarget(enemy[0]);

        for (int i = 0; i < levelInfo.enemyWave2.Count; i++)
        {
            GameObject enemyPrefab = Instantiate(Resources.Load("Enemy/" + levelInfo.enemyWave1[i] + "/BattleEnemyPrefab") as GameObject, Wave[1].transform);
            enemyPrefab.transform.position = new Vector3(3f * i, -0.15f, 0);
            EnemyBattling e = enemyPrefab.GetComponent<EnemyBattling>();
            e.loadInfo((Resources.Load("Enemy/" + levelInfo.enemyWave1[i] + "/Info") as GameObject).GetComponent<Enemy>());
            UIManager.instance.createHealthBar(e);
            e.state = 2;
            e.loadAnimator();
            e.animator.speed = 1 / MusicHandler.instance.BPM;
            enemyWave[1].Add(enemyPrefab.GetComponent<EnemyBattling>());
        }

        for (int i = 0; i < levelInfo.enemyWave2.Count; i++)
        {
            GameObject enemyPrefab = Instantiate(Resources.Load("Enemy/" + levelInfo.enemyWave1[i] + "/BattleEnemyPrefab") as GameObject, Wave[2].transform);
            enemyPrefab.transform.position = new Vector3(3f * i, -0.15f, 0);
            EnemyBattling e = enemyPrefab.GetComponent<EnemyBattling>();
            e.loadInfo((Resources.Load("Enemy/" + levelInfo.enemyWave1[i] + "/Info") as GameObject).GetComponent<Enemy>());
            UIManager.instance.createHealthBar(e);
            e.state = 2;
            e.loadAnimator();
            e.animator.speed = 1 / MusicHandler.instance.BPM;
            enemyWave[2].Add(enemyPrefab.GetComponent<EnemyBattling>());
        }
    }

    public void LoadWave() {    //建立Wave1,2,3的GameObjectParent,之後把wave1,2,3的Enemy分別丟進去可以一起管理
        Wave[0] = new GameObject("Wave0");
        Wave[1] = new GameObject("Wave1");
        Wave[2] = new GameObject("Wave2");
    }

    public void WaveChange(int waveNum) {   //管理Wave狀態
        UIManager.instance.waveCleared();
        for (int i = 0; i < Wave.Length; i++) {
            Wave[i].SetActive(false);
        }
        enemy = enemyWave[waveNum];
        //Wave[waveNum].transform.position = new Vector3(BattleCameraController.instance.transform.position.x + 15, -0.15f, 0);
        AIManager.loadSprites(enemy, player);
        nowWave = waveNum;
        MusicHandler.instance.waitTiming(5);
        //enemyWalkIn();
        changeNowTarget(enemy[0]);
    }

    public void changeNowSkill(int skillNum)
    {
            nowSkill = skill[skillNum-1];
    }

    public void skillActive()
    {
        tap();
        slideToUp();
        slideToLeft();
        slideToDown();
        slideToRight();
        pressJudge();
    }

    public void tap() {
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (nowSkill[nowGesture] == 1 && !hadMoved)
                {
                    hadMoved = true;
                    castJudge();    //手勢正確的話判斷拍點是否正確
                }
            }
        }
        else if (Input.touchCount == 2) {
            if (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(1).phase == TouchPhase.Began)
            {
                if (nowSkill[nowGesture] == 1 && !hadMoved)
                {
                    hadMoved = true;
                    castJudge();    //手勢正確的話判斷拍點是否正確
                }
            }
        }
    }

    public void onSkillClicked(int skill)//???
    {
        if (activeSkill == 0 && MusicHandler.instance.isJudgeInterval() && playerBattling.skillTime[skill-1] <= 0 && playerAI.canAttack)
        {            
            nowMask = Instantiate(mask);
            nowMask.transform.position = Vector3.zero + new Vector3(BattleCameraController.instance.transform.position.x,0,1f);
            changeNowSkill(skill);
            activeSkill = skill;
            UIManager.instance.nowSkill = playerBattling.skills[skill - 1];
            UIManager.instance.gesturePrompt();
            hadMoved = true;
            hadHit = true;
            SEManager.instance.startSkill();
        }
    }

    public void onAttackClicked()
    {
        if (activeSkill == 0 && playerAI.canAttack)
        {
            attackJudge();                   
        }
            
    }

    public void slideToLeft()
    {
        if (Input.touchCount >= 1 && Input.GetTouch(0).deltaPosition.x / Time.deltaTime <= -200 && !hadMoved)    //向左滑
        {
            if (nowSkill[nowGesture] == 3)  //判斷手勢是否正確
            {
                hadMoved = true;
                castJudge();    //手勢正確的話判斷拍點是否正確
            }
        }
    }

    public void slideToRight()
    {

        if (Input.touchCount >= 1 && Input.GetTouch(0).deltaPosition.x / Time.deltaTime >= 200 && !hadMoved)    //向右滑
        {
            if (nowSkill[nowGesture] == 4)
            {
                castJudge();
            }
        }
    }

    public void slideToUp()
    {

        if (Input.touchCount >= 1 && Input.GetTouch(0).deltaPosition.y / Time.deltaTime >= 200 && !hadMoved)    //向上滑
        {
            if (nowSkill[nowGesture] == 5)
            {
                castJudge();
            }
        }
    }

    public void slideToDown()
    {

        if (Input.touchCount >= 1 && Input.GetTouch(0).deltaPosition.y / Time.deltaTime <= -200 && !hadMoved)    //向下滑
        {
            if (nowSkill[nowGesture] == 6)
            {
                MusicHandler.instance.rhythmJudge();
            }
        }
    }

    public void comboReset() {
        if (Combo == 0)
            return;
        Combo = 0;
        BattleHandler.instance.removeBuff(playerBattling, playerBattling.comboBuff);
        playerBattling.comboBuff = BattleHandler.instance.addBuff(0, playerBattling, 1, 1000);
    }

    public void skillReset() {
        nowGesture = 0;
        activeSkill = 0;
        skillEnegyBar = 0;    
    }

    public void timingControl() {  //Buff持續時間計時

            isTimingArrived = true;
            if (!isGameStop)
            {
                BattleHandler.instance.buffDurationTiming(playerBattling);  //所有Buff的剩餘時間-1
                for (int i = 0; i < enemy.Count; i++)   //所有Buff的剩餘時間-1
                {
                    BattleHandler.instance.buffDurationTiming(enemy[i]);
                    BattleHandler.instance.coolDownTiming(enemy[i]);
                }
            }
            BattleHandler.instance.coolDownTiming(playerBattling);

                 
            if (activeSkill != 0 && nowSkill[nowGesture] == 7) {
                castJudge();
            }
    }

    public void attackJudge() {
        hadMoved = true;    //剛剛觸碰螢幕的手指動過了
        if (MusicHandler.instance.isJudgeInterval() && playerAI.canAttack)
        {
            if (playerAI.attachEnemy.Count == 0)
            {
                playerAttackAnimation();
                StartCoroutine(playerAI.rushToEnemy(nowTarget.transform.position.x));
            }
            else
            {
                playerAttackAnimation();
            }
            SEManager.instance.playerAttack();
            if (accuracyCalculate() == 2 && !hadHit && !playerBattling.isBlock)
            {
                BattleHandler.instance.spriteAttack(playerBattling,nowTarget, "Enemy");   //呼叫BattleHandler普攻       
                addCombo();
                hadHit = true;
            }
            else
            {
                comboReset();   //Combo重置
            }
        }
    }

    public void playerAttackAnimation() {
        if (!playerAnim.GetBool("isATK"))
        {
            playerAnim.SetBool("isATK2", false);
            playerAnim.SetBool("isATK", true);
        }
        else {
            playerAnim.SetBool("isATK", false);
            playerAnim.SetBool("isATK2", true);
        }
    }

    public void castJudge() {   
        hadMoved = true;    //剛剛觸碰螢幕的手指動過了
                            // if (MusicHandler.instance.isJudgeInterval()) {
        if (activeSkill == 0)
            return;

        
            
            if (nowSkill[nowGesture] != 7)
                skillEnegyBar += accuracyCalculate();
            else if (isPress)
                skillEnegyBar += 10;

            UIManager.instance.removePromptOne();

            if (++nowGesture == nowSkill.Count)
            {//技能全部手勢接完       
                UIManager.instance.removePromptObj();   
                playerAnim.SetBool("Skill" + activeSkill + "_1", false);
              //  playerAnim.SetBool("Skill" + activeSkill + "_2", false);
                nowMask.GetComponent<Animator>().SetBool("isFadeOut", true);
                BattleHandler.instance.castSkill("Skill"+(nowGesture-1),playerBattling.skills[activeSkill - 1].sectionAction[nowGesture - 1], playerBattling, player, enemy, "Enemy");
                Combo += playerBattling.skills[activeSkill - 1].combo;
                playerBattling.skillTime[activeSkill - 1] = playerBattling.skills[activeSkill - 1].coolDown;
                skillReset();   //技能狀態重置
            }
            else {            
                playerAnim.SetBool("Skill" + activeSkill + "_"+ (nowGesture), true);
                playerAnim.SetBool("Skill" + activeSkill + "_2", true);
                BattleHandler.instance.castSkill( ("Skill" + (nowGesture-1)),playerBattling.skills[activeSkill - 1].sectionAction[nowGesture - 1], playerBattling, player, enemy, "Enemy");
            }
            hadHit = true;

    }

    public void checkDead() {
        if (playerBattling.CheckisDead())
        {
            isGameEnd = true;
            playerBattling.dead();
        }
        for (int i = 0; i < enemyWave[nowWave].Count; i++) {
            if (enemyWave[nowWave][i].CheckisDead())
            {
                playerAI.attachEnemy.Remove(enemyWave[nowWave][i].gameObject);
                enemyWave[nowWave][i].dead();
                enemyWave[nowWave].Remove(enemyWave[nowWave][i]);
                if (enemyWave[nowWave].Count > 0)
                {
                    changeNowTarget(enemy[0]);
                }
                else if (++nowWave < Wave.Length)
                {
                    shutButton();
                    WaveChange(nowWave);
                }
                else {
                    isGameEnd = true;
                    isGameStop = true;
                    Debug.Log("遊戲結束");
                    break;
                }
            }
        }
        
    }

    public void changeNowTarget(SpriteBattling target)
    {
        nowTarget = target;
    }

    public void pressJudge() {
        if (!isPress && !hadMoved && nowSkill[nowGesture] == 2 && Input.touchCount > 0)
        {
            isPress = true;
            castJudge();
        }
        else if (isPress && Input.touchCount == 0) {
            isPress = false;
            if (nowSkill[nowGesture] == 8)
                castJudge();
        }
    }

    public int accuracyCalculate()
    {
        float differTime = MusicHandler.instance.rhythmJudge();
        int i;
        for (i = 0; i < timingInterval.Length; i++) {
            if (differTime <= timingInterval[i])
                break;
        }
        return 2-i;
    }

    public void notHit() {
        if (!hadHit)//拍點內沒打
        {
            if (playerBattling.blockSuccess)
            {
                playerBattling.blockSuccess = false;
                addCombo();
                return;
            }
            if (activeSkill == 0 && !isGameStop)
            {
                comboReset();
                skillReset();
            }
            else
            {
                castJudge();
            }
        }
    }

    public void addCombo() {
        Combo++;
        if (Combo % 10 == 0)
        {
            BattleHandler.instance.removeBuff(playerBattling, playerBattling.comboBuff);
            playerBattling.comboBuff = BattleHandler.instance.addBuff(0, playerBattling, 1f + (float)Combo / 100f, 1000);
        }
    }

    public void loadAnimationClips() {

    }

    public void gamePause() {
        Time.timeScale = 0;
    }

    public void resumeGame() {
        Time.timeScale = 1;
    }

    public void shutButton() {
        isGameStop = true;
    }

    public void resumeButton() {
        isGameStop = false;
    }

    public void onBlockClicked() {
        if (MusicHandler.instance.isJudgeInterval())
        {
            if (accuracyCalculate() == 2 && !hadHit)
            {
                playerBattling.isBlock = true;                
            }
                
        }
    }
}
