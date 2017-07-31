using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleMessage : MonoBehaviour {
    public static ConsoleMessage instance;
    public GameObject LevelMarkerPanel;
    public List<GameObject> LevelMarkers = new List<GameObject>();

    public Text text;
    public Transform messagePanel;
    public float showTime = 1.5f;
    float timeToHide = 0f;
    bool isActive = false;

    public void Awake()
    {
        instance = this;
    }

    bool isWaiting = false;
    public void Show(string message, bool isNewLevel = false)
    {
        messagePanel.gameObject.SetActive(true);
        text.text = message;
        timeToHide = showTime;
        isActive = true;

        if (isNewLevel)
        {
            isWaiting = true;
            LevelMarkerPanel.SetActive(true);
            var curLvel = Application.loadedLevel;
            for (int i = 0; i < LevelMarkers.Count-1; i++)
                if (i < curLvel)
                    LevelMarkers[i].SetActive(true);
        }
    }
	
	void Update () {
        if (isActive == false) return;

        if (timeToHide > 0)
            timeToHide -= Time.deltaTime;
        else
        {
            if (isWaiting == false)
               Hide();
            else if (Input.anyKey)
            {
                Hide();
                isWaiting = false;
            }

        }
    }

    private void Hide()
    {
        messagePanel.gameObject.SetActive(false);
        LevelMarkerPanel.SetActive(false);
        isActive = false;
    }
}
