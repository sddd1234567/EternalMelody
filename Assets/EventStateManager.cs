using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventStateManager : MonoBehaviour {
    public static EventStateManager instance;
  
    public List<EventState> stateMachine;
    // Use this for initialization

    void Awake() {
        instance = this;
    }

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
