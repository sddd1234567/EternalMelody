using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBarUI : MonoBehaviour {
    public Slider bar;
    public float targetValue;
    public bool end;
    // Use this for initialization

    void Awake()
    {
        bar = GetComponent<Slider>();
        bar.value = 0;
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (end && bar.value == targetValue)
            Destroy(gameObject);
    }

    public void plusValue(float value) {
        Debug.Log("plus = "+ value);
        targetValue += value / 100f;
        StartCoroutine(smoothluPlus());
    }

    IEnumerator smoothluPlus() {
        while (bar.value != targetValue)
        {
            yield return null;
            if (bar.value >= targetValue)
            {                
                break;
            }
            bar.value += (float)5 * Time.deltaTime;
        }
        bar.value = targetValue;
    }
}
