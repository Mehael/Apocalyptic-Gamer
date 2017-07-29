using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenColors : MonoBehaviour {
    public enum Power {zerodivision, none, gray, full, overcharge};
    public Power startPower = Power.full;
    private Power currentPower;
    public Transform offedScreen;
    public Text ScreenState;

    void Start() {
        SetState(startPower);
        //DontDestroyOnLoad(this);
    }

    private void SetState(Power newPower)
    {
        if ((newPower == Power.overcharge) || (newPower == Power.zerodivision)) return;

        currentPower = newPower;
        if (currentPower == Power.none)
        {
            offedScreen.gameObject.SetActive(true);
            ScreenState.text = "OFF";
            return;
        }
        offedScreen.gameObject.SetActive(false);

        if (currentPower == Power.gray)
        {
            foreach (var tile in Board.current.cells.Values)
                tile.GrayTint();
            ScreenState.text = "GRAY";
        }
        else
        {
            foreach (var tile in Board.current.cells.Values)
                tile.ColorTint();
            ScreenState.text = "COLOR";
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus) || Input.GetMouseButtonDown(1))
            SetState(currentPower - 1);
        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetMouseButtonDown(0))
            SetState(currentPower + 1);
    }
}
