using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {
    public static Player instance;      //遊戲裡永遠只會有一個Player，遊戲開始實建立實體供其他Script讀取

    //public int CRI;//critical
    public int PEN;
    public List<Skill> skills;
    public Weapon weapon;
    public int gold;
    public int temperStar;


    public int HPBeforeEquiped;
    public int ATKBeforeEquiped;
    public int DEFBeforeEquiped;
    public int CRIBeforeEquiped;
    public int PENBeforeEquiped;

    void Awake() {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }


    // Use this for initialization
    void Start () {
		    
	}
	
	// Update is called once per frame
	void Update () {
		
	}
   

    public override void LevelUp()
    {
        base.LevelUp();
        HPUp();
        ATKUp();
        DEFUp();
        CRIUp();
        PENUp();
    }
    
    private void HPUp()//not yet
    {

    }
    private void ATKUp()//not yet
    {

    }
    private void DEFUp()//not yet
    {

    }
    private void PENUp()//not yet
    {

    }
    private void CRIUp()//not yet
    {

    }

    public void equip(Weapon wp)
    {
        weapon = wp;
        HP =wp.HP+ HPBeforeEquiped ;
        ATK =wp.ATK + ATKBeforeEquiped;
        DEF =wp.DEF +DEFBeforeEquiped;
        CRI = wp.CRI+ CRIBeforeEquiped;
        PEN = wp.PEN + PENBeforeEquiped;
    }

}
