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
        if (Board.current == null)
            SetState(Power.none);
        else
            SetState(Power.full);
    }

    public void LowEnergy()
    {
        SetState(Power.gray);
        ConsoleMessage.instance.Show("Your batteries ran out.\n Change them.");
    }

    private void SetState(Power newPower)
    {
        if ((newPower == Power.overcharge) || (newPower == Power.zerodivision)) return;

        currentPower = newPower;
        Energy.instance.SetPowerState(currentPower);

        if (currentPower == Power.none)
        {
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
            CurrentMarker.position = GrayMarker.position;
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
            CurrentMarker.position = OnMarker.position;
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus) || Input.GetMouseButtonDown(1))
            SetState(currentPower - 1);
        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetMouseButtonDown(0))
            SetState(currentPower + 1);
    }
}
