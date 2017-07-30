using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
    public static Menu instance;
    public List<Text> buttons = new List<Text>();
    public Transform Selector;
    public List<Image> coloredParts;
    private Dictionary<Transform, Color> savedColors = new Dictionary<Transform, Color>();

    private int selectedItem = 0;
    private void Awake()
    {
        instance = this;
    }

    void Start () {
        foreach (var i in coloredParts)
            savedColors.Add(i.transform, i.color);
        foreach (var i in buttons)
            savedColors.Add(i.transform, i.color);

        SelectOption(0);
        PlayerMessage.instance.Show("I need to switch on the screen by L_Mouse.", true);
	}

    public void Gray()
    {
        foreach (var i in coloredParts)
            i.color = UnityEngine.Color.white;
        foreach (var i in buttons)
            i.color = UnityEngine.Color.white;

        PlayerMessage.instance.Show("Hm. I need to increase energy spending by L_Mouse.", true);
    }

    public void Color()
    {
        foreach (var i in coloredParts)
            i.color = savedColors[i.transform];
        foreach (var i in buttons)
            i.color = savedColors[i.transform];
        PlayerMessage.instance.Show("Change option by WASD or Arrows.", true);
    }

    private void SelectOption(int v)
    {
        if (v < 0) v = 0;
        if (v > buttons.Count - 1) v = buttons.Count - 1;

        if (v == 1 && coloredParts[0].color == savedColors[coloredParts[0].transform])
            PlayerMessage.instance.Show("Great. I can choose menu option by L_Mouse.", true);

        Selector.position = buttons[v].transform.position;
        selectedItem = v;
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            SelectOption(selectedItem + 1);
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            SelectOption(selectedItem - 1);
        if (Input.GetMouseButtonDown(0) && selectedItem == 1)
        {
            PlayerMessage.instance.Hide();
            Application.LoadLevel(1);

        }

    }
}
