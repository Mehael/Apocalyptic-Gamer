using System;
using System.Collections.Generic;
using UnityEngine;

public class Screen : MonoBehaviour
{
    public Power startPower = Power.Full;
    public static Screen instance;

    public PowerStateSliderView PowerStateSlider;
    public GameObject glow;

    private Power currentPower;
    public Transform offedScreen;

    private void Awake()
    {
        PowerStateSlider.StateChanged += state => SetState(state);
        instance = this;
    }

    List<String> BlindMessages = new List<string>()
    {
        "I can't see a shit!",
        "Where the hell am I?",
        "Damn this dark corridors",
        "Oh, I see, I see... Nothing =(",
    };

    List<String> ColorMessages = new List<string>()
    {
        "Hooray! I see again"
    };

    List<String> GrayMessages = new List<string>()
    {
        "Where are obstacles now?",
        "Everything is gray...Nice",
    };

    public void Start()
    {
        SetState(Application.loadedLevel == 0 ? Power.Gray : Power.Full, true);
    }

    public void LowEnergy()
    {
        SetState(Power.Gray);
        ConsoleMessage.instance.Show("Low batteries");
    }

    public void NoEnergy()
    {
        SetState(Power.None);
    }

    private void SetState(Power newPower, bool isInit = false)
    {
        if ((newPower == Power.Overcharge) || (newPower == Power.Zerodivision)) return;

        currentPower = newPower;
        Energy.instance.SetPowerState(currentPower, isInit);
        PowerStateSlider.SetState(newPower);

        if (currentPower == Power.None)
        {
            AudioSystem.instance.PlayBLIND();

            if (Application.loadedLevel == 1)
                PlayerMessage.instance.Show("I CAN pass it without a screen!");
            else
                PlayerMessage.instance.Show(BlindMessages);

            glow.SetActive(false);
            offedScreen.gameObject.SetActive(true);
            return;
        }

        if (offedScreen == null) return;

        glow.SetActive(true);
        offedScreen.gameObject.SetActive(false);

        if (currentPower == Power.Gray)
        {
            if (Application.loadedLevel > 1)
                PlayerMessage.instance.Show(GrayMessages);

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
        }
        else
        {
            if (Application.loadedLevel > 1)
                PlayerMessage.instance.Show(ColorMessages);

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
        }
    }

    internal bool IsColor()
    {
        return currentPower == Power.Full;
    }

    void Update()
    {
        return;
        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus) || Input.GetMouseButtonDown(1))
            SetState(currentPower - 1);
        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetMouseButtonDown(0))
            SetState(currentPower + 1);
    }
}
