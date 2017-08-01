using System;
using UnityEngine;
using UnityEngine.UI;

public enum InputButton
{
    Up,
    Down,
    Left,
    Right,
    Start
}

public class InputSystem : MonoBehaviour
{
    public static InputSystem Instance { get; private set; }

    [SerializeField] private Button up;
    [SerializeField] private Button down;
    [SerializeField] private Button left;
    [SerializeField] private Button right;
    [SerializeField] private Button start;

    public event Action<InputButton> ButtonPressed;

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        Instance = this;
        RegisterButton(up, InputButton.Up);
        RegisterButton(down, InputButton.Down);
        RegisterButton(left, InputButton.Left);
        RegisterButton(right, InputButton.Right);
        RegisterButton(start, InputButton.Start);
    }

    private void RegisterButton(Button button, InputButton type)
    {
        button.onClick.AddListener(() => OnButtonClicked(type));
    }

    private void OnButtonClicked(InputButton button)
    {
        if (ButtonPressed != null)
            ButtonPressed(button);
    }
}
