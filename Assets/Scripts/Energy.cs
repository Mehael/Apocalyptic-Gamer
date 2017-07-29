using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour {
    private Power energState;
    private int currentEnergy;
    public Text energyLabel;
    public static Energy instance;

    public int switchOnColorModeCost = 1;
    public int switchOnGrayModeCost = 1;

    public int moveWithoutScreenCost = 1;
    public int moveWithGrayModeCost = 2;
    public int moveWithColorModeCost = 3;

    private void Awake()
    {
        instance = this;
    }

    public void SetPowerState(Power newState)
    {
        if (energState != Power.zerodivision && energState == newState) return;
            energState = newState;

        if (energState == Power.gray)
            RemoveEnergy(moveWithGrayModeCost);
        if (energState == Power.full)
            RemoveEnergy(moveWithColorModeCost);
    }

    public void Move()
    {
        if (energState == Power.gray)
            RemoveEnergy(switchOnGrayModeCost);
        else if (energState == Power.full)
            RemoveEnergy(switchOnColorModeCost);
        else
            RemoveEnergy(moveWithoutScreenCost);
    }

    private void Start()
    {  
        SetEnergy(Board.current.StartEnergyForLevel);
    }

    private void SetEnergy(int value)
    {
        if (value < 0)
        {
            value = 0;
            Application.LoadLevel(Application.loadedLevel);
        }
        else
            currentEnergy = value;

        energyLabel.text = currentEnergy.ToString();
    }

    public void RemoveEnergy(int delta)
    {
        SetEnergy(currentEnergy - delta);
    }
}
