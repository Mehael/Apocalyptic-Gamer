using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Final : Menu {
    public GameObject bestBody;
    public GameObject notBestBody;

    public GameObject bestLavel;
    public GameObject notBestLabel;

    public Text KeysLabel;

    bool isBEST = false;
    void Start () {
        Energy.instance.Start();
        Screen.instance.Start();

        PlayerMessage.instance.Show("I DID IT! Yeeeeas!", true);
        KeysLabel.text = "Keys:" + HardDoorsCounter.KeysCollected + 
            "\\" + HardDoorsCounter.KeysAtAll;

        if (HardDoorsCounter.KeysCollected == HardDoorsCounter.KeysAtAll)
        {
            isBEST = true;
            bestLavel.SetActive(true);
            notBestLabel.SetActive(false);

            bestBody.SetActive(true);
            notBestBody.SetActive(false);
        }
        else
        {
            bestLavel.SetActive(false);
            notBestLabel.SetActive(true);

            bestBody.SetActive(false);
            notBestBody.SetActive(true);
        }


    }

    public override void Gray()
    {
        bestBody.SetActive(false);
        notBestBody.SetActive(false);

        foreach (var i in coloredParts)
            i.SetActive(false);
    }

    public override void Color()
    {
        if (isBEST)
            bestBody.SetActive(true);
        else
            notBestBody.SetActive(true);

        foreach (var i in coloredParts)
            i.SetActive(true);
    }

    override public void SelectOption(int v, bool isNatural = false) {    }

    void Update () {  }
}
