using System;
using UnityEngine;
using UnityEngine.UI;

public enum Power { zerodivision, none, gray, full, overcharge };

public class Screen : MonoBehaviour {
    public Power startPower = Power.full;
    public static Screen instance;

    public Transform OffMarker;
    public Transform OnMarker;
    public Transform GrayMarker;

    public Transform CurrentMarker;
    public GameObject glow;

    private Power currentPower;
    public Transform offedScreen;

    private void Awake()
    {
        instance = this;
    }

    public void Start() {
        if (Application.loadedLevel == 0)
            SetState(Power.none, true);
        else
            SetState(Power.full, true);
    }

    public void LowEnergy()
    {
        SetState(Power.gray);
        ConsoleMessage.instance.Show("Low batteries");
    }

    private void SetState(Power newPower, bool isInit = false)
    {
        if ((newPower == Power.overcharge) || (newPower == Power.zerodivision)) return;

        currentPower = newPower;
        Energy.instance.SetPowerState(currentPower, isInit);

        if (currentPower == Power.none)
        {
            AudioSystem.instance.PlayBLIND();

            if (Application.loadedLevel == 1)
                PlayerMessage.instance.Show("I CAN pass it without a screen!");

            glow.SetActive(false);
            offedScreen.gameObject.SetActive(true);
            CurrentMarker.position = OffMarker.position;
            return;
        }

        if (offedScreen == null) return;

        glow.SetActive(true);
        offedScreen.gameObject.SetActive(false);

        if (currentPower == Power.gray)
        {
            AudioSystem.instance.PlayGRAY();
            if (Board.current == null)
            {
                Menu.instance.Gray();
            }
            else
            {
                PlayerController.instance.Gray();
                foreach (var tile in Board.current.cells.Values)
                    tile.GrayTint();
                foreach (var tile in Board.current.items.Values)
                    tile.GrayTint();
            }
            CurrentMarker.position = GrayMarker.position;
        }
        else
        {
            AudioSystem.instance.PlayCOLOR();
            if (Board.current == null)
            {
                Menu.instance.Color();
            }
            else
            {
                PlayerController.instance.Color();
                foreach (var tile in Board.current.cells.Values)
                    tile.ColorTint();
                foreach (var tile in Board.current.items.Values)
                    tile.ColorTint();
            }
            CurrentMarker.position = OnMarker.position;
        }
    }

    internal bool IsColor()
    {
        return currentPower == Power.full;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus) || Input.GetMouseButtonDown(1))
            SetState(currentPower - 1);
        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetMouseButtonDown(0))
            SetState(currentPower + 1);
    }
}
