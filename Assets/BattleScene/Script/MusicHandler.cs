using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class MusicHandler : MonoBehaviour{

    [SerializeField]
    #region music objects definition
    private Object music;
    public GameObject musicBox;
    private AudioSource Intro;
    private AudioSource Loop;
    private AudioSource nowAudio;
    private musicInfo musicInfo;
    public float BPM;
   public float Timing;
    public float offset;
    #endregion

    public bool isStart;

    public int rhythmCount;

    public static MusicHandler instance;

    public bool isLoop;  //用來控制音樂，判斷現在是Intro還是Loop
    
    public bool isTimingArrived;

    private  const float errorInterval=0.15f;//容錯範圍(radius)
                                            // Use this for initialization
    private const float judgeInterval = 0.2f;

    public int waitCount;

    private bool isWaveTextCreated;

    public UIManager uimanager;

    public bool rhythmStart;
    

    void Awake() {
        rhythmCount = 0;
        instance = this;
    }
    void Start () {
        uimanager = UIManager.instance;
	}
	
	// Update is called once per frame
	void Update () {
        if (!BattleManager.instance.isGameEnd)
        {
            rhythmControl();
            rhythmUIControl();
        }
           

        onTimingArrived();
    }

    public void rhythmUIControl() {
        if (isJudgeInterval())
            uimanager.shining.color = new Color(uimanager.shining.color.r, uimanager.shining.color.g, uimanager.shining.color.b, -(Mathf.Abs(nowAudio.time - Timing) / (BPM / 2f) - 1f));
        else
            uimanager.shining.color = new Color(uimanager.shining.color.r, uimanager.shining.color.g, uimanager.shining.color.b,0);
    }

    public void waitTiming(int count) {
        waitCount = count + 8 - rhythmCount;
    }

    //拍點到的時候傳回true，其他時間傳回false
    public bool onTimingArrived() {
        
        if (!isTimingArrived && Intro.isPlaying && Timing <= Intro.time)
        {
            AIControl();
            waitToStart();
            isTimingArrived = true;
            BattleManager.instance.timingControl();
            
            return true;
        }
        else if (!isTimingArrived && Timing <= Loop.time)
        {
            AIControl();
            waitToStart();
            isTimingArrived = true;
            BattleManager.instance.timingControl();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AIControl() {
        if (BattleManager.instance.isGameStop || BattleManager.instance.isGameEnd)
        {
            return;
        }
        else
        {
            AIManager.instance.nowStepChange();
            AIManager.instance.canHit();
            AIManager.instance.isAttackFinish();
            AIManager.instance.isSkillFinish();
        }
    }

    public void waitToStart() {
        if ((++rhythmCount) > 4)
            rhythmCount = 1;

        if (waitCount > 0)
        {
            if (waitCount - 1 == 4)
            {
                if (rhythmCount == 1)
                {
                    UIManager.instance.createWaveText();
                    waitCount--;
                }
            }

            else if (waitCount - 1 == 0)
            {
                if (rhythmCount == 1)
                {
                    UIManager.instance.startCount();
                    waitCount--;
                }

            }
            else
                waitCount--;
        }
    }

    public void loadMusic(string musicName) {
        isLoop = false;//先放intro
        music = Resources.Load("music/" + musicName);
        musicBox = GameObject.Instantiate(music) as GameObject;
        musicInfo = musicBox.GetComponent<musicInfo>();
        Intro = musicInfo.Intro;
        Loop = musicInfo.Loop;
        BPM = musicInfo.BPM;
        Timing = musicInfo.offSet;
        offset = musicInfo.offSet;
        Loop.loop = true;//讓LOOP循環播放
        
    }

    //撥放音樂
    public void startMusic() {
        //Debug.Log("playyyyyyyy");
        Intro.Play();
        nowAudio = Intro;
        Loop.PlayDelayed(Intro.clip.length);//等INTRO播完在換LOOP
    }

    //
    public bool rhythmControl() {
        if (TutorialController.instance == null)
        {
            if (!isWaveTextCreated && !isLoop && Intro.time >= (Timing - 8 * BPM))
            {
                isWaveTextCreated = true;
                UIManager.instance.createWaveText();
            }

            if (!isStart && !isLoop && Intro.time >= (Timing - 4 * BPM))
            {
                isStart = true;
                UIManager.instance.startCount();
            }

            else if (!isStart && isLoop && Loop.time >= (Timing - 4 * BPM))
            {
                isStart = true;
                UIManager.instance.startCount();
            }
        }

        if (Loop.time != 0 && !isLoop && !Intro.isPlaying)//換成LOOP
        {
            switchToLoop();
        }

        else if (Loop.time != 0 && isLoop && Timing - Loop.time > Loop.clip.length/2)//讓TIMING的值不會太大
        {
            timingLoop();
        }

     


        if (!isLoop && Intro.time >= Timing + errorInterval)//過完一次拍點了，進入下一個拍點
        {
            rhythmStart = true;
            updateTiming();
            return true;
        }

        else if (isLoop && Loop.time >= Timing + errorInterval)//過完一次拍點了，進入下一個拍點
        {
            rhythmStart = true;
            updateTiming();
            return true;
        }
        return false;
    }

    public void updateTiming() {
        BattleManager.instance.playerBattling.isBlock = false;
        BattleManager.instance.playerBattling.animator.SetBool("isBlock", false);
        BattleManager.instance.notHit();
        Timing += BPM;//TIMING換到下次拍點
        BattleManager.instance.hadHit = false;
        isTimingArrived = false;    //這個拍點結束，等待下個拍點到
    }

    public void timingLoop() {
        
        Debug.Log("TimingLoop");
        Timing -= Loop.clip.length;
    }

    public void switchToLoop()
    {
        nowAudio = Loop;
        Timing = 0f-(Intro.clip.length - offset) % BPM;
        Debug.Log("11111");
        isLoop = true;
        
    }

    //有無打在拍點上
    public float rhythmJudge() {
        if (Intro.isPlaying) //播放Intro的時候的拍點判定
        {
            return (Mathf.Abs(Intro.time - Timing));
        }
        else//播放Loop的時候的拍點判定
        {
            return (Mathf.Abs(Loop.time - Timing));
        }
    }

    public bool isJudgeInterval()
    {
        if (BattleManager.instance.isGameStop)
            return false;

        if (Intro.isPlaying && Mathf.Abs(Intro.time - Timing) <= judgeInterval) //播放Intro的時候的拍點判定
        {
            return true;
        }
        else if (Mathf.Abs(Loop.time - Timing) <= judgeInterval)    //播放Loop的時候的拍點判定
        {
            return true;
        }
        else    //沒打中拍點
        {
            return false;
        }
    }

    public void musicPause() {
        Intro.Pause();
        Loop.Pause();
    }

    public void musicResume() {
        Intro.UnPause();
        Loop.UnPause();
    }

    public IEnumerator musicStop() {
        while (nowAudio.volume > 0)
        {
            yield return null;
            float f = (nowAudio.volume - 0.8f * Time.deltaTime);
            if (f < 0)
                f = 0;
            nowAudio.volume = f;
        }
        musicPause();
    }
}
