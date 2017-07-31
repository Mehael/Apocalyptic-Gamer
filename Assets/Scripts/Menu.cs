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

    public void Gray()
    {
        foreach (var i in coloredParts)
            i.SetActive(false);

        PlayerMessage.instance.Show("Hm. I need to increase energy spending by L_Mouse.", true);
    }

    public void Color()
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
        if (Input.GetKeyDown(KeyCode.Keypad1)) RHandController.instance.LoadLevel(1);
        if (Input.GetKeyDown(KeyCode.Keypad2)) RHandController.instance.LoadLevel(2);
        if (Input.GetKeyDown(KeyCode.Keypad3)) RHandController.instance.LoadLevel(3);
        if (Input.GetKeyDown(KeyCode.Keypad4)) RHandController.instance.LoadLevel(4);
        if (Input.GetKeyDown(KeyCode.Keypad5)) RHandController.instance.LoadLevel(5);
        if (Input.GetKeyDown(KeyCode.Keypad6)) RHandController.instance.LoadLevel(6);
        if (Input.GetKeyDown(KeyCode.Keypad7)) RHandController.instance.LoadLevel(7);
        if (Input.GetKeyDown(KeyCode.Keypad8)) RHandController.instance.LoadLevel(8);
        if (Input.GetKeyDown(KeyCode.Keypad9)) RHandController.instance.LoadLevel(9);
        if (Input.GetKeyDown(KeyCode.Keypad0)) RHandController.instance.LoadLevel(10);

#endif
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            SelectOption(selectedItem + 1, true);
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            SelectOption(selectedItem - 1, true);
        if (Input.GetMouseButtonDown(0) && selectedItem == 1 && Screen.instance.IsColor())
        {
            AudioSystem.instance.PlayDoorOpen();
            PlayerMessage.instance.Hide();
            RHandController.instance.LoadLevel(1);
        }

    }
}
