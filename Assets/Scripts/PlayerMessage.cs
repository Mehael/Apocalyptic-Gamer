using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMessage : MonoBehaviour {
    public static PlayerMessage instance;
    public Text text;
    public Transform messagePanel;
    bool isActive = false;

    public void Awake()
    {
        instance = this;
    }

    public void Show(List<string> inputList)
    {
        if (Application.loadedLevel <= 1) return;
        if (Random.Range(0, 10) > 1) return;

        Show(inputList[Random.Range(0, inputList.Count - 1)]);
    }

    public void Show(string message, bool dontHide = false)
    {
        messagePanel.gameObject.SetActive(true);
        text.text = message;

        if (dontHide) return;

        StartCoroutine(WaitAndActive());
    }

    IEnumerator WaitAndActive()
    {
        yield return new WaitForSeconds(0.5f);
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

        if (Input.anyKey)
        {
            messagePanel.gameObject.SetActive(false);
            isActive = false;
        }
    }
}
