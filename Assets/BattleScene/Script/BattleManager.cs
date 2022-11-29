using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour {
    #region skills objects definition
    public int activeSkill;
  //  public List<List<int>> skill; //技能的畫法 1：點擊 2：壓2拍 3：向左劃 4：向右劃　５：向上劃　６：向下劃     取自技能的資料
  //  public List<int> nowSkill;  //按下技能按鍵後這個會=那個技能，之後方便做手勢判斷
    public List<Skill> skilll;
    public int nowGesture;  //紀錄現在是技能的第幾下
    public float skillEnegyBar;
    #endregion

    #region targets objects definition
    public List<SpriteBattling> enemy;
    public List<SpriteBattling> player;
    public PlayerBattling playerBattling;
    public PlayerState playerState;
    public SpriteBattling nowTarget;
    #endregion    
    public AccountInfo accounting;
    public GameObject endUI;
    public GameObject fakeIcon;
    public SpriteRenderer playerIcon;

    public AudioClip attackSE;

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

    private bool isFound;

    public GameObject skillBarObj;

    public SkillBarUI skillBar;

    public AudioSource ao;

    public GameObject gold;
    public GameObject skillChip;
    
    
    
    // Use this for initialization

    void Awake() {
        ao = gameObject.AddComponent<AudioSource>();
        fakeIcon = new GameObject();
        fakeIcon.SetActive(false);
        playerIcon = fakeIcon.AddComponent<SpriteRenderer>();
        selectedLevel = Resources.Load(missionController.path) as Level;
        enemyWave = new List<SpriteBattling>[3];
        enemyWave[0] = new List<SpriteBattling>();
        enemyWave[1] = new List<SpriteBattling>();
        enemyWave[2] = new List<SpriteBattling>();
        //skill = new List<List<int>>();
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
            UIManager.instance.coolDownControl(playerBattling);
            checkDead();
        }
            
    }

    public IEnumerator enemyInView() {
        yield return 0;

        bool isEnemyIn = false;
        for (int i = 0; i < enemy.Count; i++)
        {
            float x = enemy[i].transform.position.x;
            if (Mathf.Abs(x - BattleCameraController.instance.transform.position.x) <= BattleCameraController.instance.farBGWidth/2 - 1)
            {
                isEnemyIn = true;
                if (x < nowTarget.transform.position.x)
                    nowTarget = enemy[i];
            }
        }
        playerState.canAttack = isEnemyIn;
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
        // playerBattling.walk(5f)walk(5f);
        playerBattling.hpBarController.gameObject.SetActive(true);
    }

    public void enemyWalkIn() {
        Wave[nowWave].transform.position = new Vector3(BattleCameraController.instance.transform.position.x + 33.8f, -0.15f, 0);
        Wave[nowWave].SetActive(true);
        for (int i = 0; i < enemy.Count; i++) {
            (enemy[i] as EnemyBattling).state = 0;
            enemy[i].startAnim("isWalk");
        }
    }

    public void LoadLevelPrefab() {       
        LoadPlayerBattling();
        LoadEnemyBattling(selectedLevel);
        StartCoroutine(LoadLevelBackground());
    }

    public IEnumerator LoadLevelBackground() {
        while (BattleCameraController.instance == null)
        {
            yield return null;
        }
        BattleCameraController.instance.setViewField(Instantiate(selectedLevel.farBackGround, BattleCameraController.instance.transform));
        Instantiate(selectedLevel.middleBackGround,BackGroundController.instance.farBGParent.transform);
      //  Instantiate(selectedLevel.frontBackGround[Random.Range(0, selectedLevel.frontBackGround.Count)], BackGroundController.instance.frontBGParent.transform);
        BackGroundController.instance.setFrontBackGround(selectedLevel.frontBackGround);
        BackGroundController.instance.setBackGround(selectedLevel.middleBackGround);
    }

    public void LoadPlayerBattling() {
        //playerBattling = new GameObject("PlayerBattling").AddComponent<PlayerBattling>();   //建立PlayerBattling實體物件
        playerObject = Instantiate(Resources.Load("PlayerBattling") as GameObject);
        cameraControl.playerBattling = playerObject;
        playerObject.transform.position = playerPosition;
        playerAnim = playerObject.GetComponent<Animator>(); 
        playerBattling = playerObject.GetComponent<PlayerBattling>();
        playerIcon.sprite = playerBattling.GetComponent<SpriteRenderer>().sprite;
        PlayerStateManager.instance.player = playerBattling;
        playerState = playerObject.GetComponent<PlayerState>();
        PlayerStateManager.instance.playerState = playerState;
        Player.instance.equip(Player.instance.weapon);
        playerBattling.loadInfo(Player.instance);                   //讀取Player資料放入PlayerBattling
        UIManager.instance.createHealthBar(playerBattling);
        playerBattling.hpBarController.gameObject.SetActive(true);
        skilll = playerBattling.skills;
        UIManager.instance.loadIcon(skilll);
        player.Add(playerBattling);
        SEManager.instance.loadPlayerSE(playerBattling);
    }

    public void LoadEnemyBattling(Level levelInfo) {    //將Level裡面儲存的Enemy資料建立實體，當作Wave1,2,3的Child，之後控制Wave1,2,3的Active狀態來管理Wave
        
        for (int i = 0; i < levelInfo.enemyWave1.Count; i++)
        {
            //Debug.Log(levelInfo.enemyWave1[i].enemyName);
            GameObject enemyPrefab = Instantiate(Resources.Load("Enemy/" + levelInfo.enemyWave1[i].enemyName + "/BattleEnemyPrefab") as GameObject , Wave[0].transform);
            enemyPrefab.transform.position = new Vector3(3f * i, -0.15f, 0);
            EnemyBattling e = enemyPrefab.GetComponent<EnemyBattling>();
            e.loadInfo((Resources.Load("Enemy/" + levelInfo.enemyWave1[i].enemyName + "/Info") as GameObject).GetComponent<Enemy>());
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
            GameObject enemyPrefab = Instantiate(Resources.Load("Enemy/" + levelInfo.enemyWave2[i].enemyName + "/BattleEnemyPrefab") as GameObject, Wave[1].transform);
            enemyPrefab.transform.position = new Vector3(3f * i, -0.15f, 0);
            EnemyBattling e = enemyPrefab.GetComponent<EnemyBattling>();
            e.loadInfo((Resources.Load("Enemy/" + levelInfo.enemyWave2[i].enemyName + "/Info") as GameObject).GetComponent<Enemy>());
            UIManager.instance.createHealthBar(e);
            e.state = 2;
            e.loadAnimator();
            e.animator.speed = 1 / MusicHandler.instance.BPM;
            enemyWave[1].Add(enemyPrefab.GetComponent<EnemyBattling>());
        }

        for (int i = 0; i < levelInfo.enemyWave3.Count; i++)
        {
            GameObject enemyPrefab = Instantiate(Resources.Load("Enemy/" + levelInfo.enemyWave3[i].enemyName + "/BattleEnemyPrefab") as GameObject, Wave[2].transform);
            enemyPrefab.transform.position = new Vector3(3f * i, -0.15f, 0);
            EnemyBattling e = enemyPrefab.GetComponent<EnemyBattling>();
            e.loadInfo((Resources.Load("Enemy/" + levelInfo.enemyWave3[i].enemyName + "/Info") as GameObject).GetComponent<Enemy>());
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
        playerState.attachEnemy.Clear();
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
        if (skilll[skillNum - 1].skillName != "none")
            activeSkill = skillNum;
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
                if (skilll[activeSkill -1].sectionAction[nowGesture].gesture == 1 && !hadMoved)
                {
                    hadMoved = true;
                    castJudge();    //手勢正確的話判斷拍點是否正確
                }
            }
        }
        else if (Input.touchCount == 2) {
            if (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(1).phase == TouchPhase.Began)
            {
                if (skilll[activeSkill -1].sectionAction[nowGesture].gesture == 1 && !hadMoved)
                {
                    hadMoved = true;
                    castJudge();    //手勢正確的話判斷拍點是否正確
                }
            }
        }
    }

    public void onSkillClicked(int skill)//???
    {
        if (activeSkill == 0 && MusicHandler.instance.isJudgeInterval() && playerBattling.skillTime[skill-1] <= 0 && playerState.canAttack && skilll[skill-1].skillName != "none")
        {
            skillBar = UIManager.instance.createUIObj(skillBarObj, Vector3.zero + Vector3.down * 5).GetComponent<SkillBarUI>();
            skillBar.transform.localScale = Vector3.one * 3f;
            nowMask = Instantiate(mask);
            nowMask.transform.position = Vector3.zero + new Vector3(BattleCameraController.instance.transform.position.x,-0,1f);
            changeNowSkill(skill);
            activeSkill = skill;
            UIManager.instance.nowSkill = skilll[skill-1];
            UIManager.instance.gesturePrompt();
            hadMoved = true;
            hadHit = true;
            SEManager.instance.startSkill();
        }
    }

    public void onAttackClicked()
    {
        if (activeSkill == 0 && playerState.canAttack && MusicHandler.instance.isJudgeInterval())
        {
            ao.PlayOneShot(attackSE, 0.5f);
            attackJudge();                   
        }
            
    }

    public void slideToLeft()
    {
        if (Input.touchCount >= 1 && Input.GetTouch(0).deltaPosition.x / Time.deltaTime <= -200 && !hadMoved)    //向左滑
        {
            if (skilll[activeSkill -1].sectionAction[nowGesture].gesture == 3)  //判斷手勢是否正確
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
            if (skilll[activeSkill -1].sectionAction[nowGesture].gesture == 4)
            {
                castJudge();
            }
        }
    }

    public void slideToUp()
    {

        if (Input.touchCount >= 1 && Input.GetTouch(0).deltaPosition.y / Time.deltaTime >= 200 && !hadMoved)    //向上滑
        {
            if (skilll[activeSkill -1].sectionAction[nowGesture].gesture == 5)
            {
                castJudge();
            }
        }
    }

    public void slideToDown()
    {

        if (Input.touchCount >= 1 && Input.GetTouch(0).deltaPosition.y / Time.deltaTime <= -200 && !hadMoved)    //向下滑
        {
            if (skilll[activeSkill -1].sectionAction[nowGesture].gesture == 6)
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

                 
            if (activeSkill != 0 && skilll[activeSkill-1].sectionAction[nowGesture].gesture == 7) {
                castJudge();
            }
    }

    public void attackJudge() {
        hadMoved = true;    //剛剛觸碰螢幕的手指動過了
        if (MusicHandler.instance.isJudgeInterval() && playerState.canAttack)
        {
            playerBattling.nowPlayerAttackState++;
            playerAttackAnimation();

            SEManager.instance.playerAttack();
            if (accuracyCalculate() >= 1 && !hadHit && !playerBattling.isBlock)
            {
                BattleHandler.instance.spriteAttack(playerBattling,nowTarget, "Enemy");   //呼叫BattleHandler普攻       
                addCombo();
                hadHit = true;
            }
            else
            {
                playerBattling.missTextCreate("Miss");
                comboReset();   //Combo重置
            }
        }
    }

    public void playerAttackAnimation() {
        playerState.stateChange(2);
        playerBattling.walk(0f);
        playerAnim.SetBool("isWalk", false);
        /*  if (!playerAnim.GetBool("isATK"))
          {
              playerAnim.SetBool("isATK2", false);
              playerAnim.SetBool("isATK", true);
          }
          else {
              playerAnim.SetBool("isATK", false);
              playerAnim.SetBool("isATK2", true);
          }*/

        if (playerAnim.GetBool("isATK1"))
        {
            playerAnim.SetBool("isATK1", false);
            playerAnim.SetBool("isATK2", true);
        }
        else if (playerAnim.GetBool("isATK2"))
        {
            playerAnim.SetBool("isATK2", false);
            playerAnim.SetBool("isATK3", true);
        }
        else if (playerAnim.GetBool("isATK3"))
        {
            playerAnim.SetBool("isATK3", false);
            playerAnim.SetBool("isATK4", true);
        }

        else
        {
            playerAnim.SetBool("isATK4", false);
            playerAnim.SetBool("isATK1", true);
        }
     //   playerAnim.SetBool("isATK" + playerBattling.nowPlayerAttackState, true);
     //     if (playerBattling.nowPlayerAttackState == 4)
     //         playerBattling.nowPlayerAttackState = 0;
    }

    public void castJudge() {   
        hadMoved = true;    //剛剛觸碰螢幕的手指動過了
                            // if (MusicHandler.instance.isJudgeInterval()) {
        if (activeSkill == 0)
            return;

        if (skilll[activeSkill-1].sectionAction[nowGesture].gesture != 7)
        {
            if (accuracyCalculate() == 2)
                skillBar.plusValue((float)100 / (float)skilll[activeSkill-1].sectionAction.Count);
            else if (accuracyCalculate() == 1)
                skillBar.plusValue((float)100 / (2 * (float)skilll[activeSkill - 1].sectionAction.Count));
            //skillEnegyBar += (float)100 / (float)nowSkill.Count;
        }
        else if (isPress)
            skillBar.plusValue((float)100 / (float)skilll[activeSkill-1].sectionAction.Count);
        //skillEnegyBar += (float)100 / (float)nowSkill.Count;

        UIManager.instance.removePromptOne();

            if (++nowGesture == skilll[activeSkill-1].sectionAction.Count)
            {//技能全部手勢接完       
                
                //Debug.Log("技能條 = " + skillEnegyBar);
                UIManager.instance.removePromptObj();   
                playerAnim.SetBool("Skill" + activeSkill + "_2", true);
                playerAnim.SetBool("Skill" + activeSkill + "_1", false);
                nowMask.GetComponent<Animator>().SetBool("isFadeOut", true);
            if (skillBar.targetValue >= 0.7f)
            {
                BattleHandler.instance.castSkill("Skill" + (nowGesture - 1), playerBattling.skills[activeSkill - 1].sectionAction[nowGesture - 1], playerBattling, player, enemy, "Enemy", true);
                Combo += skilll[activeSkill - 1].combo;
            }
            else
            {
                BattleHandler.instance.castSkill("Skill" + (nowGesture - 1), playerBattling.skills[activeSkill - 1].sectionAction[nowGesture - 1], playerBattling, player, enemy, "Enemy", false);
            }

            playerBattling.skillTime[activeSkill - 1] = playerBattling.skills[activeSkill - 1].coolDown;
            skillBar.end = true;
            skillBar = null;
            skillReset();   //技能狀態重置
            }
            else {            
                playerAnim.SetBool("Skill" + activeSkill + "_1", true);
                playerAnim.SetBool("Skill" + activeSkill + "_2", false);
                BattleHandler.instance.castSkill( ("Skill" + (nowGesture-1)),playerBattling.skills[activeSkill - 1].sectionAction[nowGesture - 1], playerBattling, player, enemy, "Enemy",false);
            }
            hadHit = true;

    }

    public void checkDead() {
        if (playerBattling.CheckisDead())
        {
            isGameEnd = true;
            playerBattling.dead();
            loseGame();
        }
        for (int i = 0; i < enemyWave[nowWave].Count; i++) {
            if (enemyWave[nowWave][i].CheckisDead())
            {
                StartCoroutine(drops(enemyWave[nowWave][i]));
                playerState.attachEnemy.Remove(enemyWave[nowWave][i].gameObject);
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
                   // Debug.Log("遊戲結束");                    
                    winGame();
                    break;
                }
            }
        }
    }

    public IEnumerator drops(SpriteBattling enemy) {
        EnemyBattling em = enemy as EnemyBattling;
        int goldd = em.gold;
        int skillChipp = em.skillChip;
        accounting.gold += goldd;
        accounting.skillChip += skillChipp;
        accounting.exp += em.exp;
        Vector3 pos = em.transform.position;
        yield return null;
        if (em.gold != 0)
        {
            GameObject g = Instantiate(gold);
            g.transform.position = new Vector3(pos.x, gold.transform.position.y, gold.transform.position.z);
            yield return new WaitForSeconds(0.1f);
        }
        if (em.skillChip != 0)
        {
            GameObject g = Instantiate(skillChip);
            g.transform.position = new Vector3(pos.x, skillChip.transform.position.y, skillChip.transform.position.z);
        }
    }


    public void winGame() {
        PlayerStateManager.instance.isGameStart = false;
        StartCoroutine(MusicHandler.instance.musicStop());
        StartCoroutine(SEManager.instance.playSE(SEManager.instance.winningSE));
        Player.instance.gold += selectedLevel.aw.gold;
        Player.instance.skillChip += selectedLevel.aw.skillChip;
        if (Player.instance.levelIndexs[selectedLevel.levelIndex] == 0)
        {            
            Player.instance.temperStar += selectedLevel.aw.temperStar;
            endUICreate(playerIcon, selectedLevel.levelName, "VICTORY", true, true);
        }
        else
        {
            endUICreate(playerIcon, selectedLevel.levelName, "VICTORY", true, true);
        }
        
    }

    public void loseGame() {
        PlayerStateManager.instance.isGameStart = false;
        StartCoroutine(MusicHandler.instance.musicStop());
        StartCoroutine(SEManager.instance.playSE(SEManager.instance.defeatSE));
        endUICreate(playerIcon, selectedLevel.levelName, "DEFEAT", false,false);
    }

    public void endUICreate(SpriteRenderer icon, string levelName, string winOrLose ,bool isWin,bool isFirstTime) {
        accounting.loadAccountInfo(icon, levelName, winOrLose, isWin,isFirstTime);
        endUI.gameObject.SetActive(true);
        endUI.transform.SetAsLastSibling();
    }

    public void changeNowTarget(SpriteBattling target)
    {
        nowTarget = target;
    }

    public void pressJudge() {
        if (!isPress && !hadMoved && skilll[activeSkill-1].sectionAction[nowGesture].gesture == 2 && Input.touchCount > 0)
        {
            isPress = true;
            castJudge();
        }
        else if (isPress && Input.touchCount == 0) {
            isPress = false;
            if (skilll[activeSkill-1].sectionAction[nowGesture].gesture == 8)
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

    public void pause() {
        GameObject obj = UIManager.instance.createUIObj(UIManager.instance.pauseUI, UIManager.instance.pauseUI.transform.position);
        obj.transform.localScale = Vector3.one;
        gamePause();
    }

    public void gamePause() {
        MusicHandler.instance.musicPause();
        Time.timeScale = 0;
    }

    public void resume(GameObject obj)
    {
        resumeGame();
        Destroy(obj);
    }

    public void resumeGame() {
        MusicHandler.instance.musicResume();
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
