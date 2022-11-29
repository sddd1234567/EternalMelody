using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountInfo : MonoBehaviour {
    public int exp;
    public int gold;
    public int skillChip;

    public Image playerIcon;
    public Text lvText;
    public Text goldText;
    public Text skillChipText;
    public Text levelName;
    public Text title;

    public Text boardGold;
    public Text boardSkillChip;
    public Text firstTimeText;
    public static AccountInfo instance;

    public Slider expBar;

    public bool isWin;

    void Awake() {
        instance = this;
    }

    void Start() {
        title.text = "";
    }

    void FixedUpdate() {
        boardGold.text = gold.ToString();
        boardSkillChip.text = skillChip.ToString();
    }

    public void loadAccountInfo(SpriteRenderer icon, string lName, string titlee, bool winOrLose, bool isFirstTime) {
        if (isFirstTime)
        {
            firstTimeText.text += BattleManager.instance.selectedLevel.aw.temperStar;
        }
        isWin = winOrLose;
        playerIcon.overrideSprite = Resources.Load<UnityEngine.Sprite>("Weapon/" + Player.instance.weapon + "/PlayerIcon/plrIcon");
        levelName.text = lName;
        title.text = titlee;

        expBar.value = (float)Player.instance.nowEXP / (float)Player.instance.requiredEXP[Player.instance.level - 1];
        lvText.text = "Lv." + Player.instance.level;
        goldText.text = "x" + gold;
        skillChipText.text = "x" + skillChip;

        if (isWin)
            exp += BattleManager.instance.selectedLevel.aw.exp;
        if (exp + Player.instance.nowEXP >= Player.instance.requiredEXP[Player.instance.level - 1])
        {
            levelUp(exp - Player.instance.requiredEXP[Player.instance.level - 1], Player.instance.level);
        }
        else
        {
            Player.instance.nowEXP += exp;
            StartCoroutine(expBarAnim(false, 0, Player.instance.nowEXP, Player.instance.level));
        }
        Player.instance.gold += gold;
        Player.instance.skillChip += skillChip;
    }

    public void levelUp(int expp, int nowLV) {
        Player.instance.level++;
        if (expp >= Player.instance.requiredEXP[Player.instance.level - 1])
        {
            //lvText.text = "Lv." + Player.instance.level;
            levelUp(expp - Player.instance.requiredEXP[Player.instance.level - 1], nowLV);
        }
        else
        {
            StartCoroutine(expBarAnim(true, (Player.instance.level - nowLV), expp, nowLV));
            Player.instance.nowEXP = expp;
        }
    }

    public IEnumerator expBarAnim(bool isLevelUp, int upLevel, int targetExp, int nowLV) {
        
        int l = upLevel;
        float nowExp = Player.instance.nowEXP;
        int lv = nowLV;
        yield return new WaitForSeconds(1);
        while (l != 0 || nowExp < targetExp)
        {
            yield return null;
            nowExp += 500 * Time.deltaTime;
            expBar.value = nowExp / (float)Player.instance.requiredEXP[lv - 1];
            if (nowExp >= Player.instance.requiredEXP[lv - 1])
            {
                lv++;
                nowExp -= Player.instance.requiredEXP[lv - 1];
                l--;
                lvText.text = "Lv." + lv;
            }
        }
    }
}
