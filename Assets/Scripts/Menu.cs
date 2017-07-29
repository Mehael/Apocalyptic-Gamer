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
	}

    public void Gray()
    {
        foreach (var i in coloredParts)
            i.color = UnityEngine.Color.white;
        foreach (var i in buttons)
            i.color = UnityEngine.Color.white;
    }

    public void Color()
    {
        foreach (var i in coloredParts)
            i.color = savedColors[i.transform];
        foreach (var i in buttons)
            i.color = savedColors[i.transform];
    }

    private void SelectOption(int v)
    {
        if (v < 0) v = 0;
        if (v > buttons.Count - 1) v = buttons.Count - 1;

        Selector.position = buttons[v].transform.position;
        selectedItem = v;
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            SelectOption(selectedItem + 1);
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            SelectOption(selectedItem - 1);
        if (Input.GetMouseButtonDown(0) && selectedItem == 1)
            Application.LoadLevel(1);

    }
}
