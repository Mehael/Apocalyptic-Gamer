using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMessage : MonoBehaviour {
    public static PlayerMessage instance;
    public Text text;
    public Transform messagePanel;
    public float showTime = 3f;
    float timeToHide = 0f;
    bool isActive = false;

    public void Awake()
    {
        instance = this;
    }

    public void Show(string message, bool dontHide = false)
    {
        messagePanel.gameObject.SetActive(true);
        text.text = message;

        if (dontHide) return;

        timeToHide = showTime;
        isActive = true;
    }

    public void Hide()
    {
        messagePanel.gameObject.SetActive(false);
        isActive = false;
    }

    void Update()
    {
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
