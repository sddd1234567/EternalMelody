using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveTextAnimation : UIScript {
    public void setText(int waveNum) {
        GetComponent<Text>().text += waveNum;
    }
}