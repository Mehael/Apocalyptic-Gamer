using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    private Color startColor;
    private SpriteRenderer sprite;

    void Awake () {
        sprite = GetComponent<SpriteRenderer>();
        startColor = sprite.color;
	}
	
    public void GrayTint()
    {
        sprite.color = Color.white;
    }

    public void ColorTint()
    {
        sprite.color = startColor;
    }

    public bool PressedSignUp()
    {
        if (tag == "Weak" || tag == "Trap")
            return true;

        return false;
    }

    public Sprite activeTrapState;
    public Sprite passiveTrapState;
    public int TurnsToBecomePassive = 0;

    public bool StayOneMoreTurn()
    {
        if (tag == "Trap" && sprite.sprite == passiveTrapState)
        {
            tag = "Death";
            TurnsToBecomePassive = 2;
            sprite.sprite = activeTrapState;
            return true;
        }

        if (tag == "Death")
        {
            TurnsToBecomePassive--;
            if (TurnsToBecomePassive == 0)
            {
                tag = "Trap";
                sprite.sprite = passiveTrapState;
            }
            else return true;
        }

        if (tag == "Weak")
        {
            tag = "Death";
            sprite.enabled = false;
        }

        return false;
    }
}
