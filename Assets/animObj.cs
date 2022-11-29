using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animObj : MonoBehaviour {
    Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
    }

    public void stopAnim(string animName)
    {
        anim.SetBool(animName, false);
    }

    public void changeAnimInt(string name)
    {
        anim.SetInteger(name, -1);
    }
}
