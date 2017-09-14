using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shining : MonoBehaviour {
    public Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
    }

    public void setBool() {
        animator.SetBool("shining",false);
    }
}
