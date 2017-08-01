using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour {
    private Power energState;
    public int currentEnergy;
    public Text energyLabel;
    public static Energy instance;
    public Text deltaPrefab;

    public int switchOnColorModeCost = 1;
    public int switchOnGrayModeCost = 1;

    public int moveWithoutScreenCost = 1;
    public int moveWithGrayModeCost = 2;
    public int moveWithColorModeCost = 3;

    private void Awake()
    {
        instance = this;
    }

    public void SetPowerState(Power newState, bool IsInit = false)
    {
        if (energState != Power.zerodivision && energState == newState) return;
            energState = newState;

        if (IsInit) return;

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

    public void Start() 
    {
        energyLabel.gameObject.SetActive(true);
        if (Board.current == null)
            SetEnergy(76);
        else
            SetEnergy(Board.current.StartEnergyForLevel);
    }

    IEnumerator WaitAnyKayToLoad()
    {
        yield return new WaitForEndOfFrame();
        while (Input.anyKeyDown == false)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        Screen.instance.NoEnergy();

        if (currentEnergy == 0)
            RHandController.instance.LoadLevel(Application.loadedLevel);
    }

    private void SetEnergy(int value)
    {
        if (value <= 0)
        {
            Screen.instance.LowEnergy();
            value = 0;
            if (Board.current != null)
                StartCoroutine(WaitAnyKayToLoad());
            else
                value = 100;
        }

        currentEnergy = value;

        if (Application.loadedLevel == 1 && currentEnergy < 45 && Screen.instance.IsColor())
            PlayerMessage.instance.Show("NOOO! I should decrease energy spending by R_Mouse.");

        energyLabel.text = currentEnergy.ToString();
    }

    public void RemoveEnergy(int delta)
    {
        Instantiate<Text>(deltaPrefab, Vector3.back, Quaternion.identity, transform)
            .text = "- "+ delta;

        SetEnergy(currentEnergy - delta);
    }
}
