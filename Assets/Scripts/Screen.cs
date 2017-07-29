using UnityEngine;
using UnityEngine.UI;

public enum Power { zerodivision, none, gray, full, overcharge };

public class Screen : MonoBehaviour {
    public Power startPower = Power.full;
    public static Screen instance;
    private Power currentPower;
    public Transform offedScreen;
    public Text ScreenState;

    private void Awake()
    {
        instance = this;
    }

    public void Start() {
        if (Board.current == null)
            SetState(Power.none);
        else
            SetState(Power.full);
    }

    private void SetState(Power newPower)
    {
        if ((newPower == Power.overcharge) || (newPower == Power.zerodivision)) return;

        currentPower = newPower;
        Energy.instance.SetPowerState(currentPower);

        if (currentPower == Power.none)
        {
            offedScreen.gameObject.SetActive(true);
            ScreenState.text = "OFF";
            return;
        }
        offedScreen.gameObject.SetActive(false);

        if (currentPower == Power.gray)
        {
            if (Board.current == null)
            {
                Menu.instance.Gray();
            }
            else
            {
                foreach (var tile in Board.current.cells.Values)
                    tile.GrayTint();
                foreach (var tile in Board.current.items.Values)
                    tile.GrayTint();
            }
            ScreenState.text = "GRAY";
        }
        else
        {
            if (Board.current == null)
            {
                Menu.instance.Color();
            }
            else
            {
                foreach (var tile in Board.current.cells.Values)
                    tile.ColorTint();
                foreach (var tile in Board.current.items.Values)
                    tile.ColorTint();
            }
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
