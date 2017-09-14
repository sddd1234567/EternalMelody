using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    public void destroyObj() {
        Destroy(gameObject);
    }

    public void setText(string str) {
        GetComponent<Text>().text = str;
    }

    public void setTextColor() {

    }

    public void setPosition(Vector3 pos) {
        GetComponent<RectTransform>().position = pos;
    }

    public void setScale(Vector3 sc) {
        GetComponent<RectTransform>().localScale = sc;
    }
}
