using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveTextAnimation : UIScript {
    public void setText(int waveNum) {
        GetComponent<TextMeshProUGUI>().text += waveNum;
    }
}
