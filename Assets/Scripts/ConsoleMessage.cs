using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleMessage : MonoBehaviour {
    public static ConsoleMessage instance;
    public Text text;
    public Transform messagePanel;
    public float showTime = 2f;
    float timeToHide = 0f;
    bool isActive = false;

    public void Awake()
    {
        instance = this;
    }

    public void Show(string message) {
        messagePanel.gameObject.SetActive(true);
        text.text = message;
        timeToHide = showTime;
        isActive = true;
    }
	
	void Update () {
        if (isActive == false) return;

        if (timeToHide > 0)
            timeToHide -= Time.deltaTime;
        else
        {
            messagePanel.gameObject.SetActive(false);
            isActive = false;
        }
	}
}
