using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
    public static Menu instance;
    public List<Text> buttons = new List<Text>();
    public Transform Selector;
    public List<GameObject> coloredParts;

    private int selectedItem = 0;
    private void Awake()
    {
        instance = this;
    }

    void Start () {
        SelectOption(0);
        PlayerMessage.instance.Show("I need to switch on the screen by L_Mouse.", true);
	}

    public virtual void Gray()
    {
        foreach (var i in coloredParts)
            i.SetActive(false);

        PlayerMessage.instance.Show("Hm. I need to increase energy spending by L_Mouse.", true);
    }

    public virtual void Color()
    {
        foreach (var i in coloredParts)
            i.SetActive(true);

        SelectOption(0);
        PlayerMessage.instance.Show("Change option by WASD or Arrows.", true);
    }

    virtual public void SelectOption(int v, bool isNatural = false)
    {
        if (isNatural) AudioSystem.instance.PlayFloorStep();

        if (v < 0) v = 0;
        if (v > buttons.Count - 1) v = buttons.Count - 1;

        if (v == 1 && Screen.instance.IsColor())
            PlayerMessage.instance.Show("Great. I can select menu option by L_Mouse.", true);

        Selector.position = buttons[v].transform.position;
        selectedItem = v;
    }

    void Update () {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Keypad1)) BResetController.instance.PlayReset(1);
        if (Input.GetKeyDown(KeyCode.Keypad2)) BResetController.instance.PlayReset(2);
        if (Input.GetKeyDown(KeyCode.Keypad3)) BResetController.instance.PlayReset(3);
        if (Input.GetKeyDown(KeyCode.Keypad4)) BResetController.instance.PlayReset(4);
        if (Input.GetKeyDown(KeyCode.Keypad5)) BResetController.instance.PlayReset(5);
        if (Input.GetKeyDown(KeyCode.Keypad6)) BResetController.instance.PlayReset(6);
        if (Input.GetKeyDown(KeyCode.Keypad7)) BResetController.instance.PlayReset(7);
        if (Input.GetKeyDown(KeyCode.Keypad8)) BResetController.instance.PlayReset(8);
        if (Input.GetKeyDown(KeyCode.Keypad9)) BResetController.instance.PlayReset(9);
        if (Input.GetKeyDown(KeyCode.Keypad0)) BResetController.instance.PlayReset(10);
#endif

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            SelectOption(selectedItem + 1, true);
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            SelectOption(selectedItem - 1, true);

        if (Input.GetMouseButtonDown(0) && selectedItem == 1 && Screen.instance.IsColor())
        {
            AudioSystem.instance.PlayDoorOpen();
            PlayerMessage.instance.Hide();
            BResetController.instance.PlayReset(1);
        }

    }
}
